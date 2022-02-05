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
    using System;
    using System.ComponentModel;

    #region Using

    using MediaBrowser.Common;
    using MediaBrowser.Controller;
    using MediaBrowser.Model;

    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using MediaBrowser.Model.IO;

    #endregion Using

    /// <summary>
    ///     Service containing logic for preparing a youtube-dl command
    /// </summary>
    public static class PreparationService
    {
        /// <summary>
        ///     Asynchronously prepare a youtube-dl command
        /// </summary>
        /// <param name="ydl">
        ///     The client.
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        /// <returns>
        ///     The youtube-dl command that will be executed
        /// </returns>
        internal static async Task<string> PrepareDownloadAsync(this YoutubeDL ydl, CancellationToken cancellationToken)
        {
            if (!ydl.Info.set)
            {
                Delegate[] originalDelegates = null;
                if (ydl.Info.propertyChangedEvent != null)
                {
                    originalDelegates = ydl.Info.propertyChangedEvent.GetInvocationList();
                }

                ydl.Info = await InfoService.GetDownloadInfoAsync(ydl, cancellationToken) ?? new DownloadInfo();

                if (originalDelegates != null)
                {
                    foreach (Delegate del in originalDelegates)
                    {
                        ydl.Info.PropertyChanged += (PropertyChangedEventHandler)del;
                    }
                }

                ydl.Info.set = true;
                ydl.Info.PropertyChanged += ydl.OnInfoChangedEvent;
            }

            SetupPrepare(ydl);

            return ydl.RunCommand;
        }

        /// <summary>
        ///     synchronously prepare a youtube-dl command
        /// </summary>
        /// <param name="ydl">
        ///     The client.
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token
        /// </param>
        /// <returns>
        ///     The youtube-dl command that will be executed
        /// </returns>
        internal static string PrepareDownload(this YoutubeDL ydl, CancellationToken cancellationToken)
        {
            if (!ydl.Info.set)
            {
                Delegate[] originalDelegates = null;
                if (ydl.Info.propertyChangedEvent != null)
                {
                    originalDelegates = ydl.Info.propertyChangedEvent.GetInvocationList();
                }

                ydl.Info = InfoService.GetDownloadInfo(ydl, cancellationToken) ?? new DownloadInfo();

                if (originalDelegates != null)
                {
                    foreach (Delegate del in originalDelegates)
                    {
                        ydl.Info.PropertyChanged += (PropertyChangedEventHandler)del;
                    }
                }

                ydl.Info.set = true;
                ydl.Info.PropertyChanged += ydl.OnInfoChangedEvent;
            }

            SetupPrepare(ydl);

            return ydl.RunCommand;
        }

        /// <summary>
        ///     Setup a youtube-dl command using the given options
        /// </summary>
        /// <param name="ydl">
        ///     Client with configured options
        /// </param>
        internal static void SetupPrepare(YoutubeDL ydl)
        {
            string urls = string.IsNullOrWhiteSpace(ydl.VideoUrl) ? string.Empty : string.Join(" ", ydl.VideoUrl.Split(null).Select(url => $"\"{url}\""));
            string arguments = ydl.Options.ToCliParameters() + " " + urls;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ydl.processStartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Plugin.Instance.DataFolderPath, "youtube-dl.exe"),
                    Arguments = arguments,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };
            }
            else
            {
                ydl.processStartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Plugin.Instance.DataFolderPath, "youtube-dl"),
                    Arguments = arguments,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };
            }
            if (!string.IsNullOrWhiteSpace(ydl.PythonPath))
            {
                ydl.processStartInfo.Arguments = ydl.YoutubeDlPath + " " + ydl.processStartInfo.Arguments;
                ydl.processStartInfo.FileName = ydl.PythonPath;
            }

            if (string.IsNullOrWhiteSpace(ydl.processStartInfo.FileName))
            {
                throw new FileNotFoundException("youtube-dl not found on path!");
            }

            if (!File.Exists(ydl.processStartInfo.FileName))
            {
                throw new FileNotFoundException($"{ydl.processStartInfo.FileName} not found!");
            }

            ydl.RunCommand = ydl.processStartInfo.FileName + " " + ydl.processStartInfo.Arguments;
        }
    }
}