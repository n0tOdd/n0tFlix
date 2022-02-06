
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
            public string href { get; set; }
        }

        public class Links
        {
            public Self self { get; set; }
        }

        public class Self2
        {
            public string href { get; set; }
        }

        public class AccessibilityVersion
        {
            public string href { get; set; }
        }

        public class Links2
        {
            public Self2 self { get; set; }

            public AccessibilityVersion accessibilityVersion { get; set; }
        }

        public class Webimage
        {
            public string uri { get; set; }

            public int width { get; set; }
        }

        public class image
        {
            public string id { get; set; }

            public IList<Webimage> webimages { get; set; }
        }

        public class PageListItem
        {
            public Links2 links { get; set; }

            public string id { get; set; }

            public string title { get; set; }

            public image image { get; set; }
        }

        public class root
        {
            public Links links { get; set; }

            public IList<PageListItem> pageListItems { get; set; }
        }

        public static async Task<root> GetRoot()
        {
            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://psapi.nrk.no/tv/pages");
            root root = System.Text.Json.JsonSerializer.Deserialize<root>(json);
            return root;
        }
    }
}