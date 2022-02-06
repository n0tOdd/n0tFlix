
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
            public string Href { get; set; }
        }

        public class Links
        {
            public Self Self { get; set; }
        }

        public class Self2
        {
            public string Href { get; set; }
        }

        public class AccessibilityVersion
        {
            public string Href { get; set; }
        }

        public class Links2
        {
            public Self2 Self { get; set; }

            public AccessibilityVersion AccessibilityVersion { get; set; }
        }

        public class WebImage
        {
            public string Uri { get; set; }

            public int Width { get; set; }
        }

        public class Image
        {
            public string Id { get; set; }

            public IList<WebImage> WebImages { get; set; }
        }

        public class PageListItem
        {
            public Links2 Links { get; set; }

            public string Id { get; set; }

            public string Title { get; set; }

            public Image Image { get; set; }
        }

        public class root
        {
            public Links Links { get; set; }

            public IList<PageListItem> PageListItems { get; set; }
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