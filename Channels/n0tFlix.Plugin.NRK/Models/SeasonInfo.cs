using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace n0tFlix.Plugin.NRK.Models
{
    public class SeasonInfo
    {
        public class Self
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class HighlightedEpisode
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Userdata
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Season
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }
        }

        public class Favourite
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

        public class Fargerik
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Extramaterial
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links
        {
            [JsonPropertyName("self")]
            public Self Self { get; set; }

            [JsonPropertyName("highlightedEpisode")]
            public HighlightedEpisode HighlightedEpisode { get; set; }

            [JsonPropertyName("userdata")]
            public Userdata Userdata { get; set; }

            [JsonPropertyName("seasons")]
            public IList<Season> Seasons { get; set; }

            [JsonPropertyName("favourite")]
            public Favourite Favourite { get; set; }

            [JsonPropertyName("share")]
            public Share Share { get; set; }

            [JsonPropertyName("fargerik")]
            public Fargerik Fargerik { get; set; }

            [JsonPropertyName("extramaterial")]
            public Extramaterial Extramaterial { get; set; }
        }

        public class Titles
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("subtitle")]
            public string Subtitle { get; set; }
        }

        public class Category
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class Image
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class Sequential
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("urlFriendlySeriesId")]
            public string UrlFriendlySeriesId { get; set; }

            [JsonPropertyName("highlightedEpisode")]
            public string HighlightedEpisode { get; set; }

            [JsonPropertyName("titles")]
            public Titles Titles { get; set; }

            [JsonPropertyName("category")]
            public Category Category { get; set; }

            [JsonPropertyName("image")]
            public IList<Image> Image { get; set; }
        }

        public class Ga
        {
            [JsonPropertyName("dimension1")]
            public string Dimension1 { get; set; }

            [JsonPropertyName("dimension2")]
            public string Dimension2 { get; set; }
        }

        public class Self2
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

        public class Share2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
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

            [JsonPropertyName("progresses")]
            public IList<Progress> Progresses { get; set; }

            [JsonPropertyName("share")]
            public Share2 Share { get; set; }

            [JsonPropertyName("trailer")]
            public Trailer Trailer { get; set; }
        }

        public class Titles2
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("subtitle")]
            public object Subtitle { get; set; }
        }

        public class Image2
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

        public class Self3
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

        public class Share3
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Fargerik2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links3
        {
            [JsonPropertyName("self")]
            public Self3 Self { get; set; }

            [JsonPropertyName("playbackmetadata")]
            public Playbackmetadata Playbackmetadata { get; set; }

            [JsonPropertyName("playback")]
            public Playback Playback { get; set; }

            [JsonPropertyName("recommendations")]
            public Recommendations Recommendations { get; set; }

            [JsonPropertyName("share")]
            public Share3 Share { get; set; }

            [JsonPropertyName("fargerik")]
            public Fargerik2 Fargerik { get; set; }
        }

        public class Titles3
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

        public class Image3
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
            public string Label { get; set; }
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

        public class Self4
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links4
        {
            [JsonPropertyName("self")]
            public Self4 Self { get; set; }
        }

        public class Ga2
        {
            [JsonPropertyName("_links")]
            public Links4 Links { get; set; }

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

        public class Embedded3
        {
            [JsonPropertyName("ga")]
            public Ga2 Ga { get; set; }
        }

        public class Badge
        {
            [JsonPropertyName("accessibilityValue")]
            public string AccessibilityValue { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }
        }

        public class Episode
        {
            [JsonPropertyName("_links")]
            public Links3 Links { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("prfId")]
            public string PrfId { get; set; }

            [JsonPropertyName("titles")]
            public Titles3 Titles { get; set; }

            [JsonPropertyName("details")]
            public Details Details { get; set; }

            [JsonPropertyName("releaseDateOnDemand")]
            public string ReleaseDateOnDemand { get; set; }

            [JsonPropertyName("originalTitle")]
            public string OriginalTitle { get; set; }

            [JsonPropertyName("image")]
            public IList<Image3> Image { get; set; }

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
            public Embedded3 Embedded { get; set; }

            [JsonPropertyName("badge")]
            public Badge Badge { get; set; }
        }

        public class Embedded2
        {
            [JsonPropertyName("episodes")]
            public IList<Episode> Episodes { get; set; }
        }

        public class Season2
        {
            [JsonPropertyName("_links")]
            public Links2 Links { get; set; }

            [JsonPropertyName("seriesType")]
            public string SeriesType { get; set; }

            [JsonPropertyName("sequenceNumber")]
            public int SequenceNumber { get; set; }

            [JsonPropertyName("titles")]
            public Titles2 Titles { get; set; }

            [JsonPropertyName("image")]
            public IList<Image2> Image { get; set; }

            [JsonPropertyName("backdropImage")]
            public IList<BackdropImage> BackdropImage { get; set; }

            [JsonPropertyName("posterImage")]
            public IList<PosterImage> PosterImage { get; set; }

            [JsonPropertyName("hasAvailableEpisodes")]
            public bool HasAvailableEpisodes { get; set; }

            [JsonPropertyName("_embedded")]
            public Embedded2 Embedded { get; set; }
        }

        public class Self5
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Progress2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Links5
        {
            [JsonPropertyName("self")]
            public Self5 Self { get; set; }

            [JsonPropertyName("progresses")]
            public IList<Progress2> Progresses { get; set; }
        }

        public class Titles4
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("subtitle")]
            public object Subtitle { get; set; }
        }

        public class Image4
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class BackdropImage2
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class Self6
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Playbackmetadata2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Playback2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Recommendations2
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Share4
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("templated")]
            public bool Templated { get; set; }
        }

        public class Fargerik3
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links6
        {
            [JsonPropertyName("self")]
            public Self6 Self { get; set; }

            [JsonPropertyName("playbackmetadata")]
            public Playbackmetadata2 Playbackmetadata { get; set; }

            [JsonPropertyName("playback")]
            public Playback2 Playback { get; set; }

            [JsonPropertyName("recommendations")]
            public Recommendations2 Recommendations { get; set; }

            [JsonPropertyName("share")]
            public Share4 Share { get; set; }

            [JsonPropertyName("fargerik")]
            public Fargerik3 Fargerik { get; set; }
        }

        public class Titles5
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("subtitle")]
            public string Subtitle { get; set; }
        }

        public class Details2
        {
            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }

            [JsonPropertyName("accessibilityValue")]
            public string AccessibilityValue { get; set; }
        }

        public class Image5
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class From2
        {
            [JsonPropertyName("date")]
            public string Date { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class To2
        {
            [JsonPropertyName("date")]
            public string Date { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class GeoBlock2
        {
            [JsonPropertyName("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class UsageRights2
        {
            [JsonPropertyName("from")]
            public From2 From { get; set; }

            [JsonPropertyName("to")]
            public To2 To { get; set; }

            [JsonPropertyName("geoBlock")]
            public GeoBlock2 GeoBlock { get; set; }
        }

        public class Availability2
        {
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("hasLabel")]
            public bool HasLabel { get; set; }

            [JsonPropertyName("label")]
            public object Label { get; set; }
        }

        public class Rating2
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("displayValue")]
            public string DisplayValue { get; set; }

            [JsonPropertyName("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class Body2
        {
            [JsonPropertyName("rating")]
            public Rating2 Rating { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; }
        }

        public class LegalAge2
        {
            [JsonPropertyName("legalReference")]
            public string LegalReference { get; set; }

            [JsonPropertyName("body")]
            public Body2 Body { get; set; }
        }

        public class Self7
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Links7
        {
            [JsonPropertyName("self")]
            public Self7 Self { get; set; }
        }

        public class Ga3
        {
            [JsonPropertyName("_links")]
            public Links7 Links { get; set; }

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

        public class Embedded5
        {
            [JsonPropertyName("ga")]
            public Ga3 Ga { get; set; }
        }

        public class Episode2
        {
            [JsonPropertyName("_links")]
            public Links6 Links { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("prfId")]
            public string PrfId { get; set; }

            [JsonPropertyName("titles")]
            public Titles5 Titles { get; set; }

            [JsonPropertyName("details")]
            public Details2 Details { get; set; }

            [JsonPropertyName("releaseDateOnDemand")]
            public string ReleaseDateOnDemand { get; set; }

            [JsonPropertyName("originalTitle")]
            public object OriginalTitle { get; set; }

            [JsonPropertyName("image")]
            public IList<Image5> Image { get; set; }

            [JsonPropertyName("duration")]
            public string Duration { get; set; }

            [JsonPropertyName("durationInSeconds")]
            public int DurationInSeconds { get; set; }

            [JsonPropertyName("usageRights")]
            public UsageRights2 UsageRights { get; set; }

            [JsonPropertyName("productionYear")]
            public object ProductionYear { get; set; }

            [JsonPropertyName("sequenceNumber")]
            public object SequenceNumber { get; set; }

            [JsonPropertyName("availability")]
            public Availability2 Availability { get; set; }

            [JsonPropertyName("legalAge")]
            public LegalAge2 LegalAge { get; set; }

            [JsonPropertyName("contributors")]
            public IList<object> Contributors { get; set; }

            [JsonPropertyName("_embedded")]
            public Embedded5 Embedded { get; set; }
        }

        public class Embedded4
        {
            [JsonPropertyName("episodes")]
            public IList<Episode2> Episodes { get; set; }
        }

        public class ExtraMaterial2
        {
            [JsonPropertyName("_links")]
            public Links5 Links { get; set; }

            [JsonPropertyName("titles")]
            public Titles4 Titles { get; set; }

            [JsonPropertyName("seriesType")]
            public string SeriesType { get; set; }

            [JsonPropertyName("image")]
            public IList<Image4> Image { get; set; }

            [JsonPropertyName("backdropImage")]
            public IList<BackdropImage2> BackdropImage { get; set; }

            [JsonPropertyName("hasAvailableEpisodes")]
            public bool HasAvailableEpisodes { get; set; }

            [JsonPropertyName("_embedded")]
            public Embedded4 Embedded { get; set; }
        }

        public class Embedded
        {
            [JsonPropertyName("seasons")]
            public IList<Season2> Seasons { get; set; }

            [JsonPropertyName("extraMaterial")]
            public ExtraMaterial2 ExtraMaterial { get; set; }
        }

        public class root
        {
            [JsonPropertyName("_links")]
            public Links Links { get; set; }

            [JsonPropertyName("seriesType")]
            public string SeriesType { get; set; }

            [JsonPropertyName("sequential")]
            public Sequential Sequential { get; set; }

            [JsonPropertyName("ga")]
            public Ga Ga { get; set; }

            [JsonPropertyName("_embedded")]
            public Embedded Embedded { get; set; }
        }
    }
}