using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace n0tFlix.Plugin.NRK.Models
{
    public class Categories
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

        public class Self2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class AccessibilityVersion
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links2
        {
            [JsonPropertyName("self")]
            public Self2 Self { get; set; }

            [JsonPropertyName("accessibilityVersion")]
            public AccessibilityVersion AccessibilityVersion { get; set; }
        }

        public class WebImage
        {
            [JsonPropertyName("uri")]
            public string Uri { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class Image
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("webImages")]
            public IList<WebImage> WebImages { get; set; }
        }

        public class PageListItem
        {
            [JsonPropertyName("_links")]
            public Links2 Links { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("image")]
            public Image Image { get; set; }
        }

        public class root
        {
            [JsonPropertyName("_links")]
            public Links Links { get; set; }

            [JsonPropertyName("pageListItems")]
            public IList<PageListItem> PageListItems { get; set; }
        }

        
    }
}