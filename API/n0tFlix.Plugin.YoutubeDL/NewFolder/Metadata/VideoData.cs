using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace n0tYoutubeDL.Core.Metadata
{
    /// <summary>
    /// Represents the video metadata for one video as extracted by youtube-dl.
    /// </summary>
    public class VideoData
    {
        [JsonPropertyName("_type")]
        public MetadataType ResultType { get; set; }
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("formats")]
        public FormatData[] Formats { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("ext")]
        public string Extension { get; set; }
        [JsonPropertyName("format")]
        public string Format { get; set; }
        [JsonPropertyName("player_url")]
        public string PlayerUrl { get; set; }
        [JsonPropertyName("extractor")]
        public string Extractor { get; set; }
        [JsonPropertyName("extractor_key")]
        public string ExtractorKey { get; set; }

        // If data refers to a playlist:
        [JsonPropertyName("entries")]
        public VideoData[] Entries { get; set; }
        // Additional optional fields:
        [JsonPropertyName("alt_title")]
        public string AltTitle { get; set; }
        [JsonPropertyName("display_id")]
        public string DisplayID { get; set; }
        [JsonPropertyName("thumbnails")]
        public ThumbnailData[] Thumbnails { get; set; }
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("uploader")]
        public string Uploader { get; set; }
        [JsonPropertyName("license")]
        public string License { get; set; }
        [JsonPropertyName("creator")]
        public string Creator { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
        [JsonPropertyName("upload_date")]
        public DateTime? UploadDate { get; set; }
        [JsonPropertyName("uploader_id")]
        public string UploaderID { get; set; }
        [JsonPropertyName("uploader_url")]
        public string UploaderUrl { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; }
        [JsonPropertyName("channel_id")]
        public string ChannelID { get; set; }
        [JsonPropertyName("channel_url")]
        public string ChannelUrl { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [JsonPropertyName("subtitles")]
        public Dictionary<string, SubtitleData[]> Subtitles { get; set; }
        [JsonPropertyName("duration")]
        public float? Duration { get; set; }
        [JsonPropertyName("view_count")]
        public long? ViewCount { get; set; }
        [JsonPropertyName("like_count")]
        public long? LikeCount { get; set; }
        [JsonPropertyName("dislike_count")]
        public long? DislikeCount { get; set; }
        [JsonPropertyName("repost_count")]
        public long? RepostCount { get; set; }
        [JsonPropertyName("average_rating")]
        public double? AverageRating { get; set; }
        [JsonPropertyName("comment_count")]
        public long? CommentCount { get; set; }
        [JsonPropertyName("age_limit")]
        public int? AgeLimit { get; set; }
        [JsonPropertyName("webpage_url")]
        public string WebpageUrl { get; set; }
        [JsonPropertyName("categories")]
        public string[] Categories { get; set; }
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
        [JsonPropertyName("is_live")]
        public bool? IsLive { get; set; }
        [JsonPropertyName("start_time")]
        public float? StartTime { get; set; }
        [JsonPropertyName("end_time")]
        public float? EndTime { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize<VideoData>(this);
        }
    }


}
