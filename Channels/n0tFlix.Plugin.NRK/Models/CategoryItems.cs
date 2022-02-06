using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    public class CategoryItems
    {
        public class Self
        {
            public string href { get; set; }
        }

        public class Links
        {
            public Self self { get; set; }
        }

        public class Webimage
        {
            public string uri { get; set; }

            public int width { get; set; }
        }

        public class DisplayContractimage
        {
            public string id { get; set; }

            public IList<Webimage> webimages { get; set; }
        }

        public class Webimage2
        {
            public string uri { get; set; }

            public int width { get; set; }
        }

        public class Fallbackimage
        {
            public string id { get; set; }

            public IList<Webimage2> webimages { get; set; }
        }

        public class DisplayContractContent
        {
            public string contentTitle { get; set; }

            public string description { get; set; }

            public DisplayContractimage displayContractimage { get; set; }

            public Fallbackimage fallbackimage { get; set; }
        }

        public class Self2
        {
            public string href { get; set; }
        }

        public class Fargerik
        {
            public string href { get; set; }
        }

        public class Links2
        {
            public Self2 self { get; set; }

            public Fargerik fargerik { get; set; }
        }

        public class LegalAge
        {
            public string id { get; set; }

            public string displayValue { get; set; }

            public string displayAge { get; set; }
        }

        public class UsageRights
        {
            public bool isGeoBlocked { get; set; }
        }

        public class Series
        {
            public Links2 links { get; set; }

            public LegalAge legalAge { get; set; }

            public UsageRights usageRights { get; set; }
        }

        public class Self3
        {
            
            public string href { get; set; }
        }

        public class MediaElement
        {
            
            public string href { get; set; }
        }

        public class Series2
        {
            
            public string href { get; set; }
        }

        public class Season
        {
            
            public string href { get; set; }
        }

        public class Playback
        {
            
            public string href { get; set; }
        }

        public class Links3
        {
            
            public Self3 self { get; set; }

            
            public MediaElement mediaElement { get; set; }

            
            public Series2 series { get; set; }

            
            public Season season { get; set; }

            
            public Playback playback { get; set; }
        }

        public class LegalAge2
        {
            
            public string id { get; set; }

            
            public string displayValue { get; set; }

            
            public string displayAge { get; set; }
        }

        public class UsageRights2
        {
            
            public bool isGeoBlocked { get; set; }
        }

        public class Episode
        {
            
            public Links3 links { get; set; }

            
            public string duration { get; set; }

            
            public LegalAge2 legalAge { get; set; }

            
            public UsageRights2 usageRights { get; set; }
        }

        public class Self4
        {
            
            public string href { get; set; }
        }

        public class MediaElement2
        {
            
            public string href { get; set; }
        }

        public class Playback2
        {
            
            public string href { get; set; }
        }

        public class Links4
        {
            
            public Self4 self { get; set; }

            
            public MediaElement2 mediaElement { get; set; }

            
            public Playback2 playback { get; set; }
        }

        public class LegalAge3
        {
            
            public string id { get; set; }

            
            public string displayValue { get; set; }

            
            public string displayAge { get; set; }
        }

        public class UsageRights3
        {
            
            public bool isGeoBlocked { get; set; }
        }

        public class StandaloneProgram
        {
            
            public Links4 links { get; set; }

            
            public string duration { get; set; }

            
            public LegalAge3 legalAge { get; set; }

            
            public UsageRights3 usageRights { get; set; }
        }

        public class Plug
        {
            
            public string targetType { get; set; }

            
            public DisplayContractContent displayContractContent { get; set; }

            
            public Series series { get; set; }

            
            public Episode episode { get; set; }

            
            public StandaloneProgram standaloneProgram { get; set; }
        }

        public class Included
        {
            
            public string title { get; set; }

            
            public int count { get; set; }

            
            public string displayContract { get; set; }

            
            public IList<Plug> plugs { get; set; }
        }

        public class Section
        {
            
            public string type { get; set; }

            
            public Included included { get; set; }
        }

        public class root
        {
            
            public Links links { get; set; }

            
            public string publishedTime { get; set; }

            
            public string id { get; set; }

            
            public string sitle { get; set; }

            
            public IList<Section> sections { get; set; }
        }
    }
}