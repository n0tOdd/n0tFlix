using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Plugin.NRK.Models
{
    public class Categories
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

        public class Self2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class AccessibilityVersion
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links2
        {
            [JsonProperty("self")]
            public Self2 Self { get; set; }

            [JsonProperty("accessibilityVersion")]
            public AccessibilityVersion AccessibilityVersion { get; set; }
        }

        public class WebImage
        {
            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class Image
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("webImages")]
            public IList<WebImage> WebImages { get; set; }
        }

        public class PageListItem
        {
            [JsonProperty("_links")]
            public Links2 Links { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("image")]
            public Image Image { get; set; }
        }

        public class root
        {
            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("pageListItems")]
            public IList<PageListItem> PageListItems { get; set; }
        }

        public static async Task<root> GetRoot()
        {
            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://psapi.nrk.no/tv/pages");
            root root = Newtonsoft.Json.JsonConvert.DeserializeObject<root>(json);
            return root;
        }
    }
}