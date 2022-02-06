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
            var uri = "https://www.addic7ed.com/search.php?search=" +  request.SeriesName + " s" + request.ParentIndexNumber + "e" + request.IndexNumber + "&Submit=Search";
            this.logger?.LogError(uri);
            string source = await downloader.GetString(uri, "https://www.addic7ed.com/",null,cancellationToken);
            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(source));
            var results = document.GetElementsByClassName("tabel95");
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
            foreach (var result in results)
            {
                var lang = result.GetElementsByClassName("language").Where(x => x.TextContent.ToLower().StartsWith(request.Language)).FirstOrDefault();
                if (lang == null)
                    continue;
                try
                {
                    string auth = result.GetElementsByTagName("a").Where(x => x.HasAttribute("href") && x.GetAttribute("href").StartsWith("/user")).First().TextContent;
                    this.logger.LogError(auth);
                    
                    string id = "https://www.addic7ed.com" + lang.ParentElement.GetElementsByClassName("buttonDownload").First().GetAttribute("href") + "__" + lang;
                    this.logger.LogError(id);
                    string name = result.GetElementsByClassName("NewsTitle").First().TextContent;
                    this.logger.LogError(name);


                    list.Add(new RemoteSubtitleInfo()
                    {
                        Author = auth,
                        Id = id,
                        ProviderName = "Adddic7ed",
                        Format = "srt",
                        ThreeLetterISOLanguageName = request.Language,

                        Name = name
                    });
                }
                catch(Exception ex)
                {
                    this.logger.LogError(ex.Message);
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
