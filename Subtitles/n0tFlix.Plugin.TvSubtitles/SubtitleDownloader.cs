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
using n0tFlix.Plugin.TvSubtitles.Configuration;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using AngleSharp.Dom;
using AngleSharp;
using System.Web;
using System.IO.Compression;
using n0tFlix.Helpers.Downloader;

namespace n0tFlix.Plugin.TvSubtitles
{
    /// <summary>
    /// The open subtitle downloader.
    /// </summary>
    public class SubtitleDownloader : ISubtitleProvider
    {
        private readonly ILogger<SubtitleDownloader> logger;

        private IReadOnlyList<string>? _languages;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly n0tHttpClient client;
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtitleDownloader"/> class.
        /// </summary>
        /// <param name="logger">Instance of the <see cref="ILogger{SubtitleDownloader}"/> interface.</param>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> for creating Http Clients.</param>
        public SubtitleDownloader(ILogger<SubtitleDownloader> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            LoggerFactory ll = new LoggerFactory();
            client = new n0tHttpClient(ll);

            this.logger.LogInformation("Loaded " + this.Name);
        }

        /// <inheritdoc />
        public string Name
            => this.GetType().Namespace.Replace("n0tFlix.Plugin.","");

        /// <inheritdoc />
        public IEnumerable<VideoContentType> SupportedMediaTypes
            => new[] { VideoContentType.Episode };

        /// <inheritdoc />
        public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                return default;
            string url = id;

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
            string searchwork = string.Empty;
            if (!string.IsNullOrEmpty(request.SeriesName))
                searchwork = request.SeriesName;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("q", searchwork);
            var source = await client.GetStringAsync("http://www.tvsubtitles.net/search.php", cancellationToken, headers, default);
            string cont = source;
            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(cont));
            var results = document.GetElementsByTagName("ul").Last();
            var links = results.GetElementsByTagName("a");
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
            foreach (var link in links)
            {
                try
                {
                    logger.LogError(link.InnerHtml);
                    if (!link.InnerHtml.Contains(searchwork, StringComparison.OrdinalIgnoreCase))
                        continue;
                    string url = "http://www.tvsubtitles.net" + link.GetAttribute("href");
                    if (string.IsNullOrEmpty(url))
                        continue;
                    string res = await client.GetStringAsync(url, cancellationToken);
                    document = await browser.OpenAsync(x => x.Content(res));
                    var desc = document.GetElementsByClassName("description").First();
                    var seasons = desc.GetElementsByTagName("a");
                    logger.LogError(request.ParentIndexNumber.Value.ToString());
                    var correct = seasons.Where(x =>  x.TextContent.Contains("Season " + request.ParentIndexNumber.Value.ToString(), StringComparison.OrdinalIgnoreCase)).First();
                    string seasonurl = "http://www.tvsubtitles.net/" + correct.GetAttribute("href");
                    logger.LogError(seasonurl);

                    res = await client.GetStringAsync(seasonurl, cancellationToken);
                    document = await browser.OpenAsync(x => x.Content(res));
                    logger.LogError(request.IndexNumber.Value.ToString());
                    var episodes = document.GetElementsByTagName("tbody")[2].GetElementsByTagName("tr");
                    logger.LogError(episodes.Count().ToString());
                    var thisone = episodes.Where(x => x.GetElementsByTagName("td").First().TextContent.Split("x").Last().EndsWith(request.IndexNumber.ToString(), StringComparison.OrdinalIgnoreCase)).First();
                    var hrr = thisone.GetElementsByTagName("a").Where(x => x.GetAttribute("href").StartsWith("subtitle")).First();
                    string dllink = "http://www.tvsubtitles.net/" + hrr.GetAttribute("href");
                    logger.LogError(dllink);
                    res = await client.GetStringAsync(dllink, cancellationToken);
                    document = await browser.OpenAsync(x => x.Content(res));
                    string title = document.QuerySelector("//*[@id=\"content\"]/div[4]/div/div[3]/table/tbody/tr[2]/td[3]").TextContent;
                    string author = document.QuerySelector("//*[@id=\"content\"]/div[4]/div/div[3]/table/tbody/tr[5]/td[3]").TextContent;

                    list.Add(new RemoteSubtitleInfo()
                    {
                        Name = title,
                        Format = "srt",
                        Id = dllink.Replace("subtitle", "download"),
                        Author = author,
                        ProviderName = "TvSubtitles"
                    });
                }
                catch (Exception ex)
                {
                    this.logger.LogError("Error in tvsubtitles:");
                    this.logger.LogError(ex.Message);

                }
            }
            return list;
        }


        private PluginConfiguration GetOptions()
            => TvSubtitles.Instance!.Configuration;
    }
}
