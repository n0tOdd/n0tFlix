
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace n0tFlix.Plugin.TubiTV.Models
{
    public class SeasonEpisodeInfo
    {
        public class Rating
        {
            [JsonPropertyNameName("system")]
            public string System { get; set; }

            [JsonPropertyNameName("value")]
            public string Value { get; set; }

            [JsonPropertyNameName("code")]
            public string Code { get; set; }
        }

        public class Images
        {
        }

        public class Awards
        {
            [JsonPropertyNameName("items")]
            public IList<object> Items { get; set; }
        }

        public class Rating2
        {
            [JsonPropertyNameName("system")]
            public string System { get; set; }

            [JsonPropertyNameName("value")]
            public string Value { get; set; }

            [JsonPropertyNameName("code")]
            public string Code { get; set; }
        }

        public class Images2
        {
        }

        public class Awards2
        {
            [JsonPropertyNameName("items")]
            public IList<object> Items { get; set; }
        }

        public class Manifest
        {
            [JsonPropertyNameName("url")]
            public string Url { get; set; }

            [JsonPropertyNameName("duration")]
            public int Duration { get; set; }
        }

        public class VideoResource
        {
            [JsonPropertyNameName("manifest")]
            public Manifest Manifest { get; set; }

            [JsonPropertyNameName("type")]
            public string Type { get; set; }
        }

        public class Subtitle
        {
            [JsonPropertyNameName("lang")]
            public string Lang { get; set; }

            [JsonPropertyNameName("url")]
            public string Url { get; set; }
        }

        public class CreditCuepoints
        {
            [JsonPropertyNameName("postlude")]
            public double Postlude { get; set; }

            [JsonPropertyNameName("prelogue")]
            public int Prelogue { get; set; }
        }

        public class Monetization
        {
            [JsonPropertyNameName("cue_points")]
            public IList<int> CuePoints { get; set; }
        }

        public class Child2
        {
            [JsonPropertyNameName("id")]
            public string Id { get; set; }

            [JsonPropertyNameName("type")]
            public string Type { get; set; }

            [JsonPropertyNameName("title")]
            public string Title { get; set; }

            [JsonPropertyNameName("tags")]
            public IList<string> Tags { get; set; }

            [JsonPropertyNameName("year")]
            public int Year { get; set; }

            [JsonPropertyNameName("description")]
            public string Description { get; set; }

            [JsonPropertyNameName("publisher_id")]
            public string PublisherId { get; set; }

            [JsonPropertyNameName("import_id")]
            public string ImportId { get; set; }

            [JsonPropertyNameName("ratings")]
            public IList<Rating2> Ratings { get; set; }

            [JsonPropertyNameName("actors")]
            public IList<string> Actors { get; set; }

            [JsonPropertyNameName("directors")]
            public IList<object> Directors { get; set; }

            [JsonPropertyNameName("availability_duration")]
            public int AvailabilityDuration { get; set; }

            [JsonPropertyNameName("images")]
            public Images2 Images { get; set; }

            [JsonPropertyNameName("has_subtitle")]
            public bool HasSubtitle { get; set; }

            [JsonPropertyNameName("imdb_id")]
            public object ImdbId { get; set; }

            [JsonPropertyNameName("partner_id")]
            public object PartnerId { get; set; }

            [JsonPropertyNameName("lang")]
            public string Lang { get; set; }

            [JsonPropertyNameName("country")]
            public string Country { get; set; }

            [JsonPropertyNameName("policy_match")]
            public bool PolicyMatch { get; set; }

            [JsonPropertyNameName("updated_at")]
            public string UpdatedAt { get; set; }

            [JsonPropertyNameName("awards")]
            public Awards2 Awards { get; set; }

            [JsonPropertyNameName("video_resources")]
            public IList<VideoResource> VideoResources { get; set; }

            [JsonPropertyNameName("posterarts")]
            public IList<object> Posterarts { get; set; }

            [JsonPropertyNameName("thumbnails")]
            public IList<string> Thumbnails { get; set; }

            [JsonPropertyNameName("hero_images")]
            public IList<object> HeroImages { get; set; }

            [JsonPropertyNameName("landscape_images")]
            public IList<object> LandscapeImages { get; set; }

            [JsonPropertyNameName("backgrounds")]
            public IList<string> Backgrounds { get; set; }

            [JsonPropertyNameName("gn_fields")]
            public object GnFields { get; set; }

            [JsonPropertyNameName("detailed_type")]
            public string DetailedType { get; set; }

            [JsonPropertyNameName("needs_login")]
            public bool NeedsLogin { get; set; }

            [JsonPropertyNameName("subtitles")]
            public IList<Subtitle> Subtitles { get; set; }

            [JsonPropertyNameName("has_trailer")]
            public bool HasTrailer { get; set; }

            [JsonPropertyNameName("canonical_id")]
            public string CanonicalId { get; set; }

            [JsonPropertyNameName("version_id")]
            public string VersionId { get; set; }

            [JsonPropertyNameName("duration")]
            public int Duration { get; set; }

            [JsonPropertyNameName("credit_cuepoints")]
            public CreditCuepoints CreditCuepoints { get; set; }

            [JsonPropertyNameName("url")]
            public string Url { get; set; }

            [JsonPropertyNameName("monetization")]
            public Monetization Monetization { get; set; }

            [JsonPropertyNameName("availability_starts")]
            public string AvailabilityStarts { get; set; }

            [JsonPropertyNameName("availability_ends")]
            public object AvailabilityEnds { get; set; }

            [JsonPropertyNameName("trailers")]
            public IList<object> Trailers { get; set; }

            [JsonPropertyNameName("series_id")]
            public string SeriesId { get; set; }
        }

        public class Child
        {
            [JsonPropertyNameName("id")]
            public string Id { get; set; }

            [JsonPropertyNameName("type")]
            public string Type { get; set; }

            [JsonPropertyNameName("title")]
            public string Title { get; set; }

            [JsonPropertyNameName("posterarts")]
            public IList<object> Posterarts { get; set; }

            [JsonPropertyNameName("children")]
            public IList<Child2> Children { get; set; }
        }

        public class root
        {
            [JsonPropertyNameName("id")]
            public string Id { get; set; }

            [JsonPropertyNameName("type")]
            public string Type { get; set; }

            [JsonPropertyNameName("title")]
            public string Title { get; set; }

            [JsonPropertyNameName("tags")]
            public IList<string> Tags { get; set; }

            [JsonPropertyNameName("year")]
            public int Year { get; set; }

            [JsonPropertyNameName("description")]
            public string Description { get; set; }

            [JsonPropertyNameName("publisher_id")]
            public string PublisherId { get; set; }

            [JsonPropertyNameName("import_id")]
            public string ImportId { get; set; }

            [JsonPropertyNameName("ratings")]
            public IList<Rating> Ratings { get; set; }

            [JsonPropertyNameName("actors")]
            public IList<string> Actors { get; set; }

            [JsonPropertyNameName("directors")]
            public IList<string> Directors { get; set; }

            [JsonPropertyNameName("availability_duration")]
            public object AvailabilityDuration { get; set; }

            [JsonPropertyNameName("images")]
            public Images Images { get; set; }

            [JsonPropertyNameName("has_subtitle")]
            public bool HasSubtitle { get; set; }

            [JsonPropertyNameName("imdb_id")]
            public string ImdbId { get; set; }

            [JsonPropertyNameName("partner_id")]
            public object PartnerId { get; set; }

            [JsonPropertyNameName("lang")]
            public string Lang { get; set; }

            [JsonPropertyNameName("country")]
            public string Country { get; set; }

            [JsonPropertyNameName("policy_match")]
            public bool PolicyMatch { get; set; }

            [JsonPropertyNameName("updated_at")]
            public string UpdatedAt { get; set; }

            [JsonPropertyNameName("awards")]
            public Awards Awards { get; set; }

            [JsonPropertyNameName("posterarts")]
            public IList<string> Posterarts { get; set; }

            [JsonPropertyNameName("thumbnails")]
            public IList<string> Thumbnails { get; set; }

            [JsonPropertyNameName("hero_images")]
            public IList<string> HeroImages { get; set; }

            [JsonPropertyNameName("landscape_images")]
            public IList<object> LandscapeImages { get; set; }

            [JsonPropertyNameName("backgrounds")]
            public IList<string> Backgrounds { get; set; }

            [JsonPropertyNameName("gn_fields")]
            public object GnFields { get; set; }

            [JsonPropertyNameName("detailed_type")]
            public string DetailedType { get; set; }

            [JsonPropertyNameName("needs_login")]
            public bool NeedsLogin { get; set; }

            [JsonPropertyNameName("subtitles")]
            public IList<object> Subtitles { get; set; }

            [JsonPropertyNameName("has_trailer")]
            public bool HasTrailer { get; set; }

            [JsonPropertyNameName("canonical_id")]
            public string CanonicalId { get; set; }

            [JsonPropertyNameName("version_id")]
            public string VersionId { get; set; }

            [JsonPropertyNameName("valid_duration")]
            public int ValidDuration { get; set; }

            [JsonPropertyNameName("is_recurring")]
            public bool IsRecurring { get; set; }

            [JsonPropertyNameName("children")]
            public IList<Child> Children { get; set; }
        }
    }
}