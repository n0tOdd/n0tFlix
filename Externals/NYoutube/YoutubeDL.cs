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

namespace NYoutubeDL
{
    #region Using

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Helpers;
    using Models;
    using Services;

    #endregion

    // ReSharper disable InconsistentNaming
    // due to following youtube-dl
    // naming conventions

    /// <summary>
    ///     C# interface for youtube-dl
    /// </summary>
    public class YoutubeDL
    {
        /// <summary>
        ///     The semaphore
        /// </summary>
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        ///     The youtube-dl process
        /// </summary>
        internal Process process;

        /// <summary>
        ///     The process's information
        /// </summary>
        internal ProcessStartInfo processStartInfo;

        /// <summary>
        ///     Cancellation token used to stop the thread processing youtube-dl's standard error output.
        /// </summary>
        internal CancellationTokenSource stdErrorTokenSource;

        internal EventHandler<string> stdOutputEvent;

        internal EventHandler stdOutputClosedEvent;

        /// <summary>
        ///     Cancellation token used to stop the thread processing youtube-dl's standard console output.
        /// </summary>
        internal CancellationTokenSource stdOutputTokenSource;

        /// <summary>
        ///     Cancellation token used to stop downloads.
        /// </summary>
        internal CancellationTokenSource downloadTokenSource;

        /// <summary>
        ///     Whether the client is currently downloading video info
        /// </summary>
        internal bool isGettingInfo;

        /// <summary>
        ///     Process ID of the youtube-dl process
        /// </summary>
        internal int downloadProcessID;

        /// <summary>
        ///     Creates a new YoutubeDL client
        /// </summary>
        public YoutubeDL()
        {
            downloadTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        ///     Creates a new YoutubeDL client
        /// </summary>
        /// <param name="path">
        ///     Path of youtube-dl binary
        /// </param>
        public YoutubeDL(string path) : this()
        {
            this.YoutubeDlPath = path;
        }

        /// <summary>
        ///     Information about the download
        /// </summary>
        public DownloadInfo Info { get; internal set; } = new DownloadInfo();

        /// <summary>
        ///     The options to pass to youtube-dl
        /// </summary>
        public Options.Options Options { get; set; } = new Options.Options();

        /// <summary>
        ///     Returns whether the download process is actively running
        /// </summary>
        public bool IsDownloading { get; internal set; }

        /// <summary>
        ///     Gets the complete command that was run by Download().
        /// </summary>
        /// <value>The run command.</value>
        public string RunCommand { get; internal set; }

        /// <summary>
        ///     URL of video to download
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        ///     The path to the youtube-dl binary
        /// </summary>
        public string YoutubeDlPath { get; set; } = new FileInfo("youtube-dl").GetFullPath();

        /// <summary>
        ///     Whether this youtubedl client should retrieve all info
        ///     NOTE: For large playlists / many videos, this will be excruciatingly SLOW!
        /// </summary>
        public bool RetrieveAllInfo { get; set; }

        /// <summary>
        ///     Path to Python binary. Used in server scenarios.
        /// </summary>
        public string PythonPath { get; set; }

        /// <summary>
        ///     Convert class into parameters to pass to youtube-dl process, then create and run process.
        ///     Also handle output from process.
        /// </summary>
        public async Task DownloadAsync()
        {
            await this.semaphore.WaitAsync();
            await DownloadService.DownloadAsync(this, downloadTokenSource.Token);
            this.semaphore.Release();
        }

        /// <summary>
        ///     Convert class into parameters to pass to youtube-dl process, then create and run process.
        ///     Also handle output from process.
        /// </summary>
        /// <param name="videoUrl">URL of video to download</param>
        public async Task DownloadAsync(string videoUrl)
        {
            await this.semaphore.WaitAsync();
            await DownloadService.DownloadAsync(this, videoUrl, downloadTokenSource.Token);
            this.semaphore.Release();
        }

        /// <summary>
        ///     Convert class into parameters to pass to youtube-dl process, then create and run process.
        ///     Also handle output from process.
        /// </summary>
        public void Download()
        {
            this.semaphore.Wait();
            DownloadService.Download(this, downloadTokenSource.Token);
            this.semaphore.Release();
        }

        /// <summary>
        ///     Convert class into parameters to pass to youtube-dl process, then create and run process.
        ///     Also handle output from process.
        /// </summary>
        /// <param name="videoUrl">URL of video to download</param>
        public void Download(string videoUrl)
        {
            this.semaphore.Wait();
            DownloadService.Download(this, videoUrl, downloadTokenSource.Token);
            this.semaphore.Release();
        }

        /// <summary>
        ///     Get information about the video/playlist before downloading
        /// </summary>
        /// <returns>
        ///     Object representing the information of the video/playlist
        /// </returns>
        public async Task<DownloadInfo> GetDownloadInfoAsync()
        {
            await this.semaphore.WaitAsync();
            DownloadInfo info = await InfoService.GetDownloadInfoAsync(this, downloadTokenSource.Token);
            this.semaphore.Release();
            return info;
        }

        /// <summary>
        ///     Get information about the video/playlist before downloading
        /// </summary>
        /// <param name="url">
        ///     Video/playlist to retrieve information about
        /// </param>
        /// <returns>
        ///     Object representing the information of the video/playlist
        /// </returns>
        public async Task<DownloadInfo> GetDownloadInfoAsync(string url)
        {
            await this.semaphore.WaitAsync();
            DownloadInfo info = await InfoService.GetDownloadInfoAsync(this, url, downloadTokenSource.Token);
            this.semaphore.Release();
            return info;
        }

        /// <summary>
        ///     Get information about the video/playlist before downloading
        /// </summary>
        /// <returns>
        ///     Object representing the information of the video/playlist
        /// </returns>
        public DownloadInfo GetDownloadInfo()
        {
            this.semaphore.Wait();
            DownloadInfo info = InfoService.GetDownloadInfo(this, downloadTokenSource.Token);
            this.semaphore.Release();
            return info;
        }

        /// <summary>
        ///     Get information about the video/playlist before downloading
        /// </summary>
        /// <param name="url">
        ///     Video/playlist to retrieve information about
        /// </param>
        /// <returns>
        ///     Object representing the information of the video/playlist
        /// </returns>
        public DownloadInfo GetDownloadInfo(string url)
        {
            this.semaphore.Wait();
            DownloadInfo info = InfoService.GetDownloadInfo(this, url, downloadTokenSource.Token);
            this.semaphore.Release();
            return info;
        }

        /// <summary>
        ///     Prepares the arguments to pass into downloader
        /// </summary>
        /// <returns>
        ///     The string of arguments built from the options
        /// </returns>
        public async Task<string> PrepareDownloadAsync()
        {
            await this.semaphore.WaitAsync();
            string args = await PreparationService.PrepareDownloadAsync(this, downloadTokenSource.Token);
            this.semaphore.Release();
            return args;
        }

        /// <summary>
        ///     Prepares the arguments to pass into downloader
        /// </summary>
        /// <returns>
        ///     The string of arguments built from the options
        /// </returns>
        public string PrepareDownload()
        {
            this.semaphore.Wait();
            string args = PreparationService.PrepareDownload(this, downloadTokenSource.Token);
            this.semaphore.Release();
            return args;
        }

        public void CancelDownload()
        {
            try
            {
                this.downloadTokenSource.Cancel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n{ex}\n\n");
            }
            finally
            {
                this.downloadTokenSource = new CancellationTokenSource();
            }
        }

        /// <summary>
        ///     Kills the process and associated threads.
        ///     We catch these exceptions in case the objects have already been disposed of.
        /// </summary>
        internal void KillStandardEventHandlers()
        {
            try
            {
                this.stdOutputTokenSource.Cancel();
                this.stdOutputTokenSource.Dispose();
            }
            catch (ObjectDisposedException)
            {
            }

            try
            {
                this.stdErrorTokenSource.Cancel();
                this.stdErrorTokenSource.Dispose();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        internal void StopProcess()
        {
            try
            {
                if (this.process != null)
                {
                    if (!this.process.HasExited)
                    {
                        this.process.Kill();
                    }

                    this.process.Dispose();
                }
            }
            catch (Exception)
            {
                try
                {
                    Process processById = Process.GetProcessById(this.downloadProcessID);
                    if (processById != null && processById.StartTime != null && processById.ProcessName.IndexOf("youtube-dl", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        processById.Kill();
                        processById.Dispose();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        ///     Fires the error event when error output is received from process.
        /// </summary>
        /// <param name="tokenObj">
        ///     Cancellation token
        /// </param>
        internal void StandardError(object tokenObj)
        {
            CancellationToken token = (CancellationToken)tokenObj;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (this.process != null && !this.process.HasExited)
                    {
                        string error;
                        if (!string.IsNullOrEmpty(error = this.process.StandardError.ReadLine()))
                        {
                            if (!error.Equals("null", StringComparison.InvariantCultureIgnoreCase))
                            {
                                this.StandardErrorEvent?.Invoke(this, error);
                            }
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        /// <summary>
        ///     Occurs when standard error is received.
        /// </summary>
        public event EventHandler<string> StandardErrorEvent;

        /// <summary>
        ///     Fires the output event when output is received from process.
        /// </summary>
        /// <param name="tokenObj">
        ///     Cancellation token
        /// </param>
        internal void StandardOutput(object tokenObj)
        {
            CancellationToken token = (CancellationToken)tokenObj;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (this.process != null && !this.process.HasExited)
                    {
                        string output;
                        if (!string.IsNullOrEmpty(output = this.process.StandardOutput.ReadLine()))
                        {
                            if (!output.Equals("null", StringComparison.InvariantCultureIgnoreCase))
                            {
                                this.stdOutputEvent?.Invoke(this, output);
                            }
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                }
                finally
                {
                    this.stdOutputClosedEvent?.Invoke(this, EventArgs.Empty);
                }

            }
        }

        /// <summary>
        ///     Occurs when standard output is received.
        /// </summary>
        public event EventHandler<string> StandardOutputEvent
        {
            add => this.stdOutputEvent += value;
            remove => this.stdOutputEvent -= value;
        }

        /// <summary>
        ///     Occurs when standard output is closed.
        /// </summary>
        public event EventHandler StandardOutputClosedEvent
        {
            add => this.stdOutputClosedEvent += value;
            remove => this.stdOutputClosedEvent -= value;
        }

        public event PropertyChangedEventHandler InfoChangedEvent;

        internal void OnInfoChangedEvent(object sender, PropertyChangedEventArgs args)
        {
            this.InfoChangedEvent?.Invoke(sender, args);
        }
    }
}