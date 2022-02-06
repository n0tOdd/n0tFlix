using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Controller.Session;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.Logging;

namespace n0tFlix.Plugin.YoutubeDL
{
    public class YoutubeDlEntryPoint : IServerEntryPoint
    {
        private IFileSystem FileSystem { get; set; }
        private ILogger Logger { get; set; }
        private HttpClient httpclient { get; set; }
        private IServerApplicationPaths ServerApplicationPaths { get; set; }
        private static ISessionManager SessionManager { get; set; }
        private static IJsonSerializer JsonSerializer { get; set; }

        // ReSharper disable once TooManyDependencies
        public YoutubeDlEntryPoint(ILogger<YoutubeDlEntryPoint> logger, IServerApplicationPaths paths, IHttpClientFactory httpClientFactory)
        {
            Logger = logger;
            ServerApplicationPaths = paths;
            this.httpclient = httpClientFactory.CreateClient();
        }

        public void Dispose()
        {

        }

        public async Task RunAsync()
        {

            FileInfo plug = new FileInfo(Plugin.Instance.AssemblyFilePath);
            string PluginDir = plug.Directory.FullName;
            
            string youtube_dl_path = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {

                
                if (!File.Exists(Path.Combine(PluginDir, "youtube-dl.exe")))
                {
                    Logger.LogDebug("Downloading youtube-dl.exe");
                    Stream youtubeDL = await httpclient.GetStreamAsync("https://yt-dl.org/downloads/latest/youtube-dl.exe");
                    using (var fs = new FileStream(Path.Combine(PluginDir, "youtube-dl.exe"), FileMode.CreateNew))
                    {
                        await youtubeDL.CopyToAsync(fs);
                    }
                }
                Plugin.Instance.Configuration.YoutubeDlFilePath = Path.Combine(PluginDir, "youtube-dl.exe");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                if (!File.Exists(Path.Combine(PluginDir, "youtube-dl")))
                {
                    Logger.LogDebug("Downloading youtube-dl");
                    Stream youtubeDL = await httpclient.GetStreamAsync("https://yt-dl.org/downloads/latest/youtube-dl");
                    using (var fs = new FileStream(Path.Combine(PluginDir, "youtube-dl"), FileMode.CreateNew))
                    {
                        await youtubeDL.CopyToAsync(fs);
                    }
                }
                Plugin.Instance.Configuration.YoutubeDlFilePath = Path.Combine(PluginDir, "youtube-dl");
            }
            else
            {
                Logger.LogError("ERROR, CANT RECOGNIZE OS");

            }
        }
    }

}
