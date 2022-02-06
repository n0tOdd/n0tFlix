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

        /// <summary>
        /// Default install path for python (i think this is only needed on linux/max maybe
        /// </summary>
        public string PythonPath { get; set; } = "/usr/bin/python3";
    }
}
