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
    using System.Xml;
    #region Using

    using Helpers;
    

    #endregion

    public class Options
    {
        public AdobePass AdobePassOptions { get; private set; } = new AdobePass();

        public Authentication AuthenticationOptions { get; private set; } = new Authentication();

        public Download DownloadOptions { get; private set; } = new Download();

        public Filesystem FilesystemOptions { get; private set; } = new Filesystem();

        public General GeneralOptions { get; private set; } = new General();

        public GeoRestriction GeoRestrictionOptions { get; private set; } = new GeoRestriction();

        public Network NetworkOptions { get; private set; } = new Network();

        public PostProcessing PostProcessingOptions { get; private set; } = new PostProcessing();

        public Subtitle SubtitleOptions { get; private set; } = new Subtitle();

        public ThumbnailImages ThumbnailImagesOptions { get; private set; } = new ThumbnailImages();

        public VerbositySimulation VerbositySimulationOptions { get; private set; } = new VerbositySimulation();

        public VideoFormat VideoFormatOptions { get; private set; } = new VideoFormat();

        public VideoSelection VideoSelectionOptions { get; private set; } = new VideoSelection();

        public Workarounds WorkaroundsOptions { get; private set; } = new Workarounds();

        public void Clear()
        {
            this.AdobePassOptions = new AdobePass();
            this.AuthenticationOptions = new Authentication();
            this.DownloadOptions = new Download();
            this.FilesystemOptions = new Filesystem();
            this.GeneralOptions = new General();
            this.GeoRestrictionOptions = new GeoRestriction();
            this.NetworkOptions = new Network();
            this.PostProcessingOptions = new PostProcessing();
            this.SubtitleOptions = new Subtitle();
            this.ThumbnailImagesOptions = new ThumbnailImages();
            this.VerbositySimulationOptions = new VerbositySimulation();
            this.VideoFormatOptions = new VideoFormat();
            this.VideoSelectionOptions = new VideoSelection();
            this.WorkaroundsOptions = new Workarounds();
        }

        public static Options Deserialize(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<Options>(json);
        }
        
        public string Serialize()
        {
            return System.Text.Json.JsonSerializer.Serialize<Options>(this);
        }

        /// <summary>
        ///     Retrieves the options from each option section
        /// </summary>
        /// <returns>
        ///     The parameterized string of all the options
        /// </returns>
        public string ToCliParameters()
        {
            string parameters = this.AdobePassOptions.ToCliParameters() +
                                this.AuthenticationOptions.ToCliParameters() +
                                this.DownloadOptions.ToCliParameters() +
                                this.FilesystemOptions.ToCliParameters() +
                                this.GeneralOptions.ToCliParameters() +
                                this.GeoRestrictionOptions.ToCliParameters() +
                                this.NetworkOptions.ToCliParameters() +
                                this.PostProcessingOptions.ToCliParameters() +
                                this.SubtitleOptions.ToCliParameters() +
                                this.ThumbnailImagesOptions.ToCliParameters() +
                                this.VerbositySimulationOptions.ToCliParameters() +
                                this.VideoFormatOptions.ToCliParameters() +
                                this.VideoSelectionOptions.ToCliParameters() +
                                this.WorkaroundsOptions.ToCliParameters();

            // Remove extra spaces
            return parameters.RemoveExtraWhitespace();
        }
    }
}