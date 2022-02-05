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

namespace n0tFlix.Plugin.YoutubeDL.Options
{
    #region Using

    using Helpers;

    #endregion

    /// <summary>
    ///     Object containing PostProcessing parameters
    /// </summary>
    public class PostProcessing : OptionSection
    {
        [Option] internal readonly BoolOption addMetadata = new BoolOption("--add-metadata");

        [Option] internal readonly EnumOption<Enums.AudioFormat> audioFormat =
            new EnumOption<Enums.AudioFormat>("--audio-format");

        [Option] internal readonly StringOption audioQuality = new StringOption("--audio-quality");

        [Option] internal readonly StringOption command = new StringOption("--exec");

        [Option] internal readonly EnumOption<Enums.SubtitleFormat> convertSubs =
            new EnumOption<Enums.SubtitleFormat>("--convert-subs");

        [Option] internal readonly BoolOption embedSubs = new BoolOption("--embed-subs");

        [Option] internal readonly BoolOption embedThumbnail = new BoolOption("--embed-thumbnail");

        [Option] internal readonly BoolOption extractAudio = new BoolOption("-x");

        [Option] internal readonly StringOption ffmpegLocation = new StringOption("--ffmpeg-location");

        [Option] internal readonly EnumOption<Enums.FixupPolicy> fixupPolicy =
            new EnumOption<Enums.FixupPolicy>("--fixup");

        [Option] internal readonly BoolOption keepVideo = new BoolOption("-k");

        [Option] internal readonly StringOption metadataFromTitle = new StringOption("--metadata-from-title");

        [Option] internal readonly BoolOption noPostOverwrites = new BoolOption("--no-post-overwrites");

        [Option] internal readonly StringOption postProcessorArgs = new StringOption("--postprocessor-args");

        [Option] internal readonly BoolOption preferAvconv = new BoolOption("--prefer-avconv");

        [Option] internal readonly BoolOption preferFfmpeg = new BoolOption("--prefer-ffmpeg");

        [Option] internal readonly EnumOption<Enums.VideoFormat> recodeFormat =
            new EnumOption<Enums.VideoFormat>("--recode-video");

        [Option] internal readonly BoolOption xattrs = new BoolOption("--xattrs");

        /// <summary>
        ///     --add-metadata
        /// </summary>
        public bool AddMetadata
        {
            get => this.addMetadata.Value ?? false;
            set => this.SetField(ref this.addMetadata.Value, value);
        }

        /// <summary>
        ///     --audio-format
        /// </summary>
        public Enums.AudioFormat AudioFormat
        {
            get => this.audioFormat.Value == null
                ? Enums.AudioFormat.best
                : (Enums.AudioFormat) this.audioFormat.Value;
            set => this.SetField(ref this.audioFormat.Value, (int) value);
        }

        /// <summary>
        ///     --audio-quality
        /// </summary>
        public string AudioQuality
        {
            get => this.audioQuality.Value ?? "5";
            set => this.SetField(ref this.audioQuality.Value, value);
        }

        /// <summary>
        ///     --exec
        /// </summary>
        public string Command
        {
            get => this.command.Value;
            set => this.SetField(ref this.command.Value, value);
        }

        /// <summary>
        ///     --convert-subs
        /// </summary>
        public Enums.SubtitleFormat ConvertSubs
        {
            get => this.convertSubs.Value == null
                ? Enums.SubtitleFormat.undefined
                : (Enums.SubtitleFormat) this.convertSubs.Value;
            set => this.SetField(ref this.convertSubs.Value, (int) value);
        }

        /// <summary>
        ///     --embed-subs
        /// </summary>
        public bool EmbedSubs
        {
            get => this.embedSubs.Value ?? false;
            set => this.SetField(ref this.embedSubs.Value, value);
        }

        /// <summary>
        ///     --embed-thumbnail
        /// </summary>
        public bool EmbedThumbnail
        {
            get => this.embedThumbnail.Value ?? false;
            set => this.SetField(ref this.embedThumbnail.Value, value);
        }

        /// <summary>
        ///     -x
        /// </summary>
        public bool ExtractAudio
        {
            get => this.extractAudio.Value ?? false;
            set => this.SetField(ref this.extractAudio.Value, value);
        }

        /// <summary>
        ///     --ffmpeg-location
        /// </summary>
        public string FfmpegLocation
        {
            get => this.ffmpegLocation.Value;
            set => this.SetField(ref this.ffmpegLocation.Value, value);
        }

        /// <summary>
        ///     --fixup
        /// </summary>
        public Enums.FixupPolicy FixupPolicy
        {
            get => this.fixupPolicy.Value == null
                ? Enums.FixupPolicy.detect_or_warn
                : (Enums.FixupPolicy) this.fixupPolicy.Value;
            set => this.SetField(ref this.fixupPolicy.Value, (int) value);
        }

        /// <summary>
        ///     -k
        /// </summary>
        public bool KeepVideo
        {
            get => this.keepVideo.Value ?? false;
            set => this.SetField(ref this.keepVideo.Value, value);
        }

        /// <summary>
        ///     --metadata-from-title
        /// </summary>
        public string MetadataFromTitle
        {
            get => this.metadataFromTitle.Value;
            set => this.SetField(ref this.metadataFromTitle.Value, value);
        }

        /// <summary>
        ///     --no-post-overwrites
        /// </summary>
        public bool NoPostOverwrites
        {
            get => this.noPostOverwrites.Value ?? false;
            set => this.SetField(ref this.noPostOverwrites.Value, value);
        }

        /// <summary>
        ///     --postprocessor-args
        /// </summary>
        public string PostProcessorArgs
        {
            get => this.postProcessorArgs.Value;
            set => this.SetField(ref this.postProcessorArgs.Value, value);
        }

        /// <summary>
        ///     --prefer-avconv
        /// </summary>
        public bool PreferAvconv
        {
            get => this.preferAvconv.Value ?? false;
            set => this.SetField(ref this.preferAvconv.Value, value);
        }

        /// <summary>
        ///     --prefer-ffmpeg
        /// </summary>
        public bool PreferFfmpeg
        {
            get => this.preferFfmpeg.Value ?? false;
            set => this.SetField(ref this.preferFfmpeg.Value, value);
        }

        /// <summary>
        ///     --recode-video
        /// </summary>
        public Enums.VideoFormat RecodeFormat
        {
            get => this.recodeFormat.Value == null
                ? Enums.VideoFormat.undefined
                : (Enums.VideoFormat) this.recodeFormat.Value;
            set => this.SetField(ref this.recodeFormat.Value, (int) value);
        }

        /// <summary>
        ///     [Experimental]
        ///     --xattrs
        /// </summary>
        public bool Xattrs
        {
            get => this.xattrs.Value ?? false;
            set => this.SetField(ref this.xattrs.Value, value);
        }
    }
}