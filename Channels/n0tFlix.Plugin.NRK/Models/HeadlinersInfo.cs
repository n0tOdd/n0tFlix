using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    internal class HeadlinersInfo
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

        public class Image
        {
            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class Self2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Series
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Seriespage
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Trailer
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links2
        {
            [JsonProperty("self")]
            public Self2 Self { get; set; }

            [JsonProperty("series")]
            public Series Series { get; set; }

            [JsonProperty("seriespage")]
            public Seriespage Seriespage { get; set; }

            [JsonProperty("trailer")]
            public Trailer Trailer { get; set; }
        }

        public class Headliner
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("subTitle")]
            public string SubTitle { get; set; }

            [JsonProperty("images")]
            public IList<Image> Images { get; set; }

            [JsonProperty("_links")]
            public Links2 Links { get; set; }
        }

        public class root
        {
            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("headliners")]
            public IList<Headliner> Headliners { get; set; }
        }
    }
}