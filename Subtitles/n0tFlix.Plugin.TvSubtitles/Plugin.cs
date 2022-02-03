using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using n0tFlix.Plugin.TvSubtitles.Configuration;

namespace n0tFlix.Plugin.TvSubtitles
{
    /// <summary>
    /// The open subtitles plugin.
    /// </summary>
    public class TvSubtitles : BasePlugin<PluginConfiguration>, IHasWebPages
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TvSubtitles"/> class.
        /// </summary>
        /// <param name="applicationPaths">Instance of the <see cref="IApplicationPaths"/> interface.</param>
        /// <param name="xmlSerializer">Instance of the <see cref="IXmlSerializer"/> interface.</param>
        public TvSubtitles(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        /// <inheritdoc />
        public override string Name
            => this.GetType().Name;

        /// <inheritdoc />
        public override Guid Id
            => Guid.Parse("cabbd003-68c8-43f4-a6a1-4418da64bb73");

        /// <summary>
        /// Gets the plugin instance.
        /// </summary>
        public static TvSubtitles? Instance { get; private set; }

        /// <inheritdoc />
        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = this.GetType().Name,
                    EmbeddedResourcePath = GetType().Namespace + ".Web.TvSubtitles.html",
                },
                new PluginPageInfo
                {
                    Name = this.GetType().Name + "js",
                    EmbeddedResourcePath = GetType().Namespace + ".Web.TvSubtitles.js"
                }
            };
        }
    }

}
