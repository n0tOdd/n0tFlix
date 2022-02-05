using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using n0tFlix.Plugin.Podnapisi.Configuration;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using MediaBrowser.Model.IO;
using MediaBrowser.Common;
using MediaBrowser.Model.Globalization;
using System.IO.Compression;
using System.Xml;

namespace n0tFlix.Plugin.Podnapisi
{
    /// <summary>
    /// The open subtitle downloader.
    /// </summary>
    public class SubtitleDownloader : ISubtitleProvider
    {
        private static readonly CultureInfo _usCulture = CultureInfo.ReadOnly(new CultureInfo("en-US"));
        private readonly ILogger<SubtitleDownloader> _logger;
        private readonly IFileSystem _fileSystem;
        private DateTime _lastRateLimitException;
        private DateTime _lastLogin;
        private int _rateLimitLeft = 40;
        private readonly HttpClient _httpClient;
        private readonly Downloader downloader;
        private readonly IApplicationHost _appHost;
        private ILocalizationManager _localizationManager;
        public SubtitleDownloader(ILogger<SubtitleDownloader> logger, IFileSystem fileSystem, IApplicationHost appHost, ILocalizationManager localizationManager)
        {
            _logger = logger;
            _fileSystem = fileSystem;
            
            downloader = new Downloader();
           
            _appHost = appHost;
            _localizationManager = localizationManager;

            //       OpenSubtitlesHandler.OpenSubtitles.SetUserAgent("jellyfin");
        }

        public string Name => "Podnapisi";

        private PluginConfiguration GetOptions()
            => Podnapisi.Instance.Configuration;

        public IEnumerable<VideoContentType> SupportedMediaTypes
            => new[] { VideoContentType.Episode, VideoContentType.Movie };



        private string NormalizeLanguage(string language)
        {
            if (language != null)
            {
                var culture = _localizationManager.FindLanguageInfo(language);
                if (culture != null)
                {
                    return culture.ThreeLetterISOLanguageName;
                }
            }

            return language;
        }

        public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            var pid = id.Split(',')[0];
            var title = id.Split(',')[1];
            var lang = id.Split(',')[2];
            string Url = $"https://www.podnapisi.net/{lang}/subtitles/{title}/{pid}/download";
            _logger.LogDebug("Requesting {0}", Url);

