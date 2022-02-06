using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using n0tFlix.Channel.Name.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace n0tFlix.Channel.Name
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        /// <summary>
        /// Gets the plugin instance.
        /// </summary>
        public static Plugin Instance { get; private set; }

        #region Configuration Variables for the plugin, remember to update the version on upgrades

        /// <summary>
        /// The name of youre plugin, we are gonna use the same variable all over so you just need to edit it this once ;)
        /// </summary>
        public override string Name => GetType().Namespace.Split(".").Last();

        /// <summary>
        /// The Description of youre plugin, goin to be used by the manifestmanager later to keep the repository clrean
        /// </summary>
        public override string Description => "A Description of the channel here";

        /// <summary>
        /// Just added so we can share where more is to be found :P
        /// </summary>
        public string HomePageURL => "https://n0tprojects.com";

        /// <summary>
        /// The id of our plugin rememmber DONT run multiple plugins with same guid, its just trouble in the end
        /// use new-guid in powershell for å fresh value
        /// </summary>
        public override Guid Id => Guid.Parse("Add a guid");

        /// <summary>
        /// Only way i found to keep the Version value managed, if anybody finds a better way please tell me
        /// </summary>
        /// <returns></returns>
        public override PluginInfo GetPluginInfo()
        {
                     return new PluginInfo(this.Name, new Version(1, 0, 0, 0), this.Description, this.Id, true);
            {
                
                
                
                
                
                
                
            };
        }

        /// <summary>
        /// This one is to get youre html files for the config page or who know,
        /// </summary>
        /// <returns>returns some PluginPageInfo, maybe this can hack the channel interface?</returns>
        public IEnumerable<PluginPageInfo> GetPages()
        {
            List<PluginPageInfo> pluginPageInfos = new List<PluginPageInfo>();
            pluginPageInfos.Add(new PluginPageInfo()
            {
                Name = this.Name,
                DisplayName = this.Name,
                MenuSection = "n0tFlix",//<== create my own part of the meny, slowly im taking over all the jellyfin and netflix is born :P
                EmbeddedResourcePath = GetType().Namespace + ".Configuration.configPage.html",//todo add this bro, its suposed to be here
                EnableInMainMenu = false, //<== this makes it show up on youre dashboard menu, i was hoping public menu
                MenuIcon = "",
            });
            return pluginPageInfos;
        }

        #endregion Configuration Variables for the plugin, remember to update the version on upgrades

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
                     : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override void UpdateConfiguration(BasePluginConfiguration configuration)
        {
            base.UpdateConfiguration(configuration);
        }
    }
}