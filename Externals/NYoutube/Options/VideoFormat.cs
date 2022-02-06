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

    using Helpers;

    #endregion

    /// <summary>
    ///     Object containing Video Format parameters
    /// </summary>
    public class VideoFormat : OptionSection
    {
        [Option] internal readonly BoolOption allFormats = new BoolOption("--all-formats");

        [Option] internal readonly EnumOption<Enums.VideoFormat> format = new EnumOption<Enums.VideoFormat>("-f");

        [Option] internal readonly StringOption formatAdvanced = new StringOption("-f");

        [Option] internal readonly BoolOption listFormats = new BoolOption("-F");

        [Option]
        internal readonly EnumOption<Enums.VideoFormat> mergeOutputFormat =
            new EnumOption<Enums.VideoFormat>("--merge-output-format");

        [Option] internal readonly BoolOption preferFreeFormats = new BoolOption("--prefer-free-formats");

        [Option] internal readonly BoolOption youtubeSkipDashManifest = new BoolOption("--youtube-skip-dash-manifest");

        /// <summary>
        ///     --all-formats
        /// </summary>
        public bool AllFormats
        {
            get => this.allFormats.Value ?? false;
            set => this.SetField(ref this.allFormats.Value, value);
        }

        /// <summary>
        ///     This is a simple version of -f. For more advanced format usage, use the FormatAdvanced
        ///     property.
        ///     NOTE: FormatAdvanced takes precedence over Format.
        /// </summary>
        public Enums.VideoFormat Format
        {
            get => this.format.Value == null ? Enums.VideoFormat.undefined : (Enums.VideoFormat)this.format.Value;
            set => this.SetField(ref this.format.Value, (int)value);
        }

        /// <summary>
        ///     This accepts a string matching -f according to the youtube-dl documentation below.
        ///     NOTE: FormatAdvanced takes precedence over Format.
        ///     <see cref="https://github.com/rg3/youtube-dl/blob/master/README.md#format-selection" />
        /// </summary>
        public string FormatAdvanced
        {
            get => this.formatAdvanced.Value;
            set => this.SetField(ref this.formatAdvanced.Value, value);
        }

        /// <summary>
        ///     -F
        /// </summary>
        public bool ListFormats
        {
            get => this.listFormats.Value ?? false;
            set => this.SetField(ref this.listFormats.Value, value);
        }

        /// <summary>
        ///     --merge-output-format
        /// </summary>
        public Enums.VideoFormat MergeOutputFormat
        {
            get => this.mergeOutputFormat.Value == null
                ? Enums.VideoFormat.undefined
                : (Enums.VideoFormat)this.mergeOutputFormat.Value;
            set => this.SetField(ref this.mergeOutputFormat.Value, (int)value);
        }

        /// <summary>
        ///     --prefer-free-formats
        /// </summary>
        public bool PreferFreeFormats
        {
            get => this.preferFreeFormats.Value ?? false;
            set => this.SetField(ref this.preferFreeFormats.Value, value);
        }

        /// <summary>
        ///     --youtube-skip-dash-manifest
        /// </summary>
        public bool YoutubeSkipDashManifest
        {
            get => this.youtubeSkipDashManifest.Value ?? false;
            set => this.SetField(ref this.youtubeSkipDashManifest.Value, value);
        }

        public override string ToCliParameters()
        {
            // Set format to null if formatAdvanced has a valid value
            if (!string.IsNullOrWhiteSpace(this.formatAdvanced.Value))
            {
                this.format.Value = null;
            }

            return base.ToCliParameters();
        }
    }
}