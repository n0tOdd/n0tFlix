using System;
using System.Collections.Generic;

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickType
{
    public partial class Temperatures
    {
        [JsonPropertyName("containersHash")]
        public ContainersHash ContainersHash { get; set; }

        [JsonPropertyName("contents")]
        public Dictionary<string, Content> Contents { get; set; }
    }

    public partial class ContainersHash
    {
        [JsonPropertyName("comedy")]
        public Comedy Comedy { get; set; }
    }

    public partial class Comedy
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        [JsonPropertyName("children")]
        public string[] Children { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonPropertyName("cursor")]
        public long Cursor { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("backgrounds")]
        public string[] Backgrounds { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("sponsorship")]
        public object Sponsorship { get; set; }

        [JsonPropertyName("childType")]
        public string ChildType { get; set; }
    }

    public partial class Content
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonPropertyName("type")]
        public TypeEnum Type { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("ratings")]
        public Rating[] Ratings { get; set; }

        [JsonPropertyName("actors")]
        public string[] Actors { get; set; }

        [JsonPropertyName("directors")]
        public string[] Directors { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("year")]
        public long Year { get; set; }

        [JsonPropertyName("posterarts")]
        public Uri[] Posterarts { get; set; }

        [JsonPropertyName("thumbnails")]
        public Uri[] Thumbnails { get; set; }

        [JsonPropertyName("hero_images")]
        public Uri[] HeroImages { get; set; }

        [JsonPropertyName("landscape_images")]
        public object[] LandscapeImages { get; set; }

        [JsonPropertyName("backgrounds")]
        public Uri[] Backgrounds { get; set; }

        [JsonPropertyName("availability_duration")]
        public long? AvailabilityDuration { get; set; }

        [JsonPropertyName("publisher_id")]
        public string PublisherId { get; set; }

        [JsonPropertyName("has_trailer")]
        public bool HasTrailer { get; set; }

        [JsonPropertyName("has_subtitle")]
        public bool HasSubtitle { get; set; }

        [JsonPropertyName("gn_fields")]
        public object GnFields { get; set; }

        [JsonPropertyName("import_id")]
        public string ImportId { get; set; }

        [JsonPropertyName("availability_starts")]
        public DateTimeOffset? AvailabilityStarts { get; set; }

        [JsonPropertyName("availability_ends")]
        public DateTimeOffset? AvailabilityEnds { get; set; }

        [JsonPropertyName("images")]
        public Images Images { get; set; }

        [JsonPropertyName("lang")]
        public Lang Lang { get; set; }

        [JsonPropertyName("is_cdc")]
        public bool IsCdc { get; set; }

        [JsonPropertyName("duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? Duration { get; set; }
    }

    public partial class Images
    {
    }

    public partial class Rating
    {
        [JsonPropertyName("system")]
        public SystemEnum System { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("descriptors")]
        public object[] Descriptors { get; set; }
    }

    public enum Lang { English, Spanish };

    public enum SystemEnum { Mpaa, Tvpg };

    public enum TypeEnum { S, V };

 }
}
