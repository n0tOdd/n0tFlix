using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace n0tFlix.Plugin.NRK.Models
{
    public class PlayBackInfo
    {
        public class Self
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
        }

        public class Metadata
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class Links
        {
            [JsonPropertyName("self")]
            public Self Self { get; set; }

            [JsonPropertyName("metadata")]
            public Metadata Metadata { get; set; }
        }

        public class OnDemand
        {
            [JsonPropertyName("from")]
            public string From { get; set; }

            [JsonPropertyName("to")]
            public string To { get; set; }

            [JsonPropertyName("hasRightsNow")]
            public bool HasRightsNow { get; set; }
        }

        public class Availability
        {
            [JsonPropertyName("information")]
            public string Information { get; set; }

            [JsonPropertyName("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }

            [JsonPropertyName("onDemand")]
            public OnDemand OnDemand { get; set; }

            [JsonPropertyName("live")]
            public object Live { get; set; }

            [JsonPropertyName("externalEmbeddingAllowed")]
            public bool ExternalEmbeddingAllowed { get; set; }
        }

        public class Scores
        {
            [JsonPropertyName("springStreamSite")]
            public string SpringStreamSite { get; set; }

            [JsonPropertyName("springStreamStream")]
            public string SpringStreamStream { get; set; }

            [JsonPropertyName("springStreamContentType")]
            public string SpringStreamContentType { get; set; }

            [JsonPropertyName("springStreamProgramId")]
            public string SpringStreamProgramId { get; set; }

            [JsonPropertyName("springStreamDuration")]
            public int SpringStreamDuration { get; set; }
        }

        public class Ga
        {
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

            [JsonPropertyName("dimension29")]
            public string Dimension29 { get; set; }

            [JsonPropertyName("dimension36")]
            public string Dimension36 { get; set; }
        }

        public class Custom
        {
            [JsonPropertyName("contentId")]
            public string ContentId { get; set; }

            [JsonPropertyName("series")]
            public string Series { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("episode")]
            public string Episode { get; set; }

            [JsonPropertyName("category")]
            public string Category { get; set; }

            [JsonPropertyName("mediaType")]
            public string MediaType { get; set; }

            [JsonPropertyName("cdnName")]
            public string CdnName { get; set; }

            [JsonPropertyName("applicationVersion")]
            public string ApplicationVersion { get; set; }

            [JsonPropertyName("applicationName")]
            public string ApplicationName { get; set; }

            [JsonPropertyName("serviceName")]
            public string ServiceName { get; set; }

            [JsonPropertyName("contentType")]
            public string ContentType { get; set; }
        }

        public class Conviva
        {
            [JsonPropertyName("playerName")]
            public string PlayerName { get; set; }

            [JsonPropertyName("assetName")]
            public string AssetName { get; set; }

            [JsonPropertyName("duration")]
            public int Duration { get; set; }

            [JsonPropertyName("streamType")]
            public string StreamType { get; set; }

            [JsonPropertyName("streamUrl")]
            public string StreamUrl { get; set; }

            [JsonPropertyName("custom")]
            public Custom Custom { get; set; }
        }

        public class Config
        {
            [JsonPropertyName("beacon")]
            public string Beacon { get; set; }
        }

        public class Data
        {
            [JsonPropertyName("show")]
            public object Show { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("category")]
            public string Category { get; set; }

            [JsonPropertyName("contentLength")]
            public string ContentLength { get; set; }

            [JsonPropertyName("device")]
            public string Device { get; set; }

            [JsonPropertyName("playerId")]
            public string PlayerId { get; set; }

            [JsonPropertyName("deliveryType")]
            public string DeliveryType { get; set; }

            [JsonPropertyName("playerInfo")]
            public string PlayerInfo { get; set; }

            [JsonPropertyName("cdnName")]
            public string CdnName { get; set; }
        }

        public class Luna
        {
            [JsonPropertyName("config")]
            public Config Config { get; set; }

            [JsonPropertyName("data")]
            public Data Data { get; set; }
        }

        public class Statistics
        {
            [JsonPropertyName("scores")]
            public Scores Scores { get; set; }

            [JsonPropertyName("ga")]
            public Ga Ga { get; set; }

            [JsonPropertyName("conviva")]
            public Conviva Conviva { get; set; }

            [JsonPropertyName("luna")]
            public Luna Luna { get; set; }
        }

        public class Asset
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("format")]
            public string Format { get; set; }

            [JsonPropertyName("mimeType")]
            public string MimeType { get; set; }

            [JsonPropertyName("encrypted")]
            public bool Encrypted { get; set; }
        }

        public class Subtitle
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("language")]
            public string Language { get; set; }

            [JsonPropertyName("label")]
            public string Label { get; set; }

            [JsonPropertyName("defaultOn")]
            public bool DefaultOn { get; set; }

            [JsonPropertyName("webVtt")]
            public string WebVtt { get; set; }
        }

        public class Playable
        {
            [JsonPropertyName("endSequenceStartTime")]
            public object EndSequenceStartTime { get; set; }

            [JsonPropertyName("duration")]
            public string Duration { get; set; }

            [JsonPropertyName("assets")]
            public IList<Asset> Assets { get; set; }

            [JsonPropertyName("liveBuffer")]
            public object LiveBuffer { get; set; }

            [JsonPropertyName("subtitles")]
            public IList<Subtitle> Subtitles { get; set; }
        }

        public class root
        {
            [JsonPropertyName("_links")]
            public Links Links { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("playability")]
            public string Playability { get; set; }

            [JsonPropertyName("streamingMode")]
            public string StreamingMode { get; set; }

            [JsonPropertyName("availability")]
            public Availability Availability { get; set; }

            [JsonPropertyName("statistics")]
            public Statistics Statistics { get; set; }

            [JsonPropertyName("playable")]
            public Playable Playable { get; set; }

            [JsonPropertyName("nonPlayable")]
            public object NonPlayable { get; set; }

            [JsonPropertyName("displayAspectRatio")]
            public string DisplayAspectRatio { get; set; }

            [JsonPropertyName("sourceMedium")]
            public string SourceMedium { get; set; }
        }
    }
}