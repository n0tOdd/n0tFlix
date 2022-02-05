using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Net;
using MediaBrowser.Model.Providers;
using n0tFlix.Plugin.NapiSub.Core;
using n0tFlix.Plugin.NapiSub.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System;
using MediaBrowser.Model.Globalization;
using Microsoft.Extensions.Logging;
using n0tFlix.Plugin.NapiSub.Configuration;
using MediaBrowser.Common;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Model.Serialization;
using System.Net.Http;

namespace n0tFlix.Plugin.NapiSub.Provider
{
    public class NapiSubProvider : ISubtitleProvider
    {
        private readonly ILogger<NapiSubProvider> _logger;
        private readonly IFileSystem _fileSystem;
        private readonly HttpClient _httpClient;

        private ILocalizationManager _localizationManager;

        private readonly IServerConfigurationManager _config;

        private readonly IJsonSerializer _json;

        public NapiSubProvider(ILogger<NapiSubProvider> logger, IHttpClientFactory httpClientFactory, IServerConfigurationManager config, IJsonSerializer json, IFileSystem fileSystem, ILocalizationManager localizationManager)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();


            _config = config;
            _json = json;
            _fileSystem = fileSystem;
            _localizationManager = localizationManager;
        }

        public async Task<SubtitleResponse> GetSubtitles(string hash, CancellationToken cancellationToken)
        {
            var opts = NapiCore.CreateRequest(hash, "PL");
            _logger.LogInformation($"Requesting {opts.Url}");

            try
            {
                using (var response = await _httpClient.Post(opts).ConfigureAwait(false))
                {
                    using (var reader = new StreamReader(response.Content))
                    {
                        var xml = await reader.ReadToEndAsync().ConfigureAwait(false);
                        var status = XmlParser.GetStatusFromXml(xml);

                        if (status != null && status == "success")
                        {
                            var subtitlesBase64 = XmlParser.GetSubtitlesBase64(xml);
                            var stream = XmlParser.GetSubtitlesStream(subtitlesBase64);
                            var subRip = SubtitlesConverter.ConvertToSubRipStream(stream);

                            if (subRip != null)
                            {
                                return new SubtitleResponse
                                {
                                    Format = "srt",
                                    Language = "PL",
                                    Stream = subRip
                                };
                            }
                        }
                    }
                }

                _logger.LogInformation("No subtitles downloaded");
                return new SubtitleResponse();
            }
            catch (HttpException ex)
            {
                if (!ex.StatusCode.HasValue || ex.StatusCode.Value != HttpStatusCode.NotFound) throw;
                _logger.LogDebug("ERROR");
                return new SubtitleResponse();
            }
        }

        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request,
            CancellationToken cancellationToken)
        {
            var language = _localizationManager.FindLanguageInfo(request.Language);

            if (language == null || !string.Equals(language.TwoLetterISOLanguageName, "PL", StringComparison.OrdinalIgnoreCase))
            {
                return Array.Empty<RemoteSubtitleInfo>();
            }

            var hash = await NapiCore.GetHash(request.MediaPath, cancellationToken, _fileSystem, _logger).ConfigureAwait(false);
            var opts = NapiCore.CreateRequest(hash, language.TwoLetterISOLanguageName);

            try
            {
                using (var response = await _httpClient.Post(opts).ConfigureAwait(false))
                {
                    using (var reader = new StreamReader(response.Content))
                    {
                        var xml = await reader.ReadToEndAsync().ConfigureAwait(false);
                        var status = XmlParser.GetStatusFromXml(xml);

                        if (status != null && status == "success")
                        {
                            _logger.LogInformation("Subtitles found by n0tFlix.Plugin.NapiSub");

                            return new List<RemoteSubtitleInfo>
                            {
                                new RemoteSubtitleInfo
                                {
                                    IsHashMatch = true,
                                    ProviderName = Name,
                                    Id = hash,
                                    Name = "A subtitle matched by hash",
                                    ThreeLetterISOLanguageName = language.ThreeLetterISOLanguageName,
                                    Format = "srt"
                                }
                            };
                        }
                    }

                    _logger.LogInformation("No subtitles found by n0tFlix.Plugin.NapiSub");
                    return new List<RemoteSubtitleInfo>();
                }
            }
            catch (HttpException ex)
            {
                if (!ex.StatusCode.HasValue || ex.StatusCode.Value != HttpStatusCode.NotFound) throw;
                _logger.LogDebug("ERROR");
                return new List<RemoteSubtitleInfo>();
            }
        }

        public string Name
        {
            get { return "NapiSub"; }
        }

        private PluginConfiguration GetOptions()
        {
            return Plugin.Instance.Configuration;
        }
        public IEnumerable<VideoContentType> SupportedMediaTypes =>
            new List<VideoContentType> { VideoContentType.Episode, VideoContentType.Movie };

        public int Order => 1;
    }
}