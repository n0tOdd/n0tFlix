using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using n0tFlix.Plugin.TubiTV.Configuration;

namespace n0tFlix.Plugin.TubiTV
{
    /// <summary>
    /// The open subtitles plugin.
    /// </summary>
    public class TubiTv : BasePlugin<PluginConfiguration>, IHasWebPages
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenSubtitlesPlugin"/> class.
        /// </summary>
        /// <param name="applicationPaths">Instance of the <see cref="IApplicationPaths"/> interface.</param>
        /// <param name="xmlSerializer">Instance of the <see cref="IXmlSerializer"/> interface.</param>
        public TubiTv(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        /// <inheritdoc />
        public override string Name
            => this.GetType().Name;//We use the class name to make it more easy for us to generate new projects

        /// <inheritdoc />
        public override Guid Id
            => Guid.Parse("12b959d9-6931-4600-8ab5-899c2f023d58");


        public override string Description
            => "Gives you access to all the content from " + this.Configuration.BaseUrl;
        /// <summary>
        /// Gets the plugin instance.
        /// </summary>
        public static TubiTv? Instance { get; private set; }

        /// <inheritdoc />
        public override PluginInfo GetPluginInfo()
        {
            return new PluginInfo(Name, new Version(1, 0, 0, 0), "No description here", Id, true);
        }


        /// <inheritdoc />
        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = this.GetType().Name,
                    EmbeddedResourcePath = GetType().Namespace + ".Web." + this.GetType().Name + ".html",
                },
                new PluginPageInfo
                {
                    Name = this.GetType().Name + "js",
                    EmbeddedResourcePath = GetType().Namespace + ".Web." + this.GetType().Name + ".js"
                }
            };
        }
    }

}
