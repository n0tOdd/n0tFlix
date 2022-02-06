using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    public class CategoryItems
    {
        public class Self
        {
            public string Href { get; set; }
        }

        public class Links
        {
            public Self Self { get; set; }
        }

        public class WebImage
        {
            public string Uri { get; set; }

            public int Width { get; set; }
        }

        public class DisplayContractImage
        {
            public string Id { get; set; }

            public IList<WebImage> WebImages { get; set; }
        }

        public class WebImage2
        {
            public string Uri { get; set; }

            public int Width { get; set; }
        }

        public class FallbackImage
        {
            public string Id { get; set; }

            public IList<WebImage2> WebImages { get; set; }
        }

        public class DisplayContractContent
        {
            public string ContentTitle { get; set; }

            public string Description { get; set; }

            public DisplayContractImage DisplayContractImage { get; set; }

            public FallbackImage FallbackImage { get; set; }
        }

        public class Self2
        {
            public string Href { get; set; }
        }

        public class Fargerik
        {
            public string Href { get; set; }
        }

        public class Links2
        {
            public Self2 Self { get; set; }

            public Fargerik Fargerik { get; set; }
        }

        public class LegalAge
        {
            public string Id { get; set; }

            public string DisplayValue { get; set; }

            public string DisplayAge { get; set; }
        }

        public class UsageRights
        {
            public bool IsGeoBlocked { get; set; }
        }

        public class Series
        {
            public Links2 Links { get; set; }

            public LegalAge LegalAge { get; set; }

            public UsageRights UsageRights { get; set; }
        }

        public class Self3
        {
            
            public string Href { get; set; }
        }

        public class MediaElement
        {
            
            public string Href { get; set; }
        }

        public class Series2
        {
            
            public string Href { get; set; }
        }

        public class Season
        {
            
            public string Href { get; set; }
        }

        public class Playback
        {
            
            public string Href { get; set; }
        }

        public class Links3
        {
            
            public Self3 Self { get; set; }

            
            public MediaElement MediaElement { get; set; }

            
            public Series2 Series { get; set; }

            
            public Season Season { get; set; }

            
            public Playback Playback { get; set; }
        }

        public class LegalAge2
        {
            
            public string Id { get; set; }

            
            public string DisplayValue { get; set; }

            
            public string DisplayAge { get; set; }
        }

        public class UsageRights2
        {
            
            public bool IsGeoBlocked { get; set; }
        }

        public class Episode
        {
            
            public Links3 Links { get; set; }

            
            public string Duration { get; set; }

            
            public LegalAge2 LegalAge { get; set; }

            
            public UsageRights2 UsageRights { get; set; }
        }

        public class Self4
        {
            
            public string Href { get; set; }
        }

        public class MediaElement2
        {
            
            public string Href { get; set; }
        }

        public class Playback2
        {
            
            public string Href { get; set; }
        }

        public class Links4
        {
            
            public Self4 Self { get; set; }

            
            public MediaElement2 MediaElement { get; set; }

            
            public Playback2 Playback { get; set; }
        }

        public class LegalAge3
        {
            
            public string Id { get; set; }

            
            public string DisplayValue { get; set; }

            
            public string DisplayAge { get; set; }
        }

        public class UsageRights3
        {
            
            public bool IsGeoBlocked { get; set; }
        }

        public class StandaloneProgram
        {
            
            public Links4 Links { get; set; }

            
            public string Duration { get; set; }

            
            public LegalAge3 LegalAge { get; set; }

            
            public UsageRights3 UsageRights { get; set; }
        }

        public class Plug
        {
            
            public string TargetType { get; set; }

            
            public DisplayContractContent DisplayContractContent { get; set; }

            
            public Series Series { get; set; }

            
            public Episode Episode { get; set; }

            
            public StandaloneProgram StandaloneProgram { get; set; }
        }

        public class Included
        {
            
            public string Title { get; set; }

            
            public int Count { get; set; }

            
            public string DisplayContract { get; set; }

            
            public IList<Plug> Plugs { get; set; }
        }

        public class Section
        {
            
            public string Type { get; set; }

            
            public Included Included { get; set; }
        }

        public class root
        {
            
            public Links Links { get; set; }

            
            public string PublishedTime { get; set; }

            
            public string Id { get; set; }

            
            public string Title { get; set; }

            
            public IList<Section> Sections { get; set; }
        }
    }
}