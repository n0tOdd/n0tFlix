using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    public class EpisodeInfo
    {
        public class Self
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

        public class Share
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Links
        {
            
            public Self Self { get; set; }

            
            public Series Series { get; set; }

            
            public IList<Progress> Progresses { get; set; }

            
            public Share Share { get; set; }
        }

        public class Titles
        {
            
            public string Title { get; set; }

            
            public object Subtitle { get; set; }
        }

        public class image
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class Backdropimage
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class Posterimage
        {
            
            public string Url { get; set; }

            
            public int Width { get; set; }
        }

        public class Self2
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

        public class Share2
        {
            
            public string Href { get; set; }

            
            public bool Templated { get; set; }
        }

        public class Fargerik
        {
            
            public string Href { get; set; }
        }

        public class Links2
        {
            
            public Self2 Self { get; set; }

            
            public Playbackmetadata Playbackmetadata { get; set; }

            
            public Playback Playback { get; set; }

            
            public Recommendations Recommendations { get; set; }

            
            public Share2 Share { get; set; }

            
            public Fargerik Fargerik { get; set; }
        }

        public class Titles2
        {
            
            public string Title { get; set; }

            
            public string Subtitle { get; set; }
        }

        public class Details
        {
            
            public string DisplayValue { get; set; }

            
            public string AccessibilityValue { get; set; }
        }

        public class image2
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

            
            public object Label { get; set; }
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

        public class Self3
        {
            
            public string Href { get; set; }
        }

        public class Links3
        {
            
            public Self3 Self { get; set; }
        }

        public class Ga
        {
            
            public Links3 Links { get; set; }

            
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

        public class Embedded2
        {
            
            public Ga Ga { get; set; }
        }

        public class Episode
        {
            
            public Links2 Links { get; set; }

            
            public string Id { get; set; }

            
            public string PrfId { get; set; }

            
            public Titles2 Titles { get; set; }

            
            public Details Details { get; set; }

            
            public string ReleaseDateOnDemand { get; set; }

            
            public string OriginalTitle { get; set; }

            
            public IList<image2> image { get; set; }

            
            public string Duration { get; set; }

            
            public int DurationInSeconds { get; set; }

            
            public UsageRights UsageRights { get; set; }

            
            public int ProductionYear { get; set; }

            
            public int SequenceNumber { get; set; }

            
            public Availability Availability { get; set; }

            
            public LegalAge LegalAge { get; set; }

            
            public IList<object> Contributors { get; set; }

            
            public Embedded2 Embedded { get; set; }
        }

        public class Embedded
        {
            
            public IList<Episode> Episodes { get; set; }
        }

        public class root
        {
            
            public Links Links { get; set; }

            
            public string SeriesType { get; set; }

            
            public int SequenceNumber { get; set; }

            
            public Titles Titles { get; set; }

            
            public IList<image> image { get; set; }

            
            public IList<Backdropimage> Backdropimage { get; set; }

            
            public IList<Posterimage> Posterimage { get; set; }

            
            public bool HasAvailableEpisodes { get; set; }

            
            public Embedded Embedded { get; set; }
        }
    }
}