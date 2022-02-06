using MediaBrowser.Model.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Name.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        /// <summary>
        /// The baseurl for where this gannel gets its information from
        /// </summary>
        public string BaseUrl { get; set; } 
    }
}