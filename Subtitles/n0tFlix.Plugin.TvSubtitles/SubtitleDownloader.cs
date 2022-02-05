using n0tFlix.Plugin.TvSubtitles.Configuration;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using System.Web;
using AngleSharp;
using AngleSharp.Dom;
using System.IO.Compression;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace n0tFlix.Plugin.TvSubtitles
{
    /// <summary>
    /// The open subtitle downloader.
    /// </summary>
    public class SubtitleDownloader : ISubtitleProvider
    {
        private readonly ILogger<SubtitleDownloader> logger;

        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubtitleDownloader"/> class.
        /// </summary>
        /// <param name="logger">Instance of the <see cref="ILogger{SubtitleDownloader}"/> interface.</param>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> for creating Http Clients.</param>
        public SubtitleDownloader(ILogger<SubtitleDownloader> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.logger.LogInformation("Loaded " + this.Name);
        }

        /// <inheritdoc />
        public string Name
            => "TvSubtitles";

        /// <inheritdoc />
        public IEnumerable<VideoContentType> SupportedMediaTypes
            => new[] { VideoContentType.Movie, VideoContentType.Episode };

        /// <inheritdoc />
        public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                return default;
            string url = id.Split("_").First();
            
            string source = await new HttpClient().GetStringAsync(url);
            if (string.IsNullOrEmpty(source))
                return default;
            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(source));
            var elem = document.GetElementsByClassName("btn-icon download-subtitle").First();
            string download = elem.GetAttribute("href");

            using (var stream = await new HttpClient().GetStreamAsync(download).ConfigureAwait(false))
            {
                ZipArchive zipArchive = new ZipArchive(stream);
                var entry = zipArchive.Entries.FirstOrDefault();
                if (entry != null)
                {
                    using (var unzippedEntryStream = entry.Open())
                    {
                        var ms = new MemoryStream();
                        await unzippedEntryStream.CopyToAsync(ms).ConfigureAwait(false);
                        return new SubtitleResponse()
                        {
                            Language = id.Split("_").Last(),
                            Stream = ms,
                            Format = "srt"
                        };
                    }
                }
            }
            return default;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request, CancellationToken cancellationToken)
        {
            string query = "q=" + request.Name + " " + request.ProductionYear;
            var post = new StringContent(query.Replace(" ", "+"));
            var source = await new HttpClient().PostAsync(HttpUtility.UrlEncode("http://www.tvsubtitles.net/search.php"), post);
            string cont = await source.Content.ReadAsStringAsync();
            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(cont));
            var results = document.GetElementsByClassName("margin-left:2em").First();
            var links = results.GetElementsByTagName("a");
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
            //todo add så den henter ut riktig episode som vi trenger
            return list;
        }

        
        private PluginConfiguration GetOptions()
            => TvSubtitles.Instance!.Configuration;
    }
}
