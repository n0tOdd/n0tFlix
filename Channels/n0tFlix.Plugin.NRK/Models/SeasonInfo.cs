using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    public class SeasonInfo
    {
        public class Self
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class HighlightedEpisode
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Userdata
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Season
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }
        }

        public class Favourite
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Share
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Fargerik
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Extramaterial
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links
        {
            [JsonProperty("self")]
            public Self Self { get; set; }

            [JsonProperty("highlightedEpisode")]
            public HighlightedEpisode HighlightedEpisode { get; set; }

            [JsonProperty("userdata")]
            public Userdata Userdata { get; set; }

            [JsonProperty("seasons")]
            public IList<Season> Seasons { get; set; }

            [JsonProperty("favourite")]
            public Favourite Favourite { get; set; }

            [JsonProperty("share")]
            public Share Share { get; set; }

            [JsonProperty("fargerik")]
            public Fargerik Fargerik { get; set; }

            [JsonProperty("extramaterial")]
            public Extramaterial Extramaterial { get; set; }
        }

        public class Titles
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("subtitle")]
            public string Subtitle { get; set; }
        }

        public class Category
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class Image
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class Sequential
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("urlFriendlySeriesId")]
            public string UrlFriendlySeriesId { get; set; }

            [JsonProperty("highlightedEpisode")]
            public string HighlightedEpisode { get; set; }

            [JsonProperty("titles")]
            public Titles Titles { get; set; }

            [JsonProperty("category")]
            public Category Category { get; set; }

            [JsonProperty("image")]
            public IList<Image> Image { get; set; }
        }

        public class Ga
        {
            [JsonProperty("dimension1")]
            public string Dimension1 { get; set; }

            [JsonProperty("dimension2")]
            public string Dimension2 { get; set; }
        }

        public class Self2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Series
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }
        }

        public class Progress
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Share2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
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

            [JsonProperty("progresses")]
            public IList<Progress> Progresses { get; set; }

            [JsonProperty("share")]
            public Share2 Share { get; set; }

            [JsonProperty("trailer")]
            public Trailer Trailer { get; set; }
        }

        public class Titles2
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("subtitle")]
            public object Subtitle { get; set; }
        }

        public class Image2
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class BackdropImage
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class PosterImage
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class Self3
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Playbackmetadata
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Playback
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Recommendations
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Share3
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Fargerik2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links3
        {
            [JsonProperty("self")]
            public Self3 Self { get; set; }

            [JsonProperty("playbackmetadata")]
            public Playbackmetadata Playbackmetadata { get; set; }

            [JsonProperty("playback")]
            public Playback Playback { get; set; }

            [JsonProperty("recommendations")]
            public Recommendations Recommendations { get; set; }

            [JsonProperty("share")]
            public Share3 Share { get; set; }

            [JsonProperty("fargerik")]
            public Fargerik2 Fargerik { get; set; }
        }

        public class Titles3
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("subtitle")]
            public string Subtitle { get; set; }
        }

        public class Details
        {
            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }

            [JsonProperty("accessibilityValue")]
            public string AccessibilityValue { get; set; }
        }

        public class Image3
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class From
        {
            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class To
        {
            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class GeoBlock
        {
            [JsonProperty("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class UsageRights
        {
            [JsonProperty("from")]
            public From From { get; set; }

            [JsonProperty("to")]
            public To To { get; set; }

            [JsonProperty("geoBlock")]
            public GeoBlock GeoBlock { get; set; }
        }

        public class Availability
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("hasLabel")]
            public bool HasLabel { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }
        }

        public class Rating
        {
            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }

            [JsonProperty("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class Body
        {
            [JsonProperty("rating")]
            public Rating Rating { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }
        }

        public class LegalAge
        {
            [JsonProperty("legalReference")]
            public string LegalReference { get; set; }

            [JsonProperty("body")]
            public Body Body { get; set; }
        }

        public class Self4
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links4
        {
            [JsonProperty("self")]
            public Self4 Self { get; set; }
        }

        public class Ga2
        {
            [JsonProperty("_links")]
            public Links4 Links { get; set; }

            [JsonProperty("dimension1")]
            public string Dimension1 { get; set; }

            [JsonProperty("dimension2")]
            public string Dimension2 { get; set; }

            [JsonProperty("dimension3")]
            public string Dimension3 { get; set; }

            [JsonProperty("dimension4")]
            public string Dimension4 { get; set; }

            [JsonProperty("dimension5")]
            public string Dimension5 { get; set; }

            [JsonProperty("dimension10")]
            public string Dimension10 { get; set; }

            [JsonProperty("dimension21")]
            public string Dimension21 { get; set; }

            [JsonProperty("dimension22")]
            public string Dimension22 { get; set; }

            [JsonProperty("dimension23")]
            public string Dimension23 { get; set; }

            [JsonProperty("dimension25")]
            public string Dimension25 { get; set; }

            [JsonProperty("dimension26")]
            public string Dimension26 { get; set; }
        }

        public class Embedded3
        {
            [JsonProperty("ga")]
            public Ga2 Ga { get; set; }
        }

        public class Badge
        {
            [JsonProperty("accessibilityValue")]
            public string AccessibilityValue { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class Episode
        {
            [JsonProperty("_links")]
            public Links3 Links { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("prfId")]
            public string PrfId { get; set; }

            [JsonProperty("titles")]
            public Titles3 Titles { get; set; }

            [JsonProperty("details")]
            public Details Details { get; set; }

            [JsonProperty("releaseDateOnDemand")]
            public string ReleaseDateOnDemand { get; set; }

            [JsonProperty("originalTitle")]
            public string OriginalTitle { get; set; }

            [JsonProperty("image")]
            public IList<Image3> Image { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("durationInSeconds")]
            public int DurationInSeconds { get; set; }

            [JsonProperty("usageRights")]
            public UsageRights UsageRights { get; set; }

            [JsonProperty("productionYear")]
            public int ProductionYear { get; set; }

            [JsonProperty("sequenceNumber")]
            public int SequenceNumber { get; set; }

            [JsonProperty("availability")]
            public Availability Availability { get; set; }

            [JsonProperty("legalAge")]
            public LegalAge LegalAge { get; set; }

            [JsonProperty("contributors")]
            public IList<object> Contributors { get; set; }

            [JsonProperty("_embedded")]
            public Embedded3 Embedded { get; set; }

            [JsonProperty("badge")]
            public Badge Badge { get; set; }
        }

        public class Embedded2
        {
            [JsonProperty("episodes")]
            public IList<Episode> Episodes { get; set; }
        }

        public class Season2
        {
            [JsonProperty("_links")]
            public Links2 Links { get; set; }

            [JsonProperty("seriesType")]
            public string SeriesType { get; set; }

            [JsonProperty("sequenceNumber")]
            public int SequenceNumber { get; set; }

            [JsonProperty("titles")]
            public Titles2 Titles { get; set; }

            [JsonProperty("image")]
            public IList<Image2> Image { get; set; }

            [JsonProperty("backdropImage")]
            public IList<BackdropImage> BackdropImage { get; set; }

            [JsonProperty("posterImage")]
            public IList<PosterImage> PosterImage { get; set; }

            [JsonProperty("hasAvailableEpisodes")]
            public bool HasAvailableEpisodes { get; set; }

            [JsonProperty("_embedded")]
            public Embedded2 Embedded { get; set; }
        }

        public class Self5
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Progress2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Links5
        {
            [JsonProperty("self")]
            public Self5 Self { get; set; }

            [JsonProperty("progresses")]
            public IList<Progress2> Progresses { get; set; }
        }

        public class Titles4
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("subtitle")]
            public object Subtitle { get; set; }
        }

        public class Image4
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class BackdropImage2
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class Self6
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Playbackmetadata2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Playback2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Recommendations2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Share4
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Fargerik3
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links6
        {
            [JsonProperty("self")]
            public Self6 Self { get; set; }

            [JsonProperty("playbackmetadata")]
            public Playbackmetadata2 Playbackmetadata { get; set; }

            [JsonProperty("playback")]
            public Playback2 Playback { get; set; }

            [JsonProperty("recommendations")]
            public Recommendations2 Recommendations { get; set; }

            [JsonProperty("share")]
            public Share4 Share { get; set; }

            [JsonProperty("fargerik")]
            public Fargerik3 Fargerik { get; set; }
        }

        public class Titles5
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("subtitle")]
            public string Subtitle { get; set; }
        }

        public class Details2
        {
            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }

            [JsonProperty("accessibilityValue")]
            public string AccessibilityValue { get; set; }
        }

        public class Image5
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class From2
        {
            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class To2
        {
            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class GeoBlock2
        {
            [JsonProperty("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }
        }

        public class UsageRights2
        {
            [JsonProperty("from")]
            public From2 From { get; set; }

            [JsonProperty("to")]
            public To2 To { get; set; }

            [JsonProperty("geoBlock")]
            public GeoBlock2 GeoBlock { get; set; }
        }

        public class Availability2
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("hasLabel")]
            public bool HasLabel { get; set; }

            [JsonProperty("label")]
            public object Label { get; set; }
        }

        public class Rating2
        {
            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("displayValue")]
            public string DisplayValue { get; set; }

            [JsonProperty("displayAge")]
            public string DisplayAge { get; set; }
        }

        public class Body2
        {
            [JsonProperty("rating")]
            public Rating2 Rating { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }
        }

        public class LegalAge2
        {
            [JsonProperty("legalReference")]
            public string LegalReference { get; set; }

            [JsonProperty("body")]
            public Body2 Body { get; set; }
        }

        public class Self7
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links7
        {
            [JsonProperty("self")]
            public Self7 Self { get; set; }
        }

        public class Ga3
        {
            [JsonProperty("_links")]
            public Links7 Links { get; set; }

            [JsonProperty("dimension1")]
            public string Dimension1 { get; set; }

            [JsonProperty("dimension2")]
            public string Dimension2 { get; set; }

            [JsonProperty("dimension3")]
            public string Dimension3 { get; set; }

            [JsonProperty("dimension4")]
            public string Dimension4 { get; set; }

            [JsonProperty("dimension5")]
            public string Dimension5 { get; set; }

            [JsonProperty("dimension10")]
            public string Dimension10 { get; set; }

            [JsonProperty("dimension21")]
            public string Dimension21 { get; set; }

            [JsonProperty("dimension22")]
            public string Dimension22 { get; set; }

            [JsonProperty("dimension23")]
            public string Dimension23 { get; set; }

            [JsonProperty("dimension25")]
            public string Dimension25 { get; set; }

            [JsonProperty("dimension26")]
            public string Dimension26 { get; set; }
        }

        public class Embedded5
        {
            [JsonProperty("ga")]
            public Ga3 Ga { get; set; }
        }

        public class Episode2
        {
            [JsonProperty("_links")]
            public Links6 Links { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("prfId")]
            public string PrfId { get; set; }

            [JsonProperty("titles")]
            public Titles5 Titles { get; set; }

            [JsonProperty("details")]
            public Details2 Details { get; set; }

            [JsonProperty("releaseDateOnDemand")]
            public string ReleaseDateOnDemand { get; set; }

            [JsonProperty("originalTitle")]
            public object OriginalTitle { get; set; }

            [JsonProperty("image")]
            public IList<Image5> Image { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("durationInSeconds")]
            public int DurationInSeconds { get; set; }

            [JsonProperty("usageRights")]
            public UsageRights2 UsageRights { get; set; }

            [JsonProperty("productionYear")]
            public object ProductionYear { get; set; }

            [JsonProperty("sequenceNumber")]
            public object SequenceNumber { get; set; }

            [JsonProperty("availability")]
            public Availability2 Availability { get; set; }

            [JsonProperty("legalAge")]
            public LegalAge2 LegalAge { get; set; }

            [JsonProperty("contributors")]
            public IList<object> Contributors { get; set; }

            [JsonProperty("_embedded")]
            public Embedded5 Embedded { get; set; }
        }

        public class Embedded4
        {
            [JsonProperty("episodes")]
            public IList<Episode2> Episodes { get; set; }
        }

        public class ExtraMaterial2
        {
            [JsonProperty("_links")]
            public Links5 Links { get; set; }

            [JsonProperty("titles")]
            public Titles4 Titles { get; set; }

            [JsonProperty("seriesType")]
            public string SeriesType { get; set; }

            [JsonProperty("image")]
            public IList<Image4> Image { get; set; }

            [JsonProperty("backdropImage")]
            public IList<BackdropImage2> BackdropImage { get; set; }

            [JsonProperty("hasAvailableEpisodes")]
            public bool HasAvailableEpisodes { get; set; }

            [JsonProperty("_embedded")]
            public Embedded4 Embedded { get; set; }
        }

        public class Embedded
        {
            [JsonProperty("seasons")]
            public IList<Season2> Seasons { get; set; }

            [JsonProperty("extraMaterial")]
            public ExtraMaterial2 ExtraMaterial { get; set; }
        }

        public class root
        {
            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("seriesType")]
            public string SeriesType { get; set; }

            [JsonProperty("sequential")]
            public Sequential Sequential { get; set; }

            [JsonProperty("ga")]
            public Ga Ga { get; set; }

            [JsonProperty("_embedded")]
            public Embedded Embedded { get; set; }
        }
    }
}