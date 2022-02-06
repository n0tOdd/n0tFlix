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
            Logger.LogError(Plugin.Instance.AssemblyFilePath);
            Logger.LogError(Plugin.Instance.ConfigurationFilePath);
            Logger.LogError(Plugin.Instance.DataFolderPath);
            
            string youtube_dl_path = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {

                
                if (!File.Exists(Path.Combine(Plugin.Instance.AssemblyFilePath, "youtube-dl.exe")))
                {
                    Logger.LogDebug("Downloading youtube-dl.exe");
                    Stream youtubeDL = await httpclient.GetStreamAsync("https://yt-dl.org/downloads/latest/youtube-dl.exe");
                    using (var fs = new FileStream(Path.Combine(Plugin.Instance.AssemblyFilePath, "youtube-dl.exe"), FileMode.CreateNew))
                    {
                        await youtubeDL.CopyToAsync(fs);
                    }
                }
                Plugin.Instance.Configuration.YoutubeDlFilePath = Path.Combine(Plugin.Instance.AssemblyFilePath, "youtube-dl.exe");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                if (!File.Exists(Path.Combine(Plugin.Instance.AssemblyFilePath, "youtube-dl")))
                {
                    Logger.LogDebug("Downloading youtube-dl");
                    Stream youtubeDL = await httpclient.GetStreamAsync("https://yt-dl.org/downloads/latest/youtube-dl");
                    using (var fs = new FileStream(Path.Combine(Plugin.Instance.AssemblyFilePath, "youtube-dl"), FileMode.CreateNew))
                    {
                        await youtubeDL.CopyToAsync(fs);
                    }
                    //dont belive this should give any output?
                    Plugin.Instance.Configuration.YoutubeDlFilePath = Path.Combine(Plugin.Instance.AssemblyFilePath, "youtube-dl");
                }
            }
            else
            {
                Logger.LogError("I NEED A MAC BEFORE I CAN IMPLEMENT SUPPORT FOR IT PLEASE COME WITH A DONATION IF YOU WISH FOR THIS");
                Logger.LogError("I NEED A MAC BEFORE I CAN IMPLEMENT SUPPORT FOR IT PLEASE COME WITH A DONATION IF YOU WISH FOR THIS");
                Logger.LogError("I NEED A MAC BEFORE I CAN IMPLEMENT SUPPORT FOR IT PLEASE COME WITH A DONATION IF YOU WISH FOR THIS");
                Logger.LogError("I NEED A MAC BEFORE I CAN IMPLEMENT SUPPORT FOR IT PLEASE COME WITH A DONATION IF YOU WISH FOR THIS");
            }
        }
    }

}
