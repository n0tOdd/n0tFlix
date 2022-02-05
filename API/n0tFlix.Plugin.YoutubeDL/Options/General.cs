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
    ///     Object containing General parameters
    /// </summary>
    public class General : OptionSection
    {
        [Option] internal readonly BoolOption abortOnError = new BoolOption("--abort-on-error");

        [Option] internal readonly StringOption configLocation = new StringOption("--config-location");

        [Option] internal readonly StringOption defaultSearch = new StringOption("--default-search");

        [Option] internal readonly BoolOption dumpUserAgent = new BoolOption("--dump-user-agent");

        [Option] internal readonly BoolOption extractorDescriptions = new BoolOption("--extractor-descriptions");

        [Option] internal readonly BoolOption flatPlaylist = new BoolOption("--flat-playlist");

        [Option] internal readonly BoolOption forceGenericExtractor = new BoolOption("--force-generic-extractor");

        [Option] internal readonly BoolOption ignoreConfig = new BoolOption("--ignore-config");

        [Option] internal readonly BoolOption ignoreErrors = new BoolOption("-i");

        [Option] internal readonly BoolOption listExtractors = new BoolOption("--list-extractors");

        [Option] internal readonly BoolOption markWatched = new BoolOption("--mark-watched");

        [Option] internal readonly BoolOption noColor = new BoolOption("--no-color");

        [Option] internal readonly BoolOption noMarkWatched = new BoolOption("--no-mark-watched");

        [Option] internal readonly BoolOption update = new BoolOption("-U");

        [Option] internal readonly BoolOption version = new BoolOption("--version");

        /// <summary>
        ///     --abort-on-error
        /// </summary>
        public bool AbortOnError
        {
            get => this.abortOnError.Value ?? false;
            set => this.SetField(ref this.abortOnError.Value, value);
        }

        /// <summary>
        ///     --config-location
        /// </summary>
        public string ConfigLocation
        {
            get => this.configLocation.Value;
            set => this.SetField(ref this.configLocation.Value, value);
        }

        /// <summary>
        ///     --default-search
        /// </summary>
        public string DefaultSearch
        {
            get => this.defaultSearch.Value;
            set => this.SetField(ref this.defaultSearch.Value, value);
        }

        /// <summary>
        ///     --dump-user-agent
        /// </summary>
        public bool DumpUserAgent
        {
            get => this.dumpUserAgent.Value ?? false;
            set => this.SetField(ref this.dumpUserAgent.Value, value);
        }

        /// <summary>
        ///     --extractor-descriptions
        /// </summary>
        public bool ExtractorDescriptions
        {
            get => this.extractorDescriptions.Value ?? false;
            set => this.SetField(ref this.extractorDescriptions.Value, value);
        }

        /// <summary>
        ///     --flat-playlist
        /// </summary>
        public bool FlatPlaylist
        {
            get => this.flatPlaylist.Value ?? false;
            set => this.SetField(ref this.flatPlaylist.Value, value);
        }

        /// <summary>
        ///     --force-generic-extractor
        /// </summary>
        public bool ForceGenericExtractor
        {
            get => this.forceGenericExtractor.Value ?? false;
            set => this.SetField(ref this.forceGenericExtractor.Value, value);
        }

        /// <summary>
        ///     --ignore-config
        /// </summary>
        public bool IgnoreConfig
        {
            get => this.ignoreConfig.Value ?? false;
            set => this.SetField(ref this.ignoreConfig.Value, value);
        }

        /// <summary>
        ///     -i
        /// </summary>
        public bool IgnoreErrors
        {
            get => this.ignoreErrors.Value ?? false;
            set => this.SetField(ref this.ignoreErrors.Value, value);
        }

        /// <summary>
        ///     --list-extractors
        /// </summary>
        public bool ListExtractors
        {
            get => this.listExtractors.Value ?? false;
            set => this.SetField(ref this.listExtractors.Value, value);
        }

        /// <summary>
        ///     --mark-watched
        /// </summary>
        public bool MarkWatched
        {
            get => this.markWatched.Value ?? false;
            set => this.SetField(ref this.markWatched.Value, value);
        }

        /// <summary>
        ///     --no-color
        /// </summary>
        public bool NoColor
        {
            get => this.noColor.Value ?? false;
            set => this.SetField(ref this.noColor.Value, value);
        }

        /// <summary>
        ///     --no-mark-watched
        /// </summary>
        public bool NoMarkWatched
        {
            get => this.noMarkWatched.Value ?? false;
            set => this.SetField(ref this.noMarkWatched.Value, value);
        }

        /// <summary>
        ///     -U
        /// </summary>
        public bool Update
        {
            get => this.update.Value ?? false;
            set => this.SetField(ref this.update.Value, value);
        }

        /// <summary>
        ///     --version
        /// </summary>
        public bool Version
        {
            get => this.version.Value ?? false;
            set => this.SetField(ref this.version.Value, value);
        }
    }
}