// Copyright 2020 Brian Allred
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

namespace n0tFlix.Plugin.YoutubeDL.Models
{
    #region Using

    using System.Collections.Generic;

    #endregion

    // ReSharper disable InconsistentNaming
    // due to following youtube-dl
    // naming conventions

    public class VideoInfo
    {
        public string _type { get; set; }

        public int? abr { get; set; }

        public string acodec { get; set; }

        public int? age_limit { get; set; }

        public object alt_title { get; set; }

        public object annotations { get; set; }

        public double? average_rating { get; set; }

        public List<string> categories { get; set; }

        public object creator { get; set; }

        public string description { get; set; }

        public long? dislike_count { get; set; }

        public string display_id { get; set; }

        public double? duration { get; set; }

        public object end_time { get; set; }

        public object episode_number { get; set; }

        public string ext { get; set; }

        public string extractor { get; set; }

        public string extractor_key { get; set; }

        public string format { get; set; }

        public string format_id { get; set; }

        public List<FormatInfo> formats { get; set; }

        public double? fps { get; set; }

        public int? height { get; set; }

        public string id { get; set; }

        public string ie_key { get; set; }

        public object is_live { get; set; }

        public string license { get; set; }

        public long? like_count { get; set; }

        public int? n_entries { get; set; }

        public string playlist { get; set; }

        public string playlist_id { get; set; }

        public int? playlist_index { get; set; }

        public string playlist_title { get; set; }

        public List<FormatInfo> requested_formats { get; set; }

        public object requested_subtitles { get; set; }

        public object resolution { get; set; }

        public object season_number { get; set; }

        public object series { get; set; }

        public object start_time { get; set; }

        public object stretched_ratio { get; set; }

        public List<string> tags { get; set; }

        public string thumbnail { get; set; }

        public List<ThumbnailInfo> thumbnails { get; set; }

        public string title { get; set; }

        public string upload_date { get; set; }

        public string uploader { get; set; }

        public string uploader_id { get; set; }

        public string uploader_url { get; set; }

        public string url { get; set; }

        public object vbr { get; set; }

        public string vcodec { get; set; }

        public long? view_count { get; set; }

        public string webpage_url { get; set; }

        public string webpage_url_basename { get; set; }

        public int? width { get; set; }
    }
}