using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    public class SeasonInfo
    {
        public class Self
        {
            
            public string Href { get; set; }
        }

        public class HighlightedEpisode
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Userdata
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Season
        {
            
            public string Name { get; set; }

            
            public string Href { get; set; }

            
            public string Title { get; set; }
        }

        public class Favourite
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Share
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Fargerik
        {
            
            public string Href { get; set; }
        }

        public class Extramaterial
        {
            
            public string Href { get; set; }
        }

        public class Links
        {
            
            public Self Self { get; set; }

            
            public HighlightedEpisode HighlightedEpisode { get; set; }

            
            public Userdata Userdata { get; set; }

            
            public IList<Season> Seasons { get; set; }

            
            public Favourite Favourite { get; set; }

            
            public Share Share { get; set; }

            
            public Fargerik Fargerik { get; set; }

            
            public Extramaterial Extramaterial { get; set; }
        }

        public class Titles
        {
            
            public string Title { get; set; }

            
            public string Subtitle { get; set; }
        }

        public class Category
        {
            
            public string Id { get; set; }

            
            public string Name { get; set; }
        }

        public class Image
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class Sequential
        {
            
            public string Id { get; set; }

            
            public string UrlFriendlySeriesId { get; set; }

            
            public string HighlightedEpisode { get; set; }

            
            public Titles Titles { get; set; }

            
            public Category Category { get; set; }

            
            public IList<Image> Image { get; set; }
        }

        public class Ga
        {
            
            public string Dimension1 { get; set; }

            
            public string Dimension2 { get; set; }
        }

        public class Self2
        {
            
            public string Href { get; set; }
        }

        public class Series
        {
            
            public string Name { get; set; }

            
            public string Href { get; set; }

            
            public string Title { get; set; }
        }

        public class Progress
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Share2
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Trailer
        {
            
            public string Name { get; set; }

            
            public string Href { get; set; }
        }

        public class Links2
        {
            
            public Self2 Self { get; set; }

            
            public Series Series { get; set; }

            
            public IList<Progress> Progresses { get; set; }

            
            public Share2 Share { get; set; }

            
            public Trailer Trailer { get; set; }
        }

        public class Titles2
        {
            
            public string Title { get; set; }

            
            public object Subtitle { get; set; }
        }

        public class Image2
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class BackdropImage
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class PosterImage
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class Self3
        {
            
            public string Href { get; set; }
        }

        public class Playbackmetadata
        {
            
            public string Href { get; set; }
        }

        public class Playback
        {
            
            public string Href { get; set; }
        }

        public class Recommendations
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Share3
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Fargerik2
        {
            
            public string Href { get; set; }
        }

        public class Links3
        {
            
            public Self3 Self { get; set; }

            
            public Playbackmetadata Playbackmetadata { get; set; }

            
            public Playback Playback { get; set; }

            
            public Recommendations Recommendations { get; set; }

            
            public Share3 Share { get; set; }

            
            public Fargerik2 Fargerik { get; set; }
        }

        public class Titles3
        {
            
            public string Title { get; set; }

            
            public string Subtitle { get; set; }
        }

        public class Details
        {
            
            public string DisplayValue { get; set; }

            
            public string AccessibilityValue { get; set; }
        }

        public class Image3
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class From
        {
            
            public string Date { get; set; }

            
            public string DisplayValue { get; set; }
        }

        public class To
        {
            
            public string Date { get; set; }

            
            public string DisplayValue { get; set; }
        }

        public class GeoBlock
        {
            
            public bool IsGeoBlocked { get; set; }

            
            public string DisplayValue { get; set; }
        }

        public class UsageRights
        {
            
            public From From { get; set; }

            
            public To To { get; set; }

            
            public GeoBlock GeoBlock { get; set; }
        }

        public class Availability
        {
            
            public string Status { get; set; }

            
            public bool HasLabel { get; set; }

            
            public string Label { get; set; }
        }

        public class Rating
        {
            
            public string Code { get; set; }

            
            public string DisplayValue { get; set; }

            
            public string DisplayAge { get; set; }
        }

        public class Body
        {
            
            public Rating Rating { get; set; }

            
            public string Status { get; set; }
        }

        public class LegalAge
        {
            
            public string LegalReference { get; set; }

            
            public Body Body { get; set; }
        }

        public class Self4
        {
            
            public string Href { get; set; }
        }

        public class Links4
        {
            
            public Self4 Self { get; set; }
        }

        public class Ga2
        {
            
            public Links4 Links { get; set; }

            
            public string Dimension1 { get; set; }

            
            public string Dimension2 { get; set; }

            
            public string Dimension3 { get; set; }

            
            public string Dimension4 { get; set; }

            
            public string Dimension5 { get; set; }

            
            public string Dimension10 { get; set; }

            
            public string Dimension21 { get; set; }

            
            public string Dimension22 { get; set; }

            
            public string Dimension23 { get; set; }

            
            public string Dimension25 { get; set; }

            
            public string Dimension26 { get; set; }
        }

        public class Embedded3
        {
            
            public Ga2 Ga { get; set; }
        }

        public class Badge
        {
            
            public string AccessibilityValue { get; set; }

            
            public string Type { get; set; }
        }

        public class Episode
        {
            
            public Links3 Links { get; set; }

            
            public string Id { get; set; }

            
            public string PrfId { get; set; }

            
            public Titles3 Titles { get; set; }

            
            public Details Details { get; set; }

            
            public string ReleaseDateOnDemand { get; set; }

            
            public string OriginalTitle { get; set; }

            
            public IList<Image3> Image { get; set; }

            
            public string Duration { get; set; }

            
            public int DurationInSeconds { get; set; }

            
            public UsageRights UsageRights { get; set; }

            
            public int ProductionYear { get; set; }

            
            public int SequenceNumber { get; set; }

            
            public Availability Availability { get; set; }

            
            public LegalAge LegalAge { get; set; }

            
            public IList<object> Contributors { get; set; }

            
            public Embedded3 Embedded { get; set; }

            
            public Badge Badge { get; set; }
        }

        public class Embedded2
        {
            
            public IList<Episode> Episodes { get; set; }
        }

        public class Season2
        {
            
            public Links2 Links { get; set; }

            
            public string SeriesType { get; set; }

            
            public int SequenceNumber { get; set; }

            
            public Titles2 Titles { get; set; }

            
            public IList<Image2> Image { get; set; }

            
            public IList<BackdropImage> BackdropImage { get; set; }

            
            public IList<PosterImage> PosterImage { get; set; }

            
            public bool HasAvailableEpisodes { get; set; }

            
            public Embedded2 Embedded { get; set; }
        }

        public class Self5
        {
            
            public string Href { get; set; }
        }

        public class Progress2
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Links5
        {
            
            public Self5 Self { get; set; }

            
            public IList<Progress2> Progresses { get; set; }
        }

        public class Titles4
        {
            
            public string Title { get; set; }

            
            public object Subtitle { get; set; }
        }

        public class Image4
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class BackdropImage2
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class Self6
        {
            
            public string Href { get; set; }
        }

        public class Playbackmetadata2
        {
            
            public string Href { get; set; }
        }

        public class Playback2
        {
            
            public string Href { get; set; }
        }

        public class Recommendations2
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Share4
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Fargerik3
        {
            
            public string Href { get; set; }
        }

        public class Links6
        {
            
            public Self6 Self { get; set; }

            
            public Playbackmetadata2 Playbackmetadata { get; set; }

            
            public Playback2 Playback { get; set; }

            
            public Recommendations2 Recommendations { get; set; }

            
            public Share4 Share { get; set; }

            
            public Fargerik3 Fargerik { get; set; }
        }

        public class Titles5
        {
            
            public string Title { get; set; }

            
            public string Subtitle { get; set; }
        }

        public class Details2
        {
            
            public string DisplayValue { get; set; }

            
            public string AccessibilityValue { get; set; }
        }

        public class Image5
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class From2
        {
            
            public string Date { get; set; }

            
            public string DisplayValue { get; set; }
        }

        public class To2
        {
            
            public string Date { get; set; }

            
            public string DisplayValue { get; set; }
        }

        public class GeoBlock2
        {
            
            public bool IsGeoBlocked { get; set; }

            
            public string DisplayValue { get; set; }
        }

        public class UsageRights2
        {
            
            public From2 From { get; set; }

            
            public To2 To { get; set; }

            
            public GeoBlock2 GeoBlock { get; set; }
        }

        public class Availability2
        {
            
            public string Status { get; set; }

            
            public bool HasLabel { get; set; }

            
            public object Label { get; set; }
        }

        public class Rating2
        {
            
            public string Code { get; set; }

            
            public string DisplayValue { get; set; }

            
            public string DisplayAge { get; set; }
        }

        public class Body2
        {
            
            public Rating2 Rating { get; set; }

            
            public string Status { get; set; }
        }

        public class LegalAge2
        {
            
            public string LegalReference { get; set; }

            
            public Body2 Body { get; set; }
        }

        public class Self7
        {
            
            public string Href { get; set; }
        }

        public class Links7
        {
            
            public Self7 Self { get; set; }
        }

        public class Ga3
        {
            
            public Links7 Links { get; set; }

            
            public string Dimension1 { get; set; }

            
            public string Dimension2 { get; set; }

            
            public string Dimension3 { get; set; }

            
            public string Dimension4 { get; set; }

            
            public string Dimension5 { get; set; }

            
            public string Dimension10 { get; set; }

            
            public string Dimension21 { get; set; }

            
            public string Dimension22 { get; set; }

            
            public string Dimension23 { get; set; }

            
            public string Dimension25 { get; set; }

            
            public string Dimension26 { get; set; }
        }

        public class Embedded5
        {
            
            public Ga3 Ga { get; set; }
        }

        public class Episode2
        {
            
            public Links6 Links { get; set; }

            
            public string Id { get; set; }

            
            public string PrfId { get; set; }

            
            public Titles5 Titles { get; set; }

            
            public Details2 Details { get; set; }

            
            public string ReleaseDateOnDemand { get; set; }

            
            public object OriginalTitle { get; set; }

            
            public IList<Image5> Image { get; set; }

            
            public string Duration { get; set; }

            
            public int DurationInSeconds { get; set; }

            
            public UsageRights2 UsageRights { get; set; }

            
            public object ProductionYear { get; set; }

            
            public object SequenceNumber { get; set; }

            
            public Availability2 Availability { get; set; }

            
            public LegalAge2 LegalAge { get; set; }

            
            public IList<object> Contributors { get; set; }

            
            public Embedded5 Embedded { get; set; }
        }

        public class Embedded4
        {
            
            public IList<Episode2> Episodes { get; set; }
        }

        public class ExtraMaterial2
        {
            
            public Links5 Links { get; set; }

            
            public Titles4 Titles { get; set; }

            
            public string SeriesType { get; set; }

            
            public IList<Image4> Image { get; set; }

            
            public IList<BackdropImage2> BackdropImage { get; set; }

            
            public bool HasAvailableEpisodes { get; set; }

            
            public Embedded4 Embedded { get; set; }
        }

        public class Embedded
        {
            
            public IList<Season2> Seasons { get; set; }

            
            public ExtraMaterial2 ExtraMaterial { get; set; }
        }

        public class root
        {
            
            public Links Links { get; set; }

            
            public string SeriesType { get; set; }

            
            public Sequential Sequential { get; set; }

            
            public Ga Ga { get; set; }

            
            public Embedded Embedded { get; set; }
        }
    }
}