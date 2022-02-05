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
    ///     Object containing Download parameters
    /// </summary>
    public class Download : OptionSection
    {
        [Option] internal readonly BoolOption abortOnUnavailableFragment =
            new BoolOption("--abort-on-unavailable-fragment");

        [Option] internal readonly FileSizeRateOption bufferSize = new FileSizeRateOption("--buffer-size");

        [Option] internal readonly EnumOption<Enums.ExternalDownloader> externalDownloader =
            new EnumOption<Enums.ExternalDownloader>("--external-downloader");

        [Option] internal readonly StringOption externalDownloaderArgs = new StringOption("--external-downloader-args");

        [Option] internal readonly IntOption fragmentRetries = new IntOption("--fragment-retries", true);

        [Option] internal readonly BoolOption hlsPreferFfmpeg = new BoolOption("--hls-prefer-ffmpeg");

        [Option] internal readonly BoolOption hlsPreferNative = new BoolOption("--hls-prefer-native");

        [Option] internal readonly BoolOption hlsUseMpegts = new BoolOption("--hls-use-mpegts");

        [Option] internal readonly FileSizeRateOption httpChunkSize = new FileSizeRateOption("--http-chunk-size");

        [Option] internal readonly FileSizeRateOption limitRate = new FileSizeRateOption("-r");

        [Option] internal readonly BoolOption noResizeBuffer = new BoolOption("--no-resize-buffer");

        [Option] internal readonly BoolOption playlistRandom = new BoolOption("--playlist-random");

        [Option] internal readonly BoolOption playlistReverse = new BoolOption("--playlist-reverse");

        [Option] internal readonly IntOption retries = new IntOption("-R", true);

        [Option] internal readonly BoolOption skipUnavailableFragments = new BoolOption("--skip-unavailable-fragments");

        [Option] internal readonly BoolOption xattrSetFilesize = new BoolOption("--xattr-set-filesize");

        /// <summary>
        ///     --abort-on-unavailable-fragment
        /// </summary>
        public bool AbortOnUnavailableFragment
        {
            get => this.abortOnUnavailableFragment.Value ?? false;
            set => this.SetField(ref this.abortOnUnavailableFragment.Value, value);
        }

        /// <summary>
        ///     --buffer-size
        /// </summary>
        public FileSizeRate BufferSize
        {
            get => this.bufferSize.Value;
            set => this.SetField(ref this.bufferSize.Value, value);
        }

        /// <summary>
        ///     --external-downloader
        /// </summary>
        public Enums.ExternalDownloader ExternalDownloader
        {
            get => this.externalDownloader.Value == null
                ? Enums.ExternalDownloader.undefined
                : (Enums.ExternalDownloader) this.externalDownloader.Value;
            set => this.SetField(ref this.externalDownloader.Value, (int) value);
        }

        /// <summary>
        ///     --external-downloader-args
        /// </summary>
        public string ExternalDownloaderArgs
        {
            get => this.externalDownloaderArgs.Value;
            set => this.SetField(ref this.externalDownloaderArgs.Value, value);
        }

        /// <summary>
        ///     --fragment-retries
        /// </summary>
        public int FragmentRetries
        {
            get => this.fragmentRetries.Value ?? 10;
            set => this.SetField(ref this.fragmentRetries.Value, value);
        }

        /// <summary>
        ///     --hls-prefer-ffmpeg
        /// </summary>
        public bool HlsPreferFfmpeg
        {
            get => this.hlsPreferFfmpeg.Value ?? false;
            set => this.SetField(ref this.hlsPreferFfmpeg.Value, value);
        }

        /// <summary>
        ///     --hls-prefer-native
        /// </summary>
        public bool HlsPreferNative
        {
            get => this.hlsPreferNative.Value ?? false;
            set => this.SetField(ref this.hlsPreferNative.Value, value);
        }

        /// <summary>
        ///     --hls-use-mpegts
        /// </summary>
        public bool HlsUseMpegts
        {
            get => this.hlsUseMpegts.Value ?? false;
            set => this.SetField(ref this.hlsUseMpegts.Value, value);
        }

        /// <summary>
        ///     -r
        /// </summary>
        public FileSizeRate LimitRate
        {
            get => this.limitRate.Value;
            set => this.SetField(ref this.limitRate.Value, value);
        }

        /// <summary>
        ///     --no-resize-buffer
        /// </summary>
        public bool NoResizeBuffer
        {
            get => this.noResizeBuffer.Value ?? false;
            set => this.SetField(ref this.noResizeBuffer.Value, value);
        }

        /// <summary>
        ///     --playlist-random
        /// </summary>
        public bool PlaylistRandom
        {
            get => this.playlistRandom.Value ?? false;
            set => this.SetField(ref this.playlistRandom.Value, value);
        }

        /// <summary>
        ///     --playlist-reverse
        /// </summary>
        public bool PlaylistReverse
        {
            get => this.playlistReverse.Value ?? false;
            set => this.SetField(ref this.playlistReverse.Value, value);
        }

        /// <summary>
        ///     -R
        /// </summary>
        public int Retries
        {
            get => this.retries.Value ?? 10;
            set => this.SetField(ref this.retries.Value, value);
        }

        /// <summary>
        ///     --skip-unavailable-fragments
        /// </summary>
        public bool SkipUnavailableFragments
        {
            get => this.skipUnavailableFragments.Value ?? false;
            set => this.SetField(ref this.skipUnavailableFragments.Value, value);
        }

        /// <summary>
        ///     --xattr-set-filesize
        /// </summary>
        public bool XattrSetFilesize
        {
            get => this.xattrSetFilesize.Value ?? false;
            set => this.SetField(ref this.xattrSetFilesize.Value, value);
        }

        /// <summary>
        ///     --http-chunk-size
        /// </summary>
        public FileSizeRate HttpChunkSize
        {
            get => this.httpChunkSize.Value;
            set => this.SetField(ref this.httpChunkSize.Value, value);
        }
    }
}