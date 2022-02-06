using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace n0tFlix.Plugin.NRK.Models
{
    public class CategoryItems
    {
        public class Self
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links
        {
            [JsonPropertyName("self")]
            public Self Self { get; set; }
        }

        public class WebImage
        {
            [JsonPropertyName("uri")]
            public string Uri { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class DisplayContractImage
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("webImages")]
            public IList<WebImage> WebImages { get; set; }
        }

        public class WebImage2
        {
            [JsonPropertyName("uri")]
            public string Uri { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class FallbackImage
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("webImages")]
            public IList<WebImage2> WebImages { get; set; }
        }

        public class DisplayContractContent
        {
            [JsonPropertyName("contentTitle")]
            public string ContentTitle { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("displayContractImage")]
            public DisplayContractImage DisplayContractImage { get; set; }

            [JsonPropertyName("fallbackImage")]
            public FallbackImage FallbackImage { get; set; }
        }

        public class Self2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Fargerik
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links2
        {
            [JsonPropertyName("self")]
            public Self2 Self { get; set; }

            [JsonPropertyName("fargerik")]
            public Fargerik Fargerik { get; set; }
        }

        public class LegalAge
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }

            [JsonPropertyName("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class UsageRights
        {
            [JsonPropertyName("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }
        }

        public class Series
        {
            [JsonPropertyName("_links")]
            public Links2 Links { get; set; }

            [JsonPropertyName("legalAge")]
            public LegalAge LegalAge { get; set; }

            [JsonPropertyName("usageRights")]
            public UsageRights UsageRights { get; set; }
        }

        public class Self3
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class MediaElement
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Series2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Season
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Playback
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links3
        {
            [JsonPropertyName("self")]
            public Self3 Self { get; set; }

            [JsonPropertyName("mediaElement")]
            public MediaElement MediaElement { get; set; }

            [JsonPropertyName("series")]
            public Series2 Series { get; set; }

            [JsonPropertyName("season")]
            public Season Season { get; set; }

            [JsonPropertyName("playback")]
            public Playback Playback { get; set; }
        }

        public class LegalAge2
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }

            [JsonPropertyName("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class UsageRights2
        {
            [JsonPropertyName("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }
        }

        public class Episode
        {
            [JsonPropertyName("_links")]
            public Links3 Links { get; set; }

            [JsonPropertyName("duration")]
            public string Duration { get; set; }

            [JsonPropertyName("legalAge")]
            public LegalAge2 LegalAge { get; set; }

            [JsonPropertyName("usageRights")]
            public UsageRights2 UsageRights { get; set; }
        }

        public class Self4
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class MediaElement2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Playback2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links4
        {
            [JsonPropertyName("self")]
            public Self4 Self { get; set; }

            [JsonPropertyName("mediaElement")]
            public MediaElement2 MediaElement { get; set; }

            [JsonPropertyName("playback")]
            public Playback2 Playback { get; set; }
        }

        public class LegalAge3
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }

            [JsonPropertyName("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class UsageRights3
        {
            [JsonPropertyName("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }
        }

        public class StandaloneProgram
        {
            [JsonPropertyName("_links")]
            public Links4 Links { get; set; }

            [JsonPropertyName("duration")]
            public string Duration { get; set; }

            [JsonPropertyName("legalAge")]
            public LegalAge3 LegalAge { get; set; }

            [JsonPropertyName("usageRights")]
            public UsageRights3 UsageRights { get; set; }
        }

        public class Plug
        {
            [JsonPropertyName("targetType")]
            public string TargetType { get; set; }

            [JsonPropertyName("displayContractContent")]
            public DisplayContractContent DisplayContractContent { get; set; }

            [JsonPropertyName("series")]
            public Series Series { get; set; }

            [JsonPropertyName("episode")]
            public Episode Episode { get; set; }

            [JsonPropertyName("standaloneProgram")]
            public StandaloneProgram StandaloneProgram { get; set; }
        }

        public class Included
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("count")]
            public int Count { get; set; }

            [JsonPropertyName("displayContract")]
            public string DisplayContract { get; set; }

            [JsonPropertyName("plugs")]
            public IList<Plug> Plugs { get; set; }
        }

        public class Section
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("included")]
            public Included Included { get; set; }
        }

        public class root
        {
            [JsonPropertyName("_links")]
            public Links Links { get; set; }

            [JsonPropertyName("publishedTime")]
            public string PublishedTime { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("sections")]
            public IList<Section> Sections { get; set; }
        }
    }
}