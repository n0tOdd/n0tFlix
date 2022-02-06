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

    using System;
    using System.Collections.Generic;
    using Helpers;
    using n0tFlix.Plugin.YoutubeDL.Helpers;
    using n0tFlix.Plugin.YoutubeDL.Models;
    

    #endregion Using

    /// <summary>
    ///     Class holding data about the current download, which is parsed from youtube-dl's standard output
    /// </summary>
    public class DownloadInfo : NotifyPropertyChangedEx
    {
        protected const string ALREADY = "already";

        protected const string DOWNLOADRATESTRING = "iB/s";

        protected const string DOWNLOADSIZESTRING = "iB";

        protected const string ETASTRING = "ETA";

        protected const string OFSTRING = "of";

        protected const string VIDEOSTRING = "video";

        protected const string DOWNLOADSTRING = "[download]";

        internal bool set = false;

        private string downloadRate;

        private string eta;

        private string status = Enums.DownloadStatus.WAITING.ToString();

        private string title;

        private int videoProgress;

        private string videoSize;

        /// <summary>
        ///     The current download rate
        /// </summary>
        public string DownloadRate
        {
            get => this.downloadRate;
            set => this.SetField(ref this.downloadRate, value);
        }

        /// <summary>
        ///     The collection of error messages received
        /// </summary>
        public List<string> Errors { get; } = new List<string>();

        /// <summary>
        ///     The current download's estimated time remaining
        /// </summary>
        public string Eta
        {
            get => this.eta;
            set => this.SetField(ref this.eta, value);
        }

        /// <summary>
        ///     The status of the video currently downloading
        /// </summary>
        public string Status
        {
            get => this.status;
            set
            {
                if (!this.status.Equals(Enums.DownloadStatus.ERROR.ToString()) &&
                    !this.status.Equals(Enums.DownloadStatus.WARNING.ToString()))
                {
                    this.SetField(ref this.status, value);
                }
                else if (value.Equals(Enums.DownloadStatus.ERROR.ToString()) &&
                         this.status.Equals(Enums.DownloadStatus.WARNING.ToString()))
                {
                    this.SetField(ref this.status, value);
                }
            }
        }

        /// <summary>
        ///     The title of the video currently downloading
        /// </summary>
        public string Title
        {
            get => this.title;
            set => this.SetField(ref this.title, value);
        }

        /// <summary>
        ///     The current download progresss
        /// </summary>
        public int VideoProgress
        {
            get => this.videoProgress;
            set
            {
                this.SetField(ref this.videoProgress, value);

                if (value == 0)
                {
                    this.Status = Enums.DownloadStatus.WAITING.ToString();
                }
                else if (value == 100)
                {
                    this.Status = Enums.DownloadStatus.DONE.ToString();
                }
                else
                {
                    this.Status = Enums.DownloadStatus.DOWNLOADING.ToString();
                }
            }
        }

        /// <summary>
        ///     The current download's total size
        /// </summary>
        public string VideoSize
        {
            get => this.videoSize;
            set => this.SetField(ref this.videoSize, value);
        }

        /// <summary>
        ///     The collection of warning messages received
        /// </summary>
        public List<string> Warnings { get; } = new List<string>();

        internal static DownloadInfo CreateDownloadInfo(string output)
        {
            try
            {
                PlaylistInfo info = System.Text.Json.JsonSerializer.Deserialize<PlaylistInfo>(output);
                if (!string.IsNullOrEmpty(info._type) && info._type.Equals("playlist"))
                {
                    return new PlaylistDownloadInfo(info);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                VideoInfo info = System.Text.Json.JsonSerializer.Deserialize<VideoInfo > (output);
                if (!string.IsNullOrEmpty(info.title))
                {
                    return new VideoDownloadInfo(info);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        /// <summary>
        ///     Fired when an error occurs
        /// </summary>
        public event EventHandler<string> ErrorEvent;

        internal virtual void ParseError(object sender, string error)
        {
            this.ErrorEvent?.Invoke(this, error);
            if (error.Contains("WARNING"))
            {
                this.Warnings.Add(error);
                this.Status = Enums.DownloadStatus.WARNING.ToString();
            }
            else if (error.Contains("ERROR"))
            {
                this.Errors.Add(error);
                this.Status = Enums.DownloadStatus.ERROR.ToString();
            }
        }

        internal virtual void ParseOutput(object sender, string output)
        {
            try
            {
                if (output.Contains("%"))
                {
                    int progressIndex = output.LastIndexOf(' ', output.IndexOf('%')) + 1;
                    string progressString = output.Substring(progressIndex, output.IndexOf('%') - progressIndex);
                    this.VideoProgress = (int)Math.Round(double.Parse(progressString));

                    int sizeIndex = output.LastIndexOf(' ', output.IndexOf(DOWNLOADSIZESTRING)) + 1;
                    string sizeString = output.Substring(sizeIndex, output.IndexOf(DOWNLOADSIZESTRING) - sizeIndex + 2);
                    this.VideoSize = sizeString;
                }

                if (output.Contains(DOWNLOADRATESTRING))
                {
                    int rateIndex = output.LastIndexOf(' ', output.LastIndexOf(DOWNLOADRATESTRING)) + 1;
                    string rateString =
                        output.Substring(rateIndex, output.LastIndexOf(DOWNLOADRATESTRING) - rateIndex + 4);
                    this.DownloadRate = rateString;
                }

                if (output.Contains(ETASTRING))
                {
                    this.Eta = output.Substring(output.LastIndexOf(' ') + 1);
                }

                if (output.Contains(ALREADY))
                {
                    this.Status = Enums.DownloadStatus.DONE.ToString();
                    this.VideoProgress = 100;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}