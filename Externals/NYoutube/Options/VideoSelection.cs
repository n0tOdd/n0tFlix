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

namespace NYoutubeDL.Options
{
    #region Using

    using System;
    using Helpers;

    #endregion

    /// <summary>
    ///     Object containing Video Selection parameters
    /// </summary>
    public class VideoSelection : OptionSection
    {
        [Option] internal readonly IntOption ageLimit = new IntOption("--age-limit");

        [Option] internal readonly DateTimeOption date = new DateTimeOption("--date");

        [Option] internal readonly DateTimeOption dateAfter = new DateTimeOption("--dateafter");

        [Option] internal readonly DateTimeOption dateBefore = new DateTimeOption("--datebefore");

        [Option] internal readonly StringOption downloadArchive = new StringOption("--download-archive");

        [Option] internal readonly BoolOption includeAds = new BoolOption("--include-ads");

        [Option] internal readonly StringOption matchFilter = new StringOption("--match-filter");

        [Option] internal readonly StringOption matchTitle = new StringOption("--match-title");

        [Option] internal readonly IntOption maxDownloads = new IntOption("--max-downloads");

        [Option] internal readonly FileSizeRateOption maxFileSize = new FileSizeRateOption("--max-filesize");

        [Option] internal readonly IntOption maxViews = new IntOption("--max-views");

        [Option] internal readonly FileSizeRateOption minFileSize = new FileSizeRateOption("--min-filesize");

        [Option] internal readonly IntOption minViews = new IntOption("--min-views");

        [Option] internal readonly BoolOption noPlaylist = new BoolOption("--no-playlist");

        [Option] internal readonly IntOption playlistEnd = new IntOption("--playlist-end");

        [Option] internal readonly StringOption playlistItems = new StringOption("--playlist-items");

        [Option] internal readonly IntOption playlistStart = new IntOption("--playlist-start");

        [Option] internal readonly StringOption rejectTitle = new StringOption("--reject-title");

        [Option] internal readonly BoolOption yesPlaylist = new BoolOption("--yes-playlist");

        /// <summary>
        ///     --age-limit
        /// </summary>
        public int AgeLimit
        {
            get => this.ageLimit.Value ?? -1;
            set => this.SetField(ref this.ageLimit.Value, value);
        }

        /// <summary>
        ///     --date
        /// </summary>
        public DateTime? Date
        {
            get => this.date.Value;
            set => this.SetField(ref this.date.Value, value);
        }

        /// <summary>
        ///     --dateafter
        /// </summary>
        public DateTime? DateAfter
        {
            get => this.dateAfter.Value;
            set => this.SetField(ref this.dateAfter.Value, value);
        }

        /// <summary>
        ///     --datebefore
        /// </summary>
        public DateTime? DateBefore
        {
            get => this.dateBefore.Value;
            set => this.SetField(ref this.dateBefore.Value, value);
        }

        /// <summary>
        ///     --download-archive
        /// </summary>
        public string DownloadArchive
        {
            get => this.downloadArchive.Value;
            set => this.SetField(ref this.downloadArchive.Value, value);
        }

        /// <summary>
        ///     [Experimental]
        ///     --include-ads
        /// </summary>
        public bool IncludeAds
        {
            get => this.includeAds.Value ?? false;
            set => this.SetField(ref this.includeAds.Value, value);
        }

        /// <summary>
        ///     --match-filter
        /// </summary>
        public string MatchFilter
        {
            get => this.matchFilter.Value;
            set => this.SetField(ref this.matchFilter.Value, value);
        }

        /// <summary>
        ///     --match-title
        /// </summary>
        public string MatchTitle
        {
            get => this.matchTitle.Value;
            set => this.SetField(ref this.matchTitle.Value, value);
        }

        /// <summary>
        ///     --max-downloads
        /// </summary>
        public int MaxDownloads
        {
            get => this.maxDownloads.Value ?? -1;
            set => this.SetField(ref this.maxDownloads.Value, value);
        }

        /// <summary>
        ///     --max-filesize
        /// </summary>
        public FileSizeRate MaxFileSize
        {
            get => this.maxFileSize.Value;
            set => this.SetField(ref this.maxFileSize.Value, value);
        }

        /// <summary>
        ///     --max-views
        /// </summary>
        public int MaxViews
        {
            get => this.maxViews.Value ?? -1;
            set => this.SetField(ref this.maxViews.Value, value);
        }

        /// <summary>
        ///     --min-filesize
        /// </summary>
        public FileSizeRate MinFileSize
        {
            get => this.minFileSize.Value;
            set => this.SetField(ref this.minFileSize.Value, value);
        }

        /// <summary>
        ///     --min-views
        /// </summary>
        public int MinViews
        {
            get => this.minViews.Value ?? -1;
            set => this.SetField(ref this.minViews.Value, value);
        }

        /// <summary>
        ///     --no-playlist
        /// </summary>
        public bool NoPlaylist
        {
            get => this.noPlaylist.Value ?? false;
            set => this.SetField(ref this.noPlaylist.Value, value);
        }

        /// <summary>
        ///     --playlist-end
        /// </summary>
        public int PlaylistEnd
        {
            get => this.playlistEnd.Value ?? -1;
            set => this.SetField(ref this.playlistEnd.Value, value);
        }

        /// <summary>
        ///     --playlist-items
        /// </summary>
        public string PlaylistItems
        {
            get => this.playlistItems.Value;
            set => this.SetField(ref this.playlistItems.Value, value);
        }

        /// <summary>
        ///     --playlist-start
        /// </summary>
        public int PlaylistStart
        {
            get => this.playlistStart.Value ?? 1;
            set => this.SetField(ref this.playlistStart.Value, value);
        }

        /// <summary>
        ///     --reject-title
        /// </summary>
        public string RejectTitle
        {
            get => this.rejectTitle.Value;
            set => this.SetField(ref this.rejectTitle.Value, value);
        }

        /// <summary>
        ///     --yes-playlist
        /// </summary>
        public bool YesPlaylist
        {
            get => this.yesPlaylist.Value ?? false;
            set => this.SetField(ref this.yesPlaylist.Value, value);
        }
    }
}