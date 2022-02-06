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

namespace NYoutubeDL.Helpers
{
    // ReSharper disable InconsistentNaming
    // due to following youtube-dl
    // naming conventions

    public static class Enums
    {
        /// <summary>
        ///     Audio Format Types
        /// </summary>
        public enum AudioFormat
        {
            best,

            threegp,

            aac,

            vorbis,

            mp3,

            m4a,

            opus,

            wav
        }

        /// <summary>
        ///     Download rate units (B, K, M)
        /// </summary>
        public enum ByteUnit
        {
            B,

            K,

            M
        }

        /// <summary>
        ///     External downloader.
        /// </summary>
        public enum ExternalDownloader
        {
            undefined,

            aria2c,

            avconv,

            axel,

            curl,

            ffmpeg,

            httpie,

            wget
        }

        /// <summary>
        ///     Fixup policy, how to treat errors when downloading.
        /// </summary>
        public enum FixupPolicy
        {
            nothing,

            warn,

            detect_or_warn
        }

        public enum SubtitleFormat
        {
            undefined,

            srt,

            ass,

            vtt
        }

        /// <summary>
        ///     Video Format Types
        /// </summary>
        public enum VideoFormat
        {
            undefined,

            mp4,

            flv,

            ogg,

            webm,

            mkv,

            avi,

            best,

            worst
        }

        /// <summary>
        ///     Download status enum
        /// </summary>
        public sealed class DownloadStatus : TypeSafeEnum
        {
            public static readonly DownloadStatus DONE = new DownloadStatus(5, "Done");

            public static readonly DownloadStatus DOWNLOADING = new DownloadStatus(4, "Downloading");

            public static readonly DownloadStatus ERROR = new DownloadStatus(2, "Error");

            public static readonly DownloadStatus WAITING = new DownloadStatus(1, "Waiting");

            public static readonly DownloadStatus WARNING = new DownloadStatus(3, "Warning");

            private DownloadStatus(int value, string name) : base(value, name)
            {
            }
        }
    }
}