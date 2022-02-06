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

    using System.Linq;
    using Helpers;

    #endregion

    /// <summary>
    ///     Object containing Filesystem parameters
    /// </summary>
    public class Filesystem : OptionSection
    {
        [Option] internal readonly IntOption autoNumberSize = new IntOption("--autonumber-size");

        [Option] internal readonly IntOption autoNumberStart = new IntOption("--autonumber-start");

        [Option] internal readonly StringOption batchFile = new StringOption("-a");

        [Option] internal readonly StringOption cacheDir = new StringOption("--cache-dir");

        [Option] internal readonly BoolOption continueOpt = new BoolOption("-C");

        [Option] internal readonly StringOption cookies = new StringOption("--cookies");

        [Option] internal readonly BoolOption id = new BoolOption("--id");

        [Option] internal readonly StringOption loadInfoJson = new StringOption("--load-info-json");

        [Option] internal readonly BoolOption noCacheDir = new BoolOption("--no-cache-dir");

        [Option] internal readonly BoolOption noContinue = new BoolOption("--no-continue");

        [Option] internal readonly BoolOption noMtime = new BoolOption("--no-mtime");

        [Option] internal readonly BoolOption noOverwrites = new BoolOption("-w");

        [Option] internal readonly BoolOption noPart = new BoolOption("--no-part");

        [Option] internal readonly StringOption output = new StringOption("-o");

        [Option] internal readonly BoolOption restrictFilenames = new BoolOption("--restrict-filenames");

        [Option] internal readonly BoolOption rmCacheDir = new BoolOption("--rm-cache-dir");

        [Option] internal readonly BoolOption writeAnnotations = new BoolOption("--write-annotations");

        [Option] internal readonly BoolOption writeDescription = new BoolOption("--write-description");

        [Option] internal readonly BoolOption writeInfoJson = new BoolOption("--write-info-json");

        /// <summary>
        ///     --autonumber-size
        /// </summary>
        public int AutoNumberSize
        {
            get => this.autoNumberSize.Value ?? 5;
            set => this.SetField(ref this.autoNumberSize.Value, value);
        }

        /// <summary>
        ///     --autonumber-start
        /// </summary>
        public int AutoNumberStart
        {
            get => this.autoNumberStart.Value ?? 1;
            set => this.SetField(ref this.autoNumberStart.Value, value);
        }

        /// <summary>
        ///     -a
        /// </summary>
        public string BatchFile
        {
            get => this.batchFile.Value;
            set => this.SetField(ref this.batchFile.Value, value);
        }

        /// <summary>
        ///     --cache-dir
        /// </summary>
        public string CacheDir
        {
            get => this.cacheDir.Value;
            set => this.SetField(ref this.cacheDir.Value, value);
        }

        /// <summary>
        ///     -c
        /// </summary>
        public bool Continue
        {
            get => this.continueOpt.Value ?? false;
            set => this.SetField(ref this.continueOpt.Value, value);
        }

        /// <summary>
        ///     --cookies
        /// </summary>
        public string Cookies
        {
            get => this.cookies.Value;
            set => this.SetField(ref this.cookies.Value, value);
        }

        /// <summary>
        ///     --id
        /// </summary>
        public bool Id
        {
            get => this.id.Value ?? false;
            set => this.SetField(ref this.id.Value, value);
        }

        /// <summary>
        ///     --load-info-json
        /// </summary>
        public string LoadInfoJson
        {
            get => this.loadInfoJson.Value;
            set => this.SetField(ref this.loadInfoJson.Value, value);
        }

        /// <summary>
        ///     --no-cache-dir
        /// </summary>
        public bool NoCacheDir
        {
            get => this.noCacheDir.Value ?? false;
            set => this.SetField(ref this.noCacheDir.Value, value);
        }

        /// <summary>
        ///     --no-continue
        /// </summary>
        public bool NoContinue
        {
            get => this.noContinue.Value ?? false;
            set => this.SetField(ref this.noContinue.Value, value);
        }

        /// <summary>
        ///     --no-mtime
        /// </summary>
        public bool NoMtime
        {
            get => this.noMtime.Value ?? false;
            set => this.SetField(ref this.noMtime.Value, value);
        }

        /// <summary>
        ///     -w
        /// </summary>
        public bool NoOverwrites
        {
            get => this.noOverwrites.Value ?? false;
            set => this.SetField(ref this.noOverwrites.Value, value);
        }

        /// <summary>
        ///     --no-part
        /// </summary>
        public bool NoPart
        {
            get => this.noPart.Value ?? false;
            set => this.SetField(ref this.noPart.Value, value);
        }

        /// <summary>
        ///     -o
        /// </summary>
        public string Output
        {
            get => this.output.Value;
            set => this.SetField(ref this.output.Value, value);
        }

        /// <summary>
        ///     --restrict-filenames
        /// </summary>
        public bool RestrictFilenames
        {
            get => this.restrictFilenames.Value ?? false;
            set => this.SetField(ref this.restrictFilenames.Value, value);
        }

        /// <summary>
        ///     --rm-cache-dir
        /// </summary>
        public bool RmCacheDir
        {
            get => this.rmCacheDir.Value ?? false;
            set => this.SetField(ref this.rmCacheDir.Value, value);
        }

        /// <summary>
        ///     --write-annotations
        /// </summary>
        public bool WriteAnnotations
        {
            get => this.writeAnnotations.Value ?? false;
            set => this.SetField(ref this.writeAnnotations.Value, value);
        }

        /// <summary>
        ///     --write-description
        /// </summary>
        public bool WriteDescription
        {
            get => this.writeDescription.Value ?? false;
            set => this.SetField(ref this.writeDescription.Value, value);
        }

        /// <summary>
        ///     --write-info-json
        /// </summary>
        public bool WriteInfoJson
        {
            get => this.writeInfoJson.Value ?? false;
            set => this.SetField(ref this.writeInfoJson.Value, value);
        }
    }
}