using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using n0tFlix.Plugin.Podnapisi.Configuration;

namespace n0tFlix.Plugin.Podnapisi
{
    /// <summary>
    /// The open subtitles plugin.
    /// </summary>
    public class Podnapisi : BasePlugin<PluginConfiguration>, IHasWebPages
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenSubtitlesPlugin"/> class.
        /// </summary>
        /// <param name="applicationPaths">Instance of the <see cref="IApplicationPaths"/> interface.</param>
        /// <param name="xmlSerializer">Instance of the <see cref="IXmlSerializer"/> interface.</param>
        public Podnapisi(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        /// <inheritdoc />
        public override string Name
            => this.GetType().Name;

        /// <inheritdoc />
        public override Guid Id
            => Guid.Parse("40d962d7-5a82-4ded-b201-1e8e15a6b1c7");

        /// <summary>
        /// Gets the plugin instance.
        /// </summary>
        public static Podnapisi? Instance { get; private set; }

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
