using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Plugin.SubtitleBase
{

    public class Downloader
    {
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:85.0) Gecko/20100101 Firefox/85.0";

        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler handler;
        public Downloader()
        {
            handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.SslProtocols = SslProtocols.None & SslProtocols.Tls12 & SslProtocols.Tls13 & SslProtocols.Ssl2 & SslProtocols.Ssl3 & SslProtocols.Default;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => {
                    return true;
                };
            handler.MaxAutomaticRedirections = 10;
            handler.CheckCertificateRevocationList = false;
            handler.AutomaticDecompression = System.Net.DecompressionMethods.All;
           
            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            _httpClient.DefaultRequestHeaders.Add("Pragma", "no-cache");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> GetString(string link,string referer, Dictionary<string, string> post_params, CancellationToken cancellationToken)
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
            response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<Stream> GetStream(string link,string referer, Dictionary<string, string> post_params, CancellationToken cancellationToken)
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
            response = await _httpClient.SendAsync(request, cancellationToken);

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
    }
}
