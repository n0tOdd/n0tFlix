using MediaBrowser.Model.Plugins;

namespace n0tFlix.Plugin.SubDivX.Configuration
{
    /// <summary>
    /// The plugin configuration.
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        public bool UseOriginalTitle { get; set; } = false;
        public bool ShowTitleInResult { get; set; } = true;
        public bool ShowUploaderInResult { get; set; } = true;
    }
}
