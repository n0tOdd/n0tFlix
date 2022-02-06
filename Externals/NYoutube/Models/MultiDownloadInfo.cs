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
    using System.Collections.ObjectModel;

    #endregion

    /// <summary>
    ///     Class holding data about multiple downloads (multiple single videos, multiple playlists, or a mix of both)
    /// </summary>
    public class MultiDownloadInfo : DownloadInfo
    {
        private const string Webpage = "Downloading webpage";

        private const string YoutubePlaylist = "youtube:playlist";

        private readonly Dictionary<string, DownloadInfo> videos = new Dictionary<string, DownloadInfo>();

        private DownloadInfo currentVideo;

        public MultiDownloadInfo(IEnumerable<DownloadInfo> infos)
        {
            foreach (DownloadInfo info in infos)
            {
                if (info is VideoDownloadInfo videoInfo)
                {
                    if (!this.videos.ContainsKey(videoInfo.Id))
                    {
                        this.Videos.Add(videoInfo);
                        this.videos.Add(videoInfo.Id, videoInfo);
                    }

                    continue;
                }

                if (info is PlaylistDownloadInfo playlistInfo)
                {
                    this.Playlists.Add(playlistInfo);
                    foreach (VideoDownloadInfo vInfo in playlistInfo.Videos)
                    {
                        if (!this.videos.ContainsKey(vInfo.Id))
                        {
                            this.videos.Add(vInfo.Id, vInfo);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Collection of playlists being downloaded
        /// </summary>
        public ObservableCollection<PlaylistDownloadInfo> Playlists { get; } =
            new ObservableCollection<PlaylistDownloadInfo>();

        /// <summary>
        ///     Collection of videos being downloaded
        /// </summary>
        public ObservableCollection<VideoDownloadInfo> Videos { get; } = new ObservableCollection<VideoDownloadInfo>();

        internal override void ParseError(object sender, string error)
        {
            this.currentVideo?.ParseError(sender, error);
            base.ParseError(sender, error);
        }

        internal override void ParseOutput(object sender, string output)
        {
            if (output.Contains(YoutubePlaylist))
            {
                return;
            }

            if (output.Contains(Webpage))
            {
                int startIndex = output.IndexOf(' ') + 1;
                int length = output.IndexOf(':') - startIndex;
                string id = output.Substring(startIndex, length);

                if (this.videos.ContainsKey(id))
                {
                    this.currentVideo = this.videos[id];
                }
            }
            else
            {
                this.currentVideo?.ParseOutput(sender, output);
            }

            base.ParseOutput(sender, output);
        }
    }
}