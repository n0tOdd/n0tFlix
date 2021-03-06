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
using n0tFlix.Plugin.Yifi.Configuration;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using AngleSharp;
using AngleSharp.Dom;
using System.IO.Compression;
using System.Web;

namespace n0tFlix.Plugin.Yifi
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
            => "Yifi";

        /// <inheritdoc />
        public IEnumerable<VideoContentType> SupportedMediaTypes
            => new[] {  VideoContentType.Movie };

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
            string query = request.Name + " " + request.ProductionYear;
            query = "https://yifysubtitles.org/search?q=" + HttpUtility.UrlEncode(query);
      
            string source = await new HttpClient().GetStringAsync(query);

            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(source));
            var ss = document.GetElementsByClassName("col-sm-12").First();
            var results = ss.GetElementsByTagName("li");
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
      
            foreach (IElement element in results)
            {
                var href = element.GetElementsByTagName("a").First();
                string uri = href.GetAttribute("href");
                if (string.IsNullOrEmpty(uri) || uri.StartsWith("javascript"))
                    continue;

                this.logger.LogError("https://yifysubtitles.org" + uri.Replace("%2","/"));
                string subPage = await new HttpClient().GetStringAsync("https://yifysubtitles.org" + uri.Replace("%2", "/"));
                this.logger.LogError(subPage.Length.ToString());
                if (string.IsNullOrEmpty(subPage))
                    continue;

                var sub = await browser.OpenAsync(x => x.Content(subPage));
                var subboxes = sub.GetElementsByClassName("table other-subs").First()
                    .GetElementsByTagName("tbody").First()
                    .GetElementsByTagName("tr");
                foreach (IElement subtitle in subboxes)
                {
                    string rating = subtitle.GetElementsByClassName("rating-cell").First().TextContent;
                    string language = subtitle.GetElementsByClassName("sub-lang").First().TextContent.ToLower();
                    string Name = subtitle.GetElementsByTagName("a").First().TextContent.ToLower().Replace("subtitle","");
                    

                    string link = "https://yifysubtitles.org" + subtitle.GetElementsByTagName("a").First().GetAttribute("href");
                    string uploader = subtitle.GetElementsByClassName("uploader-cell").First().TextContent;
                    if (language.StartsWith(request.Language.ToLower()))
                    {
                        list.Add(new RemoteSubtitleInfo()
                        {
                            
                            Author = uploader,
                            CommunityRating = float.Parse(rating),
                            Id = link + "_" + language,
                            ThreeLetterISOLanguageName = request.Language,
                            ProviderName ="Yifi",
                            IsHashMatch = false,
                            Name = Name,
                            Format = "srt"
                        });
                    }
                }
                //Check awailable subtitles for all the results
            }
            return list.ToArray();
        }

        
        private PluginConfiguration GetOptions()
            => Yifi.Instance!.Configuration;
    }
}
