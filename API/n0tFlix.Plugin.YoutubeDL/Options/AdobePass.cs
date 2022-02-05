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
    ///     Object containing Adobe Pass parameters
    /// </summary>
    public class AdobePass : OptionSection
    {
        [Option] internal readonly BoolOption listMso = new BoolOption("--ap-list-mso");

        [Option] internal readonly StringOption mso = new StringOption("--ap-mso");

        [Option] internal readonly StringOption password = new StringOption("--ap-password");

        [Option] internal readonly StringOption username = new StringOption("--ap-username");

        /// <summary>
        ///     --ap-list-mso
        /// </summary>
        public bool ListMso
        {
            get => this.listMso.Value ?? false;
            set => this.SetField(ref this.listMso.Value, value);
        }

        /// <summary>
        ///     --ap-mso
        /// </summary>
        public string Mso
        {
            get => this.mso.Value;
            set => this.SetField(ref this.mso.Value, value);
        }

        /// <summary>
        ///     --ap-password
        /// </summary>
        public string Password
        {
            get => this.password.Value;
            set => this.SetField(ref this.password.Value, value);
        }

        /// <summary>
        ///     --ap-username
        /// </summary>
        public string Username
        {
            get => this.username.Value;
            set => this.SetField(ref this.username.Value, value);
        }
    }
}