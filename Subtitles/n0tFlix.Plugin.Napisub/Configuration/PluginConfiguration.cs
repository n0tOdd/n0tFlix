using MediaBrowser.Model.Plugins;

namespace n0tFlix.Plugin.NapiSub.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string GetNapiUrl { get; } = "http://napiprojekt.pl/api/api-napiprojekt3.php";
        public string GetUserAgent { get; } = "Mozilla/5.0";
        public string GetMode { get; } = "1";
        public string GetClientName { get; } = "NapiProjektPython";
        public string GetClientVer { get; } = "0.1";
        public string GetSubtitlesAsText { get; } = "1"; //1 = text, 0 = zip (zip password = "iBlm8NTigvru0Jr0")
    }
}
