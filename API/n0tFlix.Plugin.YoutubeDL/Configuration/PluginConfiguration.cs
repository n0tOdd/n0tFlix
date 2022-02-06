using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaBrowser.Model.Plugins;

namespace n0tFlix.Plugin.YoutubeDL.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {

        public string YoutubeDlFilePath { get; set; } = string.Empty;
    }
}
