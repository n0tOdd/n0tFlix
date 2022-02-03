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
using n0tFlix.Plugin.SubtitleBase.Configuration;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;

namespace n0tFlix.Plugin.SubtitleBase
{
    /// <summary>
    /// The open subtitle downloader.
    /// </summary>
    public class SubtitleDownloader : ISubtitleProvider
    {
        private readonly ILogger<SubtitleDownloader> logger;

        private IReadOnlyList<string>? _languages;
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
            => this.GetType().Namespace.Replace("n0tFlix.Plugin.","");

        /// <inheritdoc />
        public IEnumerable<VideoContentType> SupportedMediaTypes
            => new[] { VideoContentType.Episode, VideoContentType.Movie };

        /// <inheritdoc />
        public Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
            => GetSubtitlesInternal(id, cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request, CancellationToken cancellationToken)
        {
            //This is the function used to search for and collect the subtitles matching our query
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return Enumerable.Empty<RemoteSubtitleInfo>();
        }

        private async Task<SubtitleResponse> GetSubtitlesInternal(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Missing param", nameof(id));
            }
            //Remember to grab this info from page you collect the subtitle from
            string format = "srt";
            string language = "english";
            byte[] data = new byte[16];
            return new SubtitleResponse { Format = format, Language = language, Stream = new MemoryStream(data) };
        }

        private PluginConfiguration GetOptions()
            => SubtitleBase.Instance!.Configuration;
    }
}
