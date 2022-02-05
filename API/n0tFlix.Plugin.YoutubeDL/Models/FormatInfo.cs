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
    // ReSharper disable InconsistentNaming
    // due to following youtube-dl
    // naming conventions

    public class FormatInfo
    {
        public int? abr { get; set; }

        public string acodec { get; set; }

        public int? asr { get; set; }

        public string container { get; set; }

        public string ext { get; set; }

        public long? filesize { get; set; }

        public string format { get; set; }

        public string format_id { get; set; }

        public string format_note { get; set; }

        public double? fps { get; set; }

        public int? height { get; set; }

        public object language { get; set; }

        public string manifest_url { get; set; }

        public object player_url { get; set; }

        public int? preference { get; set; }

        public string protocol { get; set; }

        public string resolution { get; set; }

        public double? tbr { get; set; }

        public string url { get; set; }

        public string vcodec { get; set; }

        public int? width { get; set; }
    }
}