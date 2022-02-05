using MediaBrowser.Common.Net;
using MediaBrowser.Model.IO;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Plugin.NapiSub.Core
{
    public static class NapiCore
    {
        public static async Task<string> GetHash(string path, CancellationToken cancellationToken, IFileSystem fileSystem, ILogger logger)
        {
            var buffer = new byte[10485760];
            logger.LogInformation($"Reading {path}");

            using (var stream = File.OpenRead(path))
            {
                await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
            }

            string hash;
            using (var md5 = MD5.Create())
            {
                hash = ToHex(md5.ComputeHash(buffer));
            }

            logger.LogInformation($"Computed hash {hash} of {path} for n0tFlix.Plugin.NapiSub");
            return hash;
        }

        public static string ToHex(IReadOnlyCollection<byte> bytes)
        {
            var result = new StringBuilder(bytes.Count * 2);
            foreach (var t in bytes)
                result.Append(t.ToString("x2"));
            return result.ToString();
        }

        public static string GetSecondHash(string input)
        {
            if (input.Length != 32) return "";
            int[] idx = { 0xe, 0x3, 0x6, 0x8, 0x2 };
            int[] mul = { 2, 2, 5, 4, 3 };
            int[] add = { 0x0, 0xd, 0x10, 0xb, 0x5 };
            var b = "";
            for (var j = 0; j <= 4; j++)
            {
                var a = add[j];
                var m = mul[j];
                var i = idx[j];
                var t = a + int.Parse(input[i] + "", NumberStyles.HexNumber);
                var v = int.Parse(t == 31 ? input.Substring(t, 1) : input.Substring(t, 2), NumberStyles.HexNumber);
                var x = v * m % 0x10;
                b += x.ToString("x");
            }
            return b;
        }

        public static HttpRequestOptions CreateRequest(string hash, string language)
        {
            if (hash == null) return null;

            var opts = new HttpRequestOptions
            {
                Url = N.Instance.Configuration.GetNapiUrl,
                UserAgent = Plugin.Instance.Configuration.GetUserAgent,
            };

            var dic = new Dictionary<string, string>
            {
                {
                    "mode", Plugin.Instance.Configuration.GetMode
                },
                {
                    "client", Plugin.Instance.Configuration.GetClientName
                },
                {
                    "client_ver", Plugin.Instance.Configuration.GetClientVer
                },
                {
                    "downloaded_subtitles_id", hash
                },
                {
                    "downloaded_subtitles_txt", Plugin.Instance.Configuration.GetSubtitlesAsText
                },
                {
                    "downloaded_subtitles_lang", language
                }
            };
            opts.RequestContent = SetPostData(dic);
            opts.RequestContentType = "application/x-www-form-urlencoded";
    

            return opts;
        }

        public static string SetPostData(IDictionary<string, string> values)
        {
            var strings = values.Keys.Select(key => string.Format("{0}={1}", key, values[key]));
            var postContent = string.Join("&", strings.ToArray());

            return postContent;
            
        }
    }
}
