using System;
using System.Collections.Generic;
using System.Net.Http;

namespace nrk_serializer
{
    internal class Program
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

        public class WebImage
        {
            public string uri { get; set; }

            public int width { get; set; }
        }

        public class Image
        {
            public string id { get; set; }

            public IList<WebImage> webImages { get; set; }
        }

        public class PageListItem
        {
            public Links2 links { get; set; }

            public string id { get; set; }

            public string title { get; set; }

            public Image image { get; set; }
        }

        public class root
        {
            public Links links { get; set; }

            public IList<PageListItem> pageListItems { get; set; }
        }
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            string json = httpClient.GetStringAsync("https://psapi.nrk.no/tv/pages").Result;
            root root = System.Text.Json.JsonSerializer.Deserialize<root>(json);
            foreach(var ee in root.pageListItems)
            {

            }
        }
    }
}
