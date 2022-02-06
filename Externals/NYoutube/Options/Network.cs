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
    ///     Object containing Network parameters
    /// </summary>
    public class Network : OptionSection
    {
        [Option] internal readonly BoolOption forceIpv4 = new BoolOption("-4");

        [Option] internal readonly BoolOption forceIpv6 = new BoolOption("-6");

        [Option] internal readonly StringOption proxy = new StringOption("--proxy");

        [Option] internal readonly IntOption socketTimeout = new IntOption("--socket-timeout");

        [Option] internal readonly StringOption sourceAddress = new StringOption("--source-address");

        /// <summary>
        ///     -4
        /// </summary>
        public bool ForceIpv4
        {
            get => this.forceIpv4.Value ?? false;
            set => this.SetField(ref this.forceIpv4.Value, value);
        }

        /// <summary>
        ///     -6
        /// </summary>
        public bool ForceIpv6
        {
            get => this.forceIpv6.Value ?? false;
            set => this.SetField(ref this.forceIpv6.Value, value);
        }

        /// <summary>
        ///     --proxy
        /// </summary>
        public string Proxy
        {
            get => this.proxy.Value;
            set => this.SetField(ref this.proxy.Value, value);
        }

        /// <summary>
        ///     --socket-timeout
        /// </summary>
        public int SocketTimeout
        {
            get => this.socketTimeout.Value ?? -1;
            set => this.SetField(ref this.socketTimeout.Value, value);
        }

        /// <summary>
        ///     --source-address
        /// </summary>
        public string SourceAddress
        {
            get => this.sourceAddress.Value;
            set => this.SetField(ref this.sourceAddress.Value, value);
        }
    }
}