using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    public class PlayBackInfo
    {
        public class Self
        {
            
            public string Href { get; set; }
        }

        public class Metadata
        {
            
            public string Href { get; set; }

            
            public string Name { get; set; }
        }

        public class Links
        {
            
            public Self Self { get; set; }

            
            public Metadata Metadata { get; set; }
        }

        public class OnDemand
        {
            
            public string From { get; set; }

            
            public string To { get; set; }

            
            public bool HasRightsNow { get; set; }
        }

        public class Availability
        {
            
            public string Information { get; set; }

            
            public bool IsGeoBlocked { get; set; }

            
            public OnDemand OnDemand { get; set; }

            
            public object Live { get; set; }

            
            public bool ExternalEmbeddingAllowed { get; set; }
        }

        public class Scores
        {
            
            public string SpringStreamSite { get; set; }

            
            public string SpringStreamStream { get; set; }

            
            public string SpringStreamContentType { get; set; }

            
            public string SpringStreamProgramId { get; set; }

            
            public int SpringStreamDuration { get; set; }
        }

        public class Ga
        {
            
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

            
            public string Dimension29 { get; set; }

            
            public string Dimension36 { get; set; }
        }

        public class Custom
        {
            
            public string ContentId { get; set; }

            
            public string Series { get; set; }

            
            public string Title { get; set; }

            
            public string Episode { get; set; }

            
            public string Category { get; set; }

            
            public string MediaType { get; set; }

            
            public string CdnName { get; set; }

            
            public string ApplicationVersion { get; set; }

            
            public string ApplicationName { get; set; }

            
            public string ServiceName { get; set; }

            
            public string ContentType { get; set; }
        }

        public class Conviva
        {
            
            public string PlayerName { get; set; }

            
            public string AssetName { get; set; }

            
            public int Duration { get; set; }

            
            public string StreamType { get; set; }

            
            public string StreamUrl { get; set; }

            
            public Custom Custom { get; set; }
        }

        public class Config
        {
            
            public string Beacon { get; set; }
        }

        public class Data
        {
            
            public object Show { get; set; }

            
            public string Title { get; set; }

            
            public string Category { get; set; }

            
            public string ContentLength { get; set; }

            
            public string Device { get; set; }

            
            public string PlayerId { get; set; }

            
            public string DeliveryType { get; set; }

            
            public string PlayerInfo { get; set; }

            
            public string CdnName { get; set; }
        }

        public class Luna
        {
            
            public Config Config { get; set; }

            
            public Data Data { get; set; }
        }

        public class Statistics
        {
            
            public Scores Scores { get; set; }

            
            public Ga Ga { get; set; }

            
            public Conviva Conviva { get; set; }

            
            public Luna Luna { get; set; }
        }

        public class Asset
        {
            
            public string Url { get; set; }

            
            public string Format { get; set; }

            
            public string MimeType { get; set; }

            
            public bool Encrypted { get; set; }
        }

        public class Subtitle
        {
            
            public string Type { get; set; }

            
            public string Language { get; set; }

            
            public string Label { get; set; }

            
            public bool DefaultOn { get; set; }

            
            public string WebVtt { get; set; }
        }

        public class Playable
        {
            
            public object EndSequenceStartTime { get; set; }

            
            public string Duration { get; set; }

            
            public IList<Asset> Assets { get; set; }

            
            public object LiveBuffer { get; set; }

            
            public IList<Subtitle> Subtitles { get; set; }
        }

        public class root
        {
            
            public Links Links { get; set; }

            
            public string Id { get; set; }

            
            public string Playability { get; set; }

            
            public string StreamingMode { get; set; }

            
            public Availability Availability { get; set; }

            
            public Statistics Statistics { get; set; }

            
            public Playable Playable { get; set; }

            
            public object NonPlayable { get; set; }

            
            public string DisplayAspectRatio { get; set; }

            
            public string SourceMedium { get; set; }
        }
    }
}