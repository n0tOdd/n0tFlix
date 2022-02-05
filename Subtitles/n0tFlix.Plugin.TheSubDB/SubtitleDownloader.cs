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
using n0tFlix.Plugin.TheSubDB.Configuration;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using MediaBrowser.Common;

namespace n0tFlix.Plugin.TheSubDB
{
    /// <summary>
    /// The open subtitle downloader.
    /// </summary>
    public class SubtitleDownloader : ISubtitleProvider
    {
        private readonly ILogger<SubtitleDownloader> logger;

        private IReadOnlyList<string>? _languages;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IApplicationHost _appHost;
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtitleDownloader"/> class.
        /// </summary>
        /// <param name="logger">Instance of the <see cref="ILogger{SubtitleDownloader}"/> interface.</param>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> for creating Http Clients.</param>
        public SubtitleDownloader(ILogger<SubtitleDownloader> logger, IHttpClientFactory httpClientFactory, IApplicationHost appHost)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.logger.LogInformation("Loaded " + this.Name);
            _appHost = appHost;

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
            var hash = await GetHash(request.MediaPath, cancellationToken);
       
            try
            {
                using (var response = await new HttpClient().GetAsync("http://api.thesubdb.com/?action=search&hash=" + hash).ConfigureAwait(false))
                {
                    
                    using (var reader = new StreamReader(response.Content.ReadAsStream()))
                    {
                        var result = await reader.ReadToEndAsync().ConfigureAwait(false);
                        logger.LogDebug("Search for subtitles for {0} returned {1}", hash, result);
                        return result
                            .Split(',')
                            .Where(lang => string.Equals(request.TwoLetterISOLanguageName, lang, StringComparison.OrdinalIgnoreCase)) //TODO: use three letter code
                            .Select(lang => new RemoteSubtitleInfo
                            {
                                IsHashMatch = true,
                                ProviderName = Name,
                                Id = $"{hash}&language={lang}",
                                Name = "A subtitle matched by hash",
                                Format = "srt",
                                ThreeLetterISOLanguageName = lang
                                
                            }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<SubtitleResponse> GetSubtitlesInternal(string id, CancellationToken cancellationToken)
        {
            using (var response = await new HttpClient().GetAsync("http://api.thesubdb.com/?action=download&hash=" + id).ConfigureAwait(false))
            {
                var ms = new MemoryStream();

                await response.Content.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;

                IEnumerable<string> cd;
                response.Headers.TryGetValues("Content-Disposition", out cd);

                foreach (string s in cd)
                {
                    var fileExt = (s ?? string.Empty).Split('.').LastOrDefault();

                    if (string.IsNullOrWhiteSpace(fileExt))
                    {
                        fileExt = "srt";
                    }

                    return new SubtitleResponse
                    {
                        Format = fileExt,
                        Language = id.Split('=').LastOrDefault(),
                        Stream = ms
                    };
                }
                return new SubtitleResponse();
            }
        }


        /// <summary>
        ///     Reads 64*1024 bytes from the start and the end of the file, combines them and returns its MD5 hash
        /// </summary>
        private async Task<string> GetHash(string path, CancellationToken cancellationToken)
        {
            const int readSize = 64 * 1024;
            var buffer = new byte[readSize * 2];
            logger.LogDebug("Reading {0}", path);
            using (var stream = File.OpenRead(path))
            {
                await stream.ReadAsync(buffer, 0, readSize, cancellationToken);

                if (stream.Length > readSize)
                {
                    stream.Seek(-readSize, SeekOrigin.End);
                }
                else
                {
                    stream.Position = 0;
                }

                await stream.ReadAsync(buffer, readSize, readSize, cancellationToken);
            }

            var hash = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                foreach (var b in md5.ComputeHash(buffer))
                    hash.Append(b.ToString("X2"));
            }
            logger.LogDebug("Computed hash {0} of {1}", hash.ToString(), path);
            return hash.ToString();
        }

        private PluginConfiguration GetOptions()
            => TheSubDB.Instance!.Configuration;
    }
}
