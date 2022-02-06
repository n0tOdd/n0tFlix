using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Plugin.NRK.Models
{
    public class PlayBackInfo
    {
        public class Self
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Metadata
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class Links
        {
            [JsonProperty("self")]
            public Self Self { get; set; }

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }
        }

        public class OnDemand
        {
            [JsonProperty("from")]
            public string From { get; set; }

            [JsonProperty("to")]
            public string To { get; set; }

            [JsonProperty("hasRightsNow")]
            public bool HasRightsNow { get; set; }
        }

        public class Availability
        {
            [JsonProperty("information")]
            public string Information { get; set; }

            [JsonProperty("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }

            [JsonProperty("onDemand")]
            public OnDemand OnDemand { get; set; }

            [JsonProperty("live")]
            public object Live { get; set; }

            [JsonProperty("externalEmbeddingAllowed")]
            public bool ExternalEmbeddingAllowed { get; set; }
        }

        public class Scores
        {
            [JsonProperty("springStreamSite")]
            public string SpringStreamSite { get; set; }

            [JsonProperty("springStreamStream")]
            public string SpringStreamStream { get; set; }

            [JsonProperty("springStreamContentType")]
            public string SpringStreamContentType { get; set; }

            [JsonProperty("springStreamProgramId")]
            public string SpringStreamProgramId { get; set; }

            [JsonProperty("springStreamDuration")]
            public int SpringStreamDuration { get; set; }
        }

        public class Ga
        {
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

            [JsonProperty("dimension29")]
            public string Dimension29 { get; set; }

            [JsonProperty("dimension36")]
            public string Dimension36 { get; set; }
        }

        public class Custom
        {
            [JsonProperty("contentId")]
            public string ContentId { get; set; }

            [JsonProperty("series")]
            public string Series { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("episode")]
            public string Episode { get; set; }

            [JsonProperty("category")]
            public string Category { get; set; }

            [JsonProperty("mediaType")]
            public string MediaType { get; set; }

            [JsonProperty("cdnName")]
            public string CdnName { get; set; }

            [JsonProperty("applicationVersion")]
            public string ApplicationVersion { get; set; }

            [JsonProperty("applicationName")]
            public string ApplicationName { get; set; }

            [JsonProperty("serviceName")]
            public string ServiceName { get; set; }

            [JsonProperty("contentType")]
            public string ContentType { get; set; }
        }

        public class Conviva
        {
            [JsonProperty("playerName")]
            public string PlayerName { get; set; }

            [JsonProperty("assetName")]
            public string AssetName { get; set; }

            [JsonProperty("duration")]
            public int Duration { get; set; }

            [JsonProperty("streamType")]
            public string StreamType { get; set; }

            [JsonProperty("streamUrl")]
            public string StreamUrl { get; set; }

            [JsonProperty("custom")]
            public Custom Custom { get; set; }
        }

        public class Config
        {
            [JsonProperty("beacon")]
            public string Beacon { get; set; }
        }

        public class Data
        {
            [JsonProperty("show")]
            public object Show { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("category")]
            public string Category { get; set; }

            [JsonProperty("contentLength")]
            public string ContentLength { get; set; }

            [JsonProperty("device")]
            public string Device { get; set; }

            [JsonProperty("playerId")]
            public string PlayerId { get; set; }

            [JsonProperty("deliveryType")]
            public string DeliveryType { get; set; }

            [JsonProperty("playerInfo")]
            public string PlayerInfo { get; set; }

            [JsonProperty("cdnName")]
            public string CdnName { get; set; }
        }

        public class Luna
        {
            [JsonProperty("config")]
            public Config Config { get; set; }

            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public class Statistics
        {
            [JsonProperty("scores")]
            public Scores Scores { get; set; }

            [JsonProperty("ga")]
            public Ga Ga { get; set; }

            [JsonProperty("conviva")]
            public Conviva Conviva { get; set; }

            [JsonProperty("luna")]
            public Luna Luna { get; set; }
        }

        public class Asset
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("format")]
            public string Format { get; set; }

            [JsonProperty("mimeType")]
            public string MimeType { get; set; }

            [JsonProperty("encrypted")]
            public bool Encrypted { get; set; }
        }

        public class Subtitle
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("language")]
            public string Language { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("defaultOn")]
            public bool DefaultOn { get; set; }

            [JsonProperty("webVtt")]
            public string WebVtt { get; set; }
        }

        public class Playable
        {
            [JsonProperty("endSequenceStartTime")]
            public object EndSequenceStartTime { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("assets")]
            public IList<Asset> Assets { get; set; }

            [JsonProperty("liveBuffer")]
            public object LiveBuffer { get; set; }

            [JsonProperty("subtitles")]
            public IList<Subtitle> Subtitles { get; set; }
        }

        public class root
        {
            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("playability")]
            public string Playability { get; set; }

            [JsonProperty("streamingMode")]
            public string StreamingMode { get; set; }

            [JsonProperty("availability")]
            public Availability Availability { get; set; }

            [JsonProperty("statistics")]
            public Statistics Statistics { get; set; }

            [JsonProperty("playable")]
            public Playable Playable { get; set; }

            [JsonProperty("nonPlayable")]
            public object NonPlayable { get; set; }

            [JsonProperty("displayAspectRatio")]
            public string DisplayAspectRatio { get; set; }

            [JsonProperty("sourceMedium")]
            public string SourceMedium { get; set; }
        }
    }
}