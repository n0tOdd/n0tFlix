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
using n0tFlix.Plugin.Addic7ed.Configuration;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using AngleSharp.Dom;
using AngleSharp;

namespace n0tFlix.Plugin.Addic7ed
{
    /// <summary>
    /// The open subtitle downloader.
    /// </summary>
    public class SubtitleDownloader : ISubtitleProvider
    {
        private readonly ILogger<SubtitleDownloader> logger;

        private IReadOnlyList<string>? _languages;
        private readonly Downloader downloader;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubtitleDownloader"/> class.
        /// </summary>
        /// <param name="logger">Instance of the <see cref="ILogger{SubtitleDownloader}"/> interface.</param>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> for creating Http Clients.</param>
        public SubtitleDownloader(ILogger<SubtitleDownloader> logger)
        {
            this.logger = logger;
            this.downloader = new Downloader();
            this.logger.LogInformation("Loaded " + this.Name);
        }

        /// <inheritdoc />
        public string Name
            => this.GetType().Namespace.Replace("n0tFlix.Plugin.","");

        /// <inheritdoc />
        public IEnumerable<VideoContentType> SupportedMediaTypes
            => new[] { VideoContentType.Episode };

        /// <inheritdoc />
        public Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
            => GetSubtitlesInternal(id, cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request, CancellationToken cancellationToken)
        {
            var uri = "https://www.addic7ed.com/search.php?search=" +  request.SeriesName + request.ParentIndexNumber + "e" + request.IndexNumber + "&Submit=Search";
            string source = await downloader.GetString(uri, "https://www.addic7ed.com/",null,cancellationToken);
            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(source));
            var results = document.GetElementsByClassName("tabel95");
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
            foreach(var result in results)
            {
                var lang = result.GetElementsByClassName("language").First().TextContent;
                if(lang.ToLower().StartsWith(request.Language.ToLower()))
                {
                    list.Add(new RemoteSubtitleInfo()
                    {
                        Author = result.GetElementsByTagName("a").Where(x => x.HasAttribute("href") && x.GetAttribute("href").StartsWith("/user")).First().TextContent,
                        Id = "https://www.addic7ed.com" + result.GetElementsByClassName("buttonDownload").First().GetAttribute("href") + "__" + lang,
                        ProviderName = "Adddic7ed",
                        Format = "srt",
                        ThreeLetterISOLanguageName = request.Language,
                       
                        Name = result.GetElementsByClassName("NewsTitle").First().TextContent
                    });
                }
            }
            
            return list;
        }

        private async Task<SubtitleResponse> GetSubtitlesInternal(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Missing param", nameof(id));
            }
            string[] ss = id.Split("__");
            Stream data = await downloader.GetStream(ss[0], "https://www.addic7ed.com/", null, cancellationToken);
            //Remember to grab this info from page you collect the subtitle from
            return new SubtitleResponse { Format = "srt", Language = ss[1], Stream =data };
        }

        private PluginConfiguration GetOptions()
            => Addic7ed.Instance!.Configuration;
    }
}
