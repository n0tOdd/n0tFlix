using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaBrowser.Model.Plugins;

namespace n0tFlix.Plugin.TubiTV.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        /// <summary>
        /// The baseurl for where this gannel gets its information from
        /// </summary>
        public string BaseUrl { get; set; } = "https://tubitv.com";

        /// <summary>
        /// What category we should put this channel in
        /// </summary>
        public string ChannelCategory { get; set; } = "Multi Genre";
    }
}
