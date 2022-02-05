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
using n0tFlix.Plugin.SubDivX.Configuration;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Controller.Library;
using System.Text.RegularExpressions;
using System.IO.Compression;

namespace n0tFlix.Plugin.SubDivX
{
    public class SubdivXDownloader : ISubtitleProvider, IHasOrder
    {
        private readonly ILogger<SubdivXDownloader> _logger;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILibraryManager _libraryManager;

        private PluginConfiguration GetOptions() => SubDivX.Instance.Configuration;

        private PluginConfiguration _configuration => SubDivX.Instance.Configuration;

        public string Name => "SubdivX";

        public IEnumerable<VideoContentType> SupportedMediaTypes => new List<VideoContentType> { VideoContentType.Episode, VideoContentType.Movie };

        public int Order => 1;

        public SubdivXDownloader(ILogger<SubdivXDownloader> logger
            , IJsonSerializer jsonSerializer
            , ILibraryManager libraryManager)
        {
            _logger = logger;
            _jsonSerializer = jsonSerializer;
            _libraryManager = libraryManager;
        }

        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request, CancellationToken cancellationToken)
        {
            await Task.Run(() => {
                var json = _jsonSerializer.SerializeToString(request);
                _logger.LogInformation($"SubdivX Search | Request-> {json}");
            });

            var configuration = GetOptions();

            _logger.LogInformation($"SubdivX Search | UseOriginalTitle-> {configuration.UseOriginalTitle}");

            if (!string.Equals(request.TwoLetterISOLanguageName, "ES", StringComparison.OrdinalIgnoreCase))
            {
                return Array.Empty<RemoteSubtitleInfo>();
            }

            var item = _libraryManager.FindByPath(request.MediaPath, false);

            if (request.ContentType == VideoContentType.Episode)
            {
                var name = request.SeriesName;

                if (configuration.UseOriginalTitle)
                    if (!string.IsNullOrWhiteSpace(item.OriginalTitle))
                        name = item.OriginalTitle;

                var query = $"{name} S{request.ParentIndexNumber:D2}E{request.IndexNumber:D2}";

                var subtitles = SearchSubtitles(query);
                if (subtitles?.Count > 0)
                    return subtitles;
            }
            else
            {
                var name = request.Name;
                if (configuration.UseOriginalTitle)
                    if (!string.IsNullOrWhiteSpace(item.OriginalTitle))
                        name = item.OriginalTitle;

                var query = $"{name} {request.ProductionYear}";

                var subtitles = SearchSubtitles(query);
                if (subtitles?.Count > 0)
                    return subtitles;
            }

            return Array.Empty<RemoteSubtitleInfo>();
        }

        public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            await Task.Run(() => {
                _logger.LogInformation($"SubdivX GetSubtitles id: {id}");
            });

            var subtitle = DownloadSubtitle(id);
            if (subtitle != null)
                return subtitle;

            return new SubtitleResponse();
        }

        private List<RemoteSubtitleInfo> SearchSubtitles(string query)
        {
            var page = 1;
            var subtitles = new List<RemoteSubtitleInfo>();
            do
            {
                var list = SearchSubtitles(query, page);
                if (list == null)
                    break;

                subtitles.AddRange(list);
                page++;
            } while (true);

            return subtitles;
        }

        private List<RemoteSubtitleInfo> SearchSubtitles(string query, int page)
        {
            var html = GetHtml($"http://www.subdivx.com/index.php?accion=5&q={query}&pg={page}");
            if (string.IsNullOrWhiteSpace(html))
                return null;

            string reListSub = "<a\\s+class=\"titulo_menu_izq\"\\s+href=\"http://www.subdivx.com/(?<id>[a-zA-Z\\w -]*).html\">(?<title>.*)</a></div>";
            reListSub += "+.*<img\\s+src=\"img/calif(?<calif>\\d)\\.gif\"\\s+class=\"detalle_calif\"\\s+name=\"detalle_calif\">+.*";
            reListSub += "\\n<div\\s+id=\"buscador_detalle_sub\">(?<desc>.*?)</div>+.*<b>Downloads:</b>(?<download>.+?)<b>Cds:</b>+.*<b>Subido\\ por:</b>\\s*<a.+?>(?<uploader>.+?)</a>.+?</div></div>";
            Regex re = new Regex(reListSub);

            var mat = re.Matches(html);
            if (mat.Count == 0)
                return null;

            var subtitles = new List<RemoteSubtitleInfo>();
            foreach (Match x in mat)
            {
                var sub = new RemoteSubtitleInfo()
                {
                    Name = "",
                    ThreeLetterISOLanguageName = "ESP",
                    Id = x.Groups["id"].Value,
                    CommunityRating = float.Parse(x.Groups["calif"].Value.Trim()),
                    DownloadCount = int.Parse(x.Groups["download"].Value.Trim().Replace(",", "").Replace(".", "")),
                    Author = x.Groups["uploader"].Value,
                    ProviderName = Name,
                    Format = "srt"
                };
                if (_configuration.ShowTitleInResult || _configuration.ShowUploaderInResult)
                {
                    if (_configuration.ShowTitleInResult)
                        sub.Name = $"{x.Groups["title"].Value.Trim()}";

                    if (_configuration.ShowUploaderInResult)
                        sub.Name += (_configuration.ShowTitleInResult ? " | " : "") + $"Uploader: { x.Groups["uploader"].Value.Trim()}";

                    sub.Comment = x.Groups["desc"].Value;
                }
                else
                {
                    sub.Name = x.Groups["desc"].Value;
                }

                subtitles.Add(sub);
            }

            return subtitles;
        }

        private SubtitleResponse DownloadSubtitle(string id)
        {
            var html = GetHtml($"http://www.subdivx.com/{id}.html");
            if (string.IsNullOrWhiteSpace(html))
                return null;

            string reDownloadPage = "<a class=\"link1\" href=\".*=(?<id>.*?)&u=(?<u>.*)\">Bajar subtítulo ahora</a>";
            Regex re2 = new Regex(reDownloadPage);

            var mat2 = re2.Match(html);
            if (!mat2.Success)
                return null;

            var fileId = mat2.Groups["id"].Value;
            var u = mat2.Groups["u"].Value;

            var getSubtitleUrl = $"http://www.subdivx.com/bajar.php?id={fileId}&u={u}";

            var fileStream = GetFileStream(getSubtitleUrl);

            return new SubtitleResponse()
            {
                Format = "srt",
                IsForced = false,
                Language = "ES",
                Stream = fileStream
            };
        }

        private string GetHtml(string urlAddress)
        {
            _logger.LogInformation($"GetHtml Url: {urlAddress}");

            string data = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (Stream receiveStream = response.GetResponseStream())
                {
                    StreamReader readStream = null;

                    if (string.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
            }
            return data;
        }

        private Stream GetFileStream(string urlAddress)
        {
            _logger.LogDebug($"GetFileStream Url: {urlAddress}");

            Stream fileStream = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var fileStreamZip = response.GetResponseStream())
                    {
                        fileStream = UnzipAsync(fileStreamZip).Result;
                    }

                    response.Close();
                }
            }

            return fileStream;
        }

        private async Task<Stream> UnzipAsync(Stream zippedStream)
        {
            MemoryStream ms = new MemoryStream();
            var archive = new ZipArchive(zippedStream, ZipArchiveMode.Read);

            await archive.Entries.FirstOrDefault().Open().CopyToAsync(ms).ConfigureAwait(false);
            ms.Position = 0;

            var fileExt = archive.Entries.FirstOrDefault().FullName.Split('.').LastOrDefault();

            if (string.IsNullOrWhiteSpace(fileExt))
            {
                fileExt = "srt";
            }

            return ms;
        }
    }
}