            using (var response = await _httpClient.GetAsync(Url).ConfigureAwait(false))
            {
                var ms = new MemoryStream();
                String sContentType = response.Content.Headers.ContentType.MediaType.ToLower();
                if (!sContentType.Contains("zip"))
                {
                    return new SubtitleResponse()
                    {
                        Language = NormalizeLanguage(lang),
                        Stream = ms
                    };
                }

                var archive = new ZipArchive(response.Content.ReadAsStream(), ZipArchiveMode.Read);

                await archive.Entries.FirstOrDefault().Open().CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;

                var fileExt = archive.Entries.FirstOrDefault().FullName.Split('.').LastOrDefault();

                if (string.IsNullOrWhiteSpace(fileExt))
                {
                    fileExt = "srt";
                }

                return new SubtitleResponse
                {
                    Format = fileExt,
                    Language = NormalizeLanguage(lang),
                    Stream = ms
                };

            }
        }

        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request,
     CancellationToken cancellationToken)
        {

            var url = new StringBuilder("http://podnapisi.net/subtitles/search/old?sXML=1");
            url.Append($"&sL={request.TwoLetterISOLanguageName}");
            if (request.SeriesName == null)
            {
                url.Append($"&sK={request.Name}");
            }
            else
            {
                url.Append($"&sK={request.SeriesName}");
            }
            if (request.ParentIndexNumber.HasValue)
            {
                url.Append($"&sTS={request.ParentIndexNumber}");
            }
            if (request.IndexNumber.HasValue)
            {
                url.Append($"&sTE={request.IndexNumber}");
            }
            if (request.ProductionYear.HasValue)
            {
                url.Append($"&sY={request.ProductionYear}");
            }
            url = url.Replace(" ", ".%20");

            _logger.LogError("Requesting {0}", url);

            try
            {
                using (var response = await this.downloader.GetStream(url.ToString(), "http://podnapisi.net", null, cancellationToken))
                {
                    this._logger?.LogError("Got result");
                   
                        var settings = Create(false);
                        settings.CheckCharacters = false;
                        settings.IgnoreComments = true;
                        settings.DtdProcessing = DtdProcessing.Parse;
                        settings.MaxCharactersFromEntities = 1024;
                        settings.Async = true;

                        using (var result = XmlReader.Create(response, settings))
                        {
                            return (await ParseSearch(result).ConfigureAwait(false)).OrderByDescending(i => i.DownloadCount);
                        }
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private XmlReaderSettings Create(bool enableValidation)
        {
            var settings = new XmlReaderSettings();

            if (!enableValidation)
            {
                settings.ValidationType = ValidationType.None;
            }

            return settings;
        }

        public int Order => 2;

        private async Task<List<RemoteSubtitleInfo>> ParseSearch(XmlReader reader)
        {
            var list = new List<RemoteSubtitleInfo>();
            await reader.MoveToContentAsync().ConfigureAwait(false);
            await reader.ReadAsync().ConfigureAwait(false);

            while (!reader.EOF && reader.ReadState == ReadState.Interactive)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "subtitle":
                            {
                                if (reader.IsEmptyElement)
                                {
                                    await reader.ReadAsync().ConfigureAwait(false);
                                    continue;
                                }
                                using (var subReader = reader.ReadSubtree())
                                {
                                    list.Add(await ParseSubtitleList(subReader).ConfigureAwait(false));
                                }
                                break;
                            }
                        default:
                            {
                                await reader.SkipAsync().ConfigureAwait(false);
                                break;
                            }
                    }
                }
                else
                {
                    await reader.ReadAsync().ConfigureAwait(false);
                }
            }
            return list;
        }

        private async Task<RemoteSubtitleInfo> ParseSubtitleList(XmlReader reader)
        {
            var SubtitleInfo = new RemoteSubtitleInfo
            {
                ProviderName = Name,
                Format = "srt"
            };
            await reader.MoveToContentAsync().ConfigureAwait(false);
            await reader.ReadAsync().ConfigureAwait(false);
            var id = new StringBuilder();

            while (!reader.EOF && reader.ReadState == ReadState.Interactive)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "pid":
                            {
                                id.Append($"{(await reader.ReadElementContentAsStringAsync().ConfigureAwait(false))},");
                                break;
                            }
                        case "release":
                            {
                                SubtitleInfo.Name = await reader.ReadElementContentAsStringAsync().ConfigureAwait(false);
                                break;
                            }
                        case "url":
                            {
                                id.Append($"{(await reader.ReadElementContentAsStringAsync().ConfigureAwait(false)).Split('/')[5]},");
                                break;
                            }
                        case "language":
                            {
                                var lang = await reader.ReadElementContentAsStringAsync().ConfigureAwait(false);
                                SubtitleInfo.ThreeLetterISOLanguageName = NormalizeLanguage(lang);
                                id.Append($"{lang},");
                                break;
                            }
                        case "rating":
                            {
                                SubtitleInfo.CommunityRating = await ReadFloat(reader).ConfigureAwait(false);
                                break;
                            }
                        case "downloads":
                            {
                                SubtitleInfo.DownloadCount = await ReadInt(reader).ConfigureAwait(false);
                                break;
                            }
                        default:
                            {
                                await reader.SkipAsync().ConfigureAwait(false);
                                break;
                            }
                    }
                }
                else
                {
                    await reader.ReadAsync().ConfigureAwait(false);
                }
            }
            SubtitleInfo.Id = id.ToString();
            return SubtitleInfo;
        }

        private async Task<float?> ReadFloat(XmlReader reader)
        {
            var val = await reader.ReadElementContentAsStringAsync().ConfigureAwait(false);

            if (float.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }

            return null;
        }

        private async Task<int?> ReadInt(XmlReader reader)
        {
            var val = await reader.ReadElementContentAsStringAsync().ConfigureAwait(false);

            if (int.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
            {
                return result;
            }

            return null;
        }
    }
}
