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

namespace n0tFlix.Plugin.YoutubeDL.Services
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using n0tFlix.Plugin.YoutubeDL.Helpers;
    using Options;

    #endregion Using

    /// <summary>
    /// Service containing logic for retrieving information about videos / playlists
    /// </summary>
    internal static class InfoService
    {
        /// <summary>
        ///     Asynchronously retrieve video / playlist information
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        /// <returns>
        ///     An object containing the download information
        /// </returns>
        internal static async Task<DownloadInfo> GetDownloadInfoAsync(this YoutubeDL ydl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(ydl.VideoUrl))
            {
                return null;
            }

            List<DownloadInfo> infos = new List<DownloadInfo>();

            // Save the original options and set the ones we need
            string originalOptions = ydl.Options.Serialize();
            SetInfoOptions(ydl);

            // Save the original event delegates and clear the event handler
            Delegate[] originalDelegates = null;
            if (ydl.stdOutputEvent != null)
            {
                originalDelegates = ydl.stdOutputEvent.GetInvocationList();
                ydl.stdOutputEvent = null;
            }

            // Local function for easier event handling
            void ParseInfoJson(object sender, string output)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    infos.Add(DownloadInfo.CreateDownloadInfo(output));
                }
            }

            ydl.StandardOutputEvent += ParseInfoJson;

            // Set up the command
            PreparationService.SetupPrepare(ydl);

            // Download the info
            await DownloadService.DownloadAsync(ydl, cancellationToken);

            while ((!ydl.process.HasExited || infos.Count == 0) && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            // Set the info object
            ydl.Info = infos.Count > 1 ? new MultiDownloadInfo(infos) : infos[0];

            // Set options back to what they were
            ydl.Options = Options.Deserialize(originalOptions);

            // Clear the event handler
            ydl.stdOutputEvent = null;

            // Re-subscribe to each event delegate
            if (originalDelegates != null)
            {
                foreach (Delegate del in originalDelegates)
                {
                    ydl.StandardOutputEvent += (EventHandler<string>)del;
                }
            }

            return ydl.Info;
        }

        /// <summary>
        ///     Synchronously retrieve video / playlist information
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        /// <returns>
        ///     An object containing the download information
        /// </returns>
        internal static DownloadInfo GetDownloadInfo(this YoutubeDL ydl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(ydl.VideoUrl))
            {
                return null;
            }

            List<DownloadInfo> infos = new List<DownloadInfo>();

            // Save the original options and set the ones we need
            string originalOptions = ydl.Options.Serialize();
            SetInfoOptions(ydl);

            // Save the original event delegates and clear the event handler
            Delegate[] originalDelegates = null;
            if (ydl.stdOutputEvent != null)
            {
                originalDelegates = ydl.stdOutputEvent.GetInvocationList();
                ydl.stdOutputEvent = null;
            }

            // Local function for easier event handling
            void ParseInfoJson(object sender, string output)
            {
                infos.Add(DownloadInfo.CreateDownloadInfo(output));
            }

            ydl.StandardOutputEvent += ParseInfoJson;

            // Set up the command
            PreparationService.SetupPrepare(ydl);

            // Download the info
            DownloadService.Download(ydl, cancellationToken);

            while ((!ydl.process.HasExited || infos.Count == 0) && !cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(1);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            // Set the info object
            ydl.Info = infos.Count > 1 ? new MultiDownloadInfo(infos) : infos[0];

            // Set options back to what they were
            ydl.Options = Options.Deserialize(originalOptions);

            // Clear the event handler
            ydl.stdOutputEvent = null;

            // Re-subscribe to each event delegate
            if (originalDelegates != null)
            {
                foreach (Delegate del in originalDelegates)
                {
                    ydl.StandardOutputEvent += (EventHandler<string>)del;
                }
            }

            return ydl.Info;
        }

        /// <summary>
        ///     Asynchronously retrieve video / playlist information
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="url">
        ///     URL of video / playlist
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        /// <returns>
        ///     An object containing the download information
        /// </returns>
        internal static async Task<DownloadInfo> GetDownloadInfoAsync(this YoutubeDL ydl, string url, CancellationToken cancellationToken)
        {
            ydl.VideoUrl = url;
            await GetDownloadInfoAsync(ydl, cancellationToken);
            return ydl.Info;
        }

        /// <summary>
        ///     Synchronously retrieve video / playlist information
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="url">
        ///     URL of video / playlist
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        /// <returns>
        ///     An object containing the download information
        /// </returns>
        internal static DownloadInfo GetDownloadInfo(this YoutubeDL ydl, string url, CancellationToken cancellationToken)
        {
            ydl.VideoUrl = url;
            return GetDownloadInfo(ydl, cancellationToken);
        }

        private static void SetInfoOptions(YoutubeDL ydl)
        {
            Options infoOptions = new Options
            {
                VerbositySimulationOptions =
                {
                    DumpSingleJson = true,
                    Simulate = true
                },
                GeneralOptions =
                {
                    FlatPlaylist = !ydl.RetrieveAllInfo,
                    IgnoreErrors = true
                },
                AuthenticationOptions =
                {
                    Username = ydl.Options.AuthenticationOptions.Username,
                    Password = ydl.Options.AuthenticationOptions.Password,
                    NetRc = ydl.Options.AuthenticationOptions.NetRc,
                    VideoPassword = ydl.Options.AuthenticationOptions.VideoPassword,
                    TwoFactor = ydl.Options.AuthenticationOptions.TwoFactor
                },
                VideoFormatOptions =
                {
                    FormatAdvanced = ydl.Options.VideoFormatOptions.FormatAdvanced
                },
                WorkaroundsOptions =
                {
                    UserAgent = ydl.Options.WorkaroundsOptions.UserAgent
                }
            };

            if (ydl.Options.VideoFormatOptions.Format != Enums.VideoFormat.undefined)
            {
                infoOptions.VideoFormatOptions.Format = ydl.Options.VideoFormatOptions.Format;
            }

            ydl.Options = infoOptions;
        }
    }
}