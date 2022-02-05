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

using n0tFlix.Plugin.YoutubeDL.Models;

namespace n0tFlix.Plugin.YoutubeDL.Models
{
    #region Using

    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    #endregion Using

    /// <summary>
    ///     Class holding information about a playlist being downloaded
    /// </summary>
    public class PlaylistDownloadInfo : DownloadInfo
    {
        private VideoDownloadInfo currentVideo;

        private int videoIndex = 1;
        private PlaylistInfo info;

        public PlaylistDownloadInfo(PlaylistInfo info)
        {
            this.Title = info.title;
            foreach (VideoInfo videoInfo in info.entries)
            {
                this.Videos.Add(new VideoDownloadInfo(videoInfo));
            }
        }

        /// <summary>
        ///     The current video downloading
        /// </summary>
        public VideoDownloadInfo CurrentVideo
        {
            get => this.currentVideo;
            set => this.SetField(ref this.currentVideo, value);
        }

        /// <summary>
        ///     The index of the downloading video
        /// </summary>
        public int VideoIndex
        {
            get => this.videoIndex;
            set => this.SetField(ref this.videoIndex, value);
        }

        /// <summary>
        ///     Collection of videos this playlist contains
        /// </summary>
        public ObservableCollection<VideoDownloadInfo> Videos { get; } = new ObservableCollection<VideoDownloadInfo>();

        internal override void ParseError(object sender, string error)
        {
            this.currentVideo?.ParseError(sender, error);
            base.ParseError(sender, error);
        }

        internal override void ParseOutput(object sender, string output)
        {
            if (output.Contains(DOWNLOADSTRING) && output.Contains(VIDEOSTRING) && output.Contains(OFSTRING))
            {
                Regex regex = new Regex(".*?(\\d+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match match = regex.Match(output);
                if (match.Success)
                {
                    this.VideoIndex = int.Parse(match.Groups[1].ToString());
                    this.CurrentVideo = this.Videos[this.videoIndex - 1];
                }
            }

            this.CurrentVideo?.ParseOutput(sender, output);
            base.ParseOutput(sender, output);
        }
    }
}