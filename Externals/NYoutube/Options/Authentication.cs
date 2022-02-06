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

namespace NYoutubeDL.Options
{
    #region Using

    using Helpers;

    #endregion

    /// <summary>
    ///     Object containing Authentication parameters
    /// </summary>
    public class Authentication : OptionSection
    {
        [Option] internal readonly BoolOption netRc = new BoolOption("-n");

        [Option] internal readonly StringOption password = new StringOption("-p");

        [Option] internal readonly StringOption twoFactor = new StringOption("-2");

        [Option] internal readonly StringOption username = new StringOption("-u");

        [Option] internal readonly StringOption videoPassword = new StringOption("--video-password");

        /// <summary>
        ///     -n
        /// </summary>
        public bool NetRc
        {
            get => this.netRc.Value ?? false;
            set => this.SetField(ref this.netRc.Value, value);
        }

        /// <summary>
        ///     -p
        /// </summary>
        public string Password
        {
            get => this.password.Value;
            set => this.SetField(ref this.password.Value, value);
        }

        /// <summary>
        ///     -2
        /// </summary>
        public string TwoFactor
        {
            get => this.twoFactor.Value;
            set => this.SetField(ref this.twoFactor.Value, value);
        }

        /// <summary>
        ///     -u
        /// </summary>
        public string Username
        {
            get => this.username.Value;
            set => this.SetField(ref this.username.Value, value);
        }

        /// <summary>
        ///     --video-password
        /// </summary>
        public string VideoPassword
        {
            get => this.videoPassword.Value;
            set => this.SetField(ref this.videoPassword.Value, value);
        }
    }
}