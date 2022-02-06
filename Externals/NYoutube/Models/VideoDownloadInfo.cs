// Copyright 2021 Brian Allred
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

namespace NYoutubeDL.Models
{
    #region Using

    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///     Class holding information about a video
    /// </summary>
    public class VideoDownloadInfo : DownloadInfo
    {
        public VideoDownloadInfo(VideoInfo info)
        {
            if (info == null)
            {
                this.Title = "Video deleted or otherwise unreachable";
                return;
            }

            this.Abr = info.abr;
            this.Acodec = info.acodec;
            this.AgeLimit = info.age_limit;
            this.AverageRating = info.average_rating;
            this.Categories = info.categories;
            this.Description = info.description;
            this.DislikeCount = info.dislike_count;
            this.DisplayId = info.display_id;
            this.Duration = info.duration;
            this.Ext = info.ext;
            this.Extractor = info.extractor;
            this.ExtractorKey = info.extractor_key;
            this.Format = info.format;
            this.FormatId = info.format_id;

            if (info.formats != null)
            {
                foreach (FormatInfo formatInfo in info.formats)
                {
                    this.Formats.Add(new FormatDownloadInfo(formatInfo));
                }
            }

            this.Fps = info.fps;
            this.Height = info.height;
            this.Id = info.id;
            this.IeKey = info.ie_key;
            this.License = info.license;
            this.LikeCount = info.like_count;
            this.NEntries = info.n_entries;
            this.Playlist = info.playlist;
            this.PlaylistId = info.playlist_id;
            this.PlaylistIndex = info.playlist_index;
            this.PlaylistTitle = info.playlist_title;

            if (info.requested_formats != null)
            {
                foreach (FormatInfo formatInfo in info.requested_formats)
                {
                    this.RequestedFormats.Add(new FormatDownloadInfo(formatInfo));
                }
            }

            this.Tags = info.tags;
            this.Thumbnail = info.thumbnail;

            if (info.thumbnails != null)
            {
                foreach (ThumbnailInfo thumbnail in info.thumbnails)
                {
                    this.Thumbnails.Add(new ThumbnailDownloadInfo(thumbnail));
                }
            }

            this.Title = info.title;
            this.UploadDate = info.upload_date;
            this.Uploader = info.uploader;
            this.UploaderId = info.uploader_id;
            this.UploaderUrl = info.uploader_url;
            this.Url = info.url;
            this.Vcodec = info.vcodec;
            this.ViewCount = info.view_count;
            this.WebpageUrl = info.webpage_url;
            this.WebpageUrlBasename = info.webpage_url_basename;
            this.Width = info.width;
        }

        public string Acodec { get; }

        public float? Abr { get; }

        public int? AgeLimit { get; }

        public double? AverageRating { get; }

        public List<string> Categories { get; }

        public string Description { get; }

        public long? DislikeCount { get; }

        public string DisplayId { get; }

        public double? Duration { get; }

        public string Ext { get; set; }

        public string Extractor { get; set; }

        public string ExtractorKey { get; set; }

        public string Format { get; }

        public string FormatId { get; }

        public List<FormatDownloadInfo> Formats { get; } = new List<FormatDownloadInfo>();

        public double? Fps { get; }

        public int? Height { get; }

        public string IeKey { get; }

        public string License { get; }

        public long? LikeCount { get; }

        public int? NEntries { get; }

        public string Playlist { get; }

        public string PlaylistId { get; }

        public int? PlaylistIndex { get; }

        public string PlaylistTitle { get; }

        public List<FormatDownloadInfo> RequestedFormats { get; } = new List<FormatDownloadInfo>();

        public string Thumbnail { get; }

        public string UploadDate { get; }

        public string Uploader { get; }

        public string UploaderId { get; }

        public string UploaderUrl { get; }

        public string Url { get; }

        public string Vcodec { get; }

        public long? ViewCount { get; }

        public string WebpageUrl { get; }

        public string WebpageUrlBasename { get; }

        public int? Width { get; }

        /// <summary>
        ///     The ID string of the video
        /// </summary>
        public string Id { get; }

        public List<ThumbnailDownloadInfo> Thumbnails { get; } = new List<ThumbnailDownloadInfo>();

        public List<string> Tags { get; }
    }
}