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
    public class FormatDownloadInfo
    {
        public FormatDownloadInfo(FormatInfo info)
        {
            this.Abr = info.abr;
            this.Acodec = info.acodec;
            this.Asr = info.asr;
            this.Container = info.container;
            this.Ext = info.ext;
            this.Filesize = info.filesize;
            this.Format = info.format;
            this.FormatId = info.format_id;
            this.FormatNote = info.format_note;
            this.Fps = info.fps;
            this.Height = info.height;
            this.ManifestUrl = info.manifest_url;
            this.Preference = info.preference;
            this.Protocol = info.protocol;
            this.Resolution = info.resolution;
            this.Tbr = info.tbr;
            this.Url = info.url;
            this.Vcodec = info.vcodec;
            this.Width = info.width;
        }

        public int? Abr { get; }

        public string Acodec { get; }

        public int? Asr { get; }

        public string Container { get; }

        public string Ext { get; }

        public long? Filesize { get; }

        public string Format { get; }

        public string FormatId { get; }

        public string FormatNote { get; }

        public double? Fps { get; }

        public int? Height { get; }

        public string ManifestUrl { get; }

        public int? Preference { get; }

        public string Protocol { get; }

        public string Resolution { get; }

        public double? Tbr { get; }

        public string Url { get; }

        public string Vcodec { get; }

        public int? Width { get; }
    }
}