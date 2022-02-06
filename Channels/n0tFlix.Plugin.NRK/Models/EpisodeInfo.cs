using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace n0tFlix.Plugin.NRK.Models
{
    public class EpisodeInfo
    {
        public class Self
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Series
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }
        }

        public class Progress
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Share
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Links
        {
            [JsonPropertyName("self")]
            public Self Self { get; set; }

            [JsonPropertyName("series")]
            public Series Series { get; set; }

            [JsonPropertyName("progresses")]
            public IList<Progress> Progresses { get; set; }

            [JsonPropertyName("share")]
            public Share Share { get; set; }
        }

        public class Titles
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("subtitle")]
            public object Subtitle { get; set; }
        }

        public class Image
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class BackdropImage
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class PosterImage
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class Self2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Playbackmetadata
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Playback
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Recommendations
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Share2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
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

            [JsonPropertyName("playbackmetadata")]
            public Playbackmetadata Playbackmetadata { get; set; }

            [JsonPropertyName("playback")]
            public Playback Playback { get; set; }

            [JsonPropertyName("recommendations")]
            public Recommendations Recommendations { get; set; }

            [JsonPropertyName("share")]
            public Share2 Share { get; set; }

            [JsonPropertyName("fargerik")]
            public Fargerik Fargerik { get; set; }
        }

        public class Titles2
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("subtitle")]
            public string Subtitle { get; set; }
        }

        public class Details
        {
            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }

            [JsonPropertyName("accessibilityValue")]
            public string AccessibilityValue { get; set; }
        }

        public class Image2
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class From
        {
            [JsonPropertyName("date")]
            public string Date { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class To
        {
            [JsonPropertyName("date")]
            public string Date { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class GeoBlock
        {
            [JsonPropertyName("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class UsageRights
        {
            [JsonPropertyName("from")]
            public From From { get; set; }

            [JsonPropertyName("to")]
            public To To { get; set; }

            [JsonPropertyName("geoBlock")]
            public GeoBlock GeoBlock { get; set; }
        }

        public class Availability
        {
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("hasLabel")]
            public bool HasLabel { get; set; }

            [JsonPropertyName("label")]
            public object Label { get; set; }
        }

        public class Rating
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }

            [JsonPropertyName("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class Body
        {
            [JsonPropertyName("rating")]
            public Rating Rating { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; }
        }

        public class LegalAge
        {
            [JsonPropertyName("legalReference")]
            public string LegalReference { get; set; }

            [JsonPropertyName("body")]
            public Body Body { get; set; }
        }

        public class Self3
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links3
        {
            [JsonPropertyName("self")]
            public Self3 Self { get; set; }
        }

        public class Ga
        {
            [JsonPropertyName("_links")]
            public Links3 Links { get; set; }

            [JsonPropertyName("dimension1")]
            public string Dimension1 { get; set; }

            [JsonPropertyName("dimension2")]
            public string Dimension2 { get; set; }

            [JsonPropertyName("dimension3")]
            public string Dimension3 { get; set; }

            [JsonPropertyName("dimension4")]
            public string Dimension4 { get; set; }

            [JsonPropertyName("dimension5")]
            public string Dimension5 { get; set; }

            [JsonPropertyName("dimension10")]
            public string Dimension10 { get; set; }

            [JsonPropertyName("dimension21")]
            public string Dimension21 { get; set; }

            [JsonPropertyName("dimension22")]
            public string Dimension22 { get; set; }

            [JsonPropertyName("dimension23")]
            public string Dimension23 { get; set; }

            [JsonPropertyName("dimension25")]
            public string Dimension25 { get; set; }

            [JsonPropertyName("dimension26")]
            public string Dimension26 { get; set; }
        }

        public class Embedded2
        {
            [JsonPropertyName("ga")]
            public Ga Ga { get; set; }
        }

        public class Episode
        {
            [JsonPropertyName("_links")]
            public Links2 Links { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("prfId")]
            public string PrfId { get; set; }

            [JsonPropertyName("titles")]
            public Titles2 Titles { get; set; }

            [JsonPropertyName("details")]
            public Details Details { get; set; }

            [JsonPropertyName("releaseDateOnDemand")]
            public string ReleaseDateOnDemand { get; set; }

            [JsonPropertyName("originalTitle")]
            public string OriginalTitle { get; set; }

            [JsonPropertyName("image")]
            public IList<Image2> Image { get; set; }

            [JsonPropertyName("duration")]
            public string Duration { get; set; }

            [JsonPropertyName("durationInSeconds")]
            public int DurationInSeconds { get; set; }

            [JsonPropertyName("usageRights")]
            public UsageRights UsageRights { get; set; }

            [JsonPropertyName("productionYear")]
            public int ProductionYear { get; set; }

            [JsonPropertyName("sequenceNumber")]
            public int SequenceNumber { get; set; }

            [JsonPropertyName("availability")]
            public Availability Availability { get; set; }

            [JsonPropertyName("legalAge")]
            public LegalAge LegalAge { get; set; }

            [JsonPropertyName("contributors")]
            public IList<object> Contributors { get; set; }

            [JsonPropertyName("_embedded")]
            public Embedded2 Embedded { get; set; }
        }

        public class Embedded
        {
            [JsonPropertyName("episodes")]
            public IList<Episode> Episodes { get; set; }
        }

        public class root
        {
            [JsonPropertyName("_links")]
            public Links Links { get; set; }

            [JsonPropertyName("seriesType")]
            public string SeriesType { get; set; }

            [JsonPropertyName("sequenceNumber")]
            public int SequenceNumber { get; set; }

            [JsonPropertyName("titles")]
            public Titles Titles { get; set; }

            [JsonPropertyName("image")]
            public IList<Image> Image { get; set; }

            [JsonPropertyName("backdropImage")]
            public IList<BackdropImage> BackdropImage { get; set; }

            [JsonPropertyName("posterImage")]
            public IList<PosterImage> PosterImage { get; set; }

            [JsonPropertyName("hasAvailableEpisodes")]
            public bool HasAvailableEpisodes { get; set; }

            [JsonPropertyName("_embedded")]
            public Embedded Embedded { get; set; }
        }
    }
}