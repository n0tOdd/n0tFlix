using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    public class CategoryItems
    {
        public class Self
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links
        {
            [JsonProperty("self")]
            public Self Self { get; set; }
        }

        public class WebImage
        {
            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class DisplayContractImage
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("webImages")]
            public IList<WebImage> WebImages { get; set; }
        }

        public class WebImage2
        {
            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class FallbackImage
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("webImages")]
            public IList<WebImage2> WebImages { get; set; }
        }

        public class DisplayContractContent
        {
            [JsonProperty("contentTitle")]
            public string ContentTitle { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("displayContractImage")]
            public DisplayContractImage DisplayContractImage { get; set; }

            [JsonProperty("fallbackImage")]
            public FallbackImage FallbackImage { get; set; }
        }

        public class Self2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Fargerik
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links2
        {
            [JsonProperty("self")]
            public Self2 Self { get; set; }

            [JsonProperty("fargerik")]
            public Fargerik Fargerik { get; set; }
        }

        public class LegalAge
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }

            [JsonProperty("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class UsageRights
        {
            [JsonProperty("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }
        }

        public class Series
        {
            [JsonProperty("_links")]
            public Links2 Links { get; set; }

            [JsonProperty("legalAge")]
            public LegalAge LegalAge { get; set; }

            [JsonProperty("usageRights")]
            public UsageRights UsageRights { get; set; }
        }

        public class Self3
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class MediaElement
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Series2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Season
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Playback
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links3
        {
            [JsonProperty("self")]
            public Self3 Self { get; set; }

            [JsonProperty("mediaElement")]
            public MediaElement MediaElement { get; set; }

            [JsonProperty("series")]
            public Series2 Series { get; set; }

            [JsonProperty("season")]
            public Season Season { get; set; }

            [JsonProperty("playback")]
            public Playback Playback { get; set; }
        }

        public class LegalAge2
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }

            [JsonProperty("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class UsageRights2
        {
            [JsonProperty("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }
        }

        public class Episode
        {
            [JsonProperty("_links")]
            public Links3 Links { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("legalAge")]
            public LegalAge2 LegalAge { get; set; }

            [JsonProperty("usageRights")]
            public UsageRights2 UsageRights { get; set; }
        }

        public class Self4
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class MediaElement2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Playback2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links4
        {
            [JsonProperty("self")]
            public Self4 Self { get; set; }

            [JsonProperty("mediaElement")]
            public MediaElement2 MediaElement { get; set; }

            [JsonProperty("playback")]
            public Playback2 Playback { get; set; }
        }

        public class LegalAge3
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }

            [JsonProperty("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class UsageRights3
        {
            [JsonProperty("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }
        }

        public class StandaloneProgram
        {
            [JsonProperty("_links")]
            public Links4 Links { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("legalAge")]
            public LegalAge3 LegalAge { get; set; }

            [JsonProperty("usageRights")]
            public UsageRights3 UsageRights { get; set; }
        }

        public class Plug
        {
            [JsonProperty("targetType")]
            public string TargetType { get; set; }

            [JsonProperty("displayContractContent")]
            public DisplayContractContent DisplayContractContent { get; set; }

            [JsonProperty("series")]
            public Series Series { get; set; }

            [JsonProperty("episode")]
            public Episode Episode { get; set; }

            [JsonProperty("standaloneProgram")]
            public StandaloneProgram StandaloneProgram { get; set; }
        }

        public class Included
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("displayContract")]
            public string DisplayContract { get; set; }

            [JsonProperty("plugs")]
            public IList<Plug> Plugs { get; set; }
        }

        public class Section
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("included")]
            public Included Included { get; set; }
        }

        public class root
        {
            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("publishedTime")]
            public string PublishedTime { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("sections")]
            public IList<Section> Sections { get; set; }
        }
    }
}