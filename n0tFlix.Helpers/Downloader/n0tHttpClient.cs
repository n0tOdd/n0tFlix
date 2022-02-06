using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace n0tFlix.Helpers.Downloader
{
    /// <summary>
    /// Just a premade HttpClient wrapper so i dont need to recreate it on every project
    /// </summary>
    public class n0tHttpClient
    {

        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.80 Safari/537.36 Edg/98.0.1108.43";

        private readonly HttpClient httpClient;
        private readonly HttpClientHandler handler;
        internal CookieContainer cookieContainer;
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger<n0tHttpClient> logger;

        public n0tHttpClient(ILoggerFactory loggerFactory, bool DisableSslVerification=true,int TimeOutInSeconds = 45)
        {
            this.loggerFactory = loggerFactory;
            this.logger = this.loggerFactory.CreateLogger<n0tHttpClient>();
            this.cookieContainer = new CookieContainer();
            handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression = System.Net.DecompressionMethods.All;
            handler.CookieContainer = this.cookieContainer;
            handler.UseCookies = true;
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.SslProtocols = SslProtocols.None & SslProtocols.Tls12 & SslProtocols.Tls13;
            if (DisableSslVerification)
            {
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                handler.CheckCertificateRevocationList = false;
            }else
            {
                handler.CheckCertificateRevocationList = true;

            }
            handler.MaxAutomaticRedirections = 10;

            httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            httpClient.DefaultRequestHeaders.Add("Pragma", "no-cache");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            httpClient.Timeout = TimeSpan.FromSeconds(TimeOutInSeconds);
        }


        /// <summary>
        /// Creates a new cookiecontainer for us
        /// </summary>
        /// <returns></returns>
        public async Task DeleteCookies()
        {
            this.cookieContainer = new CookieContainer();
        }


        public async Task<Stream> GetStreamAsync(string link, CancellationToken cancellationToken, string referer=null, Dictionary<string, string> post_params=null)
        {
            HttpRequestMessage request;
            HttpResponseMessage response;

            if (post_params != null && post_params.Count() > 0)
            {
                request = new HttpRequestMessage(HttpMethod.Post, link);
                request.Content = new FormUrlEncodedContent(post_params);
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Get, link);
            }

            request.Headers.Add("Referer", referer);
            response = await httpClient.SendAsync(request, cancellationToken);

            Stream memStream = new MemoryStream();

            if (response.Content.Headers.ContentEncoding.Contains("gzip"))
            {
                var gz = new GZipStream(response.Content.ReadAsStream(), CompressionMode.Decompress);
                gz.CopyTo(memStream);
            }
            else
            {
                await response.Content.CopyToAsync(memStream, cancellationToken);
            }
            memStream.Seek(0, SeekOrigin.Begin);
            return memStream;
        }



        public async Task<string> GetStringAsync(string link, CancellationToken cancellationToken, Dictionary<string, string> post_params=null, string referer = null)
        {
            HttpRequestMessage request;
            HttpResponseMessage response;

            //Detects if we are gonna post some data or just do a get request
            if (post_params != null && post_params.Count() > 0)
            {
                request = new HttpRequestMessage(HttpMethod.Post, link);
                request.Content = new FormUrlEncodedContent(post_params);
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Get, link);
            }
            //Puts a custom referer if set if not it goes for our main host url
            if(!string.IsNullOrEmpty(referer))
                request.Headers.Add("Referer", referer);
            else
                request.Headers.Add("Referer", request.RequestUri.Host);
            
            response = await httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return result;
        }
    }
}
