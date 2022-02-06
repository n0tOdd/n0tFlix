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
    ///     Object containing Thumbnail Images parameters
    /// </summary>
    public class ThumbnailImages : OptionSection
    {
        [Option] internal readonly BoolOption listThumbnails = new BoolOption("--list-thumbnail");

        [Option] internal readonly BoolOption writeAllThumbnails = new BoolOption("--write-all-thumbnails");

        [Option] internal readonly BoolOption writeThumbnail = new BoolOption("--write-thumbnail");

        /// <summary>
        ///     --list-thumbnails
        /// </summary>
        public bool ListThumbnails
        {
            get => this.listThumbnails.Value ?? false;
            set => this.SetField(ref this.listThumbnails.Value, value);
        }

        /// <summary>
        ///     --write-all-thumbnails
        /// </summary>
        public bool WriteAllThumbnails
        {
            get => this.writeAllThumbnails.Value ?? false;
            set => this.SetField(ref this.writeAllThumbnails.Value, value);
        }

        /// <summary>
        ///     --write-thumbnail
        /// </summary>
        public bool WriteThumbnail
        {
            get => this.writeThumbnail.Value ?? false;
            set => this.SetField(ref this.writeThumbnail.Value, value);
        }
    }
}