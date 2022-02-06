using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace n0tFlix.Plugin.NRK.Models
{
    internal class HeadlinersInfo
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

        public class Image
        {
            [JsonPropertyName("uri")]
            public string Uri { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class Self2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Series
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Seriespage
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Trailer
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links2
        {
            [JsonPropertyName("self")]
            public Self2 Self { get; set; }

            [JsonPropertyName("series")]
            public Series Series { get; set; }

            [JsonPropertyName("seriespage")]
            public Seriespage Seriespage { get; set; }

            [JsonPropertyName("trailer")]
            public Trailer Trailer { get; set; }
        }

        public class Headliner
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("subTitle")]
            public string SubTitle { get; set; }

            [JsonPropertyName("images")]
            public IList<Image> Images { get; set; }

            [JsonPropertyName("_links")]
            public Links2 Links { get; set; }
        }

        public class root
        {
            [JsonPropertyName("_links")]
            public Links Links { get; set; }

            [JsonPropertyName("headliners")]
            public IList<Headliner> Headliners { get; set; }
        }
    }
}