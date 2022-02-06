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

namespace NYoutubeDL.Services
{
    #region Using

    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Helpers;

    #endregion

    /// <summary>
    ///     Service containing download functionality
    /// </summary>
    internal static class DownloadService
    {
        /// <summary>
        ///     Asynchronously download a video/playlist
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        internal static async Task DownloadAsync(this YoutubeDL ydl, CancellationToken cancellationToken)
        {
            cancellationToken.Register(() =>
            {
                Cancel(ydl);
            });

            if (!ydl.isGettingInfo)
            {
                ydl.IsDownloading = true;
            }

            if (ydl.processStartInfo == null)
            {
                ydl.isGettingInfo = true;
                await PreparationService.PrepareDownloadAsync(ydl, cancellationToken);

                if (ydl.processStartInfo == null)
                {
                    throw new NullReferenceException();
                }

                ydl.isGettingInfo = false;
            }

            SetupDownload(ydl, cancellationToken);

            await ydl.process?.WaitForExitAsync(cancellationToken);

            if (!ydl.isGettingInfo)
            {
                ydl.IsDownloading = false;
                ydl.processStartInfo = null;
            }
        }

        /// <summary>
        ///     Synchronously download a video/playlist
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        internal static void Download(this YoutubeDL ydl, CancellationToken cancellationToken)
        {
            cancellationToken.Register(() =>
            {
                Cancel(ydl);
            });

            if (!ydl.isGettingInfo)
            {
                ydl.IsDownloading = true;
            }

            if (ydl.processStartInfo == null)
            {
                ydl.isGettingInfo = true;
                PreparationService.PrepareDownload(ydl, cancellationToken);

                if (ydl.processStartInfo == null)
                {
                    throw new NullReferenceException();
                }

                ydl.isGettingInfo = false;
            }

            SetupDownload(ydl, cancellationToken);

            ydl.process?.WaitForExit();

            if (!ydl.isGettingInfo)
            {
                ydl.IsDownloading = false;
                ydl.processStartInfo = null;
            }
        }

        /// <summary>
        ///     Asynchronously download a video/playlist
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="url">
        ///     The video / playlist URL to download
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        internal static async Task DownloadAsync(this YoutubeDL ydl, string url, CancellationToken cancellationToken)
        {
            ydl.VideoUrl = url;
            await DownloadAsync(ydl, cancellationToken);
        }

        /// <summary>
        ///     Synchronously download a video/playlist
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="url">
        ///     The video / playlist URL to download
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        internal static void Download(this YoutubeDL ydl, string url, CancellationToken cancellationToken)
        {
            ydl.VideoUrl = url;
            Download(ydl, cancellationToken);
        }

        private static void SetupDownload(YoutubeDL ydl, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            ydl.process = new Process { StartInfo = ydl.processStartInfo, EnableRaisingEvents = true };

            ydl.stdOutputTokenSource = new CancellationTokenSource();
            ydl.stdErrorTokenSource = new CancellationTokenSource();

            ydl.process.Exited += (sender, args) => ydl.KillStandardEventHandlers();

            // Note that synchronous calls are needed in order to process the output line by line.
            // Asynchronous output reading results in batches of output lines coming in all at once.
            // The following two threads convert synchronous output reads into asynchronous events.

            ThreadPool.QueueUserWorkItem(ydl.StandardOutput, ydl.stdOutputTokenSource.Token);
            ThreadPool.QueueUserWorkItem(ydl.StandardError, ydl.stdErrorTokenSource.Token);

            if (ydl.Info.set)
            {
                ydl.StandardOutputEvent += (sender, output) => ydl.Info.ParseOutput(sender, output.Trim());
                ydl.StandardErrorEvent += (sender, output) => ydl.Info.ParseError(sender, output.Trim());
            }

            ydl.process.Start();
            ydl.downloadProcessID = ydl.process.Id;
        }

        private static void Cancel(YoutubeDL ydl, int count = 0)
        {
            try
            {
                ydl.StopProcess();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n{ex}\n\n");
            }
            finally
            {
                ydl.isGettingInfo = false;
                ydl.IsDownloading = false;
            }
        }
    }
}