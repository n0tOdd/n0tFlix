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
    #region Using

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public static class Extensions
    {
        internal static bool ExistsOnPath(this FileInfo fileInfo)
        {
            return !string.IsNullOrWhiteSpace(fileInfo.GetFullPath());
        }

        /// <summary>
        ///     Attempts to resolve the path of a given file info into a fully absolute path
        /// </summary>
        /// <param name="fileInfo">
        ///     File with relative path
        /// </param>
        /// <returns>
        ///     File's absolute path
        /// </returns>
        internal static string GetFullPath(this FileInfo fileInfo)
        {
            if (File.Exists(fileInfo.Name))
            {
                return Path.GetFullPath(fileInfo.Name);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string filePath = fileInfo.Name + ".exe";
                if (File.Exists(filePath))
                {
                    return Path.GetFullPath(filePath);
                }
            }

            string environmentVariable = Environment.GetEnvironmentVariable("PATH");
            if (environmentVariable != null)
            {
                foreach (string path in environmentVariable.Split(Path.PathSeparator))
                {
                    string fullPath = Path.Combine(path, fileInfo.Name);
                    if (File.Exists(fullPath))
                    {
                        return fullPath;
                    }

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        fullPath += ".exe";
                        if (File.Exists(fullPath))
                        {
                            return fullPath;
                        }
                    }
                }
            }

            return null;
        }

        internal static string RemoveExtraWhitespace(this string str)
        {
            return string.Join(" ", str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        ///     Waits asynchronously for the process to exit.
        /// </summary>
        /// <param name="process">The process to wait for cancellation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token. If invoked, the task will return
        ///     immediately as canceled.
        /// </param>
        /// <returns>
        ///     A Task representing waiting for the process to end.
        /// </returns>
        internal static async Task WaitForExitAsync(this Process process,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<bool>();

            void Process_Exited(object sender, EventArgs e)
            {
                tcs.TrySetResult(true);
            }

            process.EnableRaisingEvents = true;
            process.Exited += Process_Exited;

            try
            {
                if (process.HasExited)
                {
                    return;
                }

                using (cancellationToken.Register(() => tcs.TrySetCanceled()))
                {
                    await tcs.Task;
                }
            }
            finally
            {
                process.Exited -= Process_Exited;
            }
        }
    }
}