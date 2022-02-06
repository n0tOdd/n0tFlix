using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Querying;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace n0tFlix.Plugin.YoutubeDL.API
{
    /// <summary>
    /// The open subtitles plugin controller.
    /// </summary>
    /// <summary>
    /// The open subtitles plugin controller.
    /// </summary>
    [ApiController]
    [Route("n0tFlix.Plugin.YoutubeDL")]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize(Policy = "DefaultAuthorization")]
    public class YoutubeDlController : ControllerBase
    {

        public class CollectInfo
        {
            public string URL { get; set; }
        }
        /// <summary>
        /// Validates login info.
        /// </summary>
        /// <remarks>
        /// Accepts plugin configuration as JSON body.
        /// </remarks>
        /// <response code="200">Login info valid.</response>
        /// <response code="400">Login info is missing data.</response>
        /// <response code="401">Login info not valid.</response>
        /// <param name="body">The request body.</param>
        /// <returns>
        /// An <see cref="NoContentResult"/> if the login info is valid, a <see cref="BadRequestResult"/> if the request body missing is data
        /// or <see cref="UnauthorizedResult"/> if the login info is not valid.
        /// </returns>
        [HttpPost()]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<JsonResult> Get([FromBody] CollectInfo body)
        {
            Console.WriteLine(body.URL.ToLower());

            NYoutubeDL.YoutubeDL youtubeDL = new NYoutubeDL.YoutubeDL(Plugin.Instance.Configuration.YoutubeDlFilePath);
      //      YoutubeDL youtubeDL = new YoutubeDL("/var/lib/jellyfin/plugins/YoutubeDL_1.0.0.0/youtube-dl");
            youtubeDL.Options.VerbositySimulationOptions.GetUrl = true;
      //      youtubeDL.Options.VerbositySimulationOptions.Simulate = true;
          //  youtubeDL.Options.VerbositySimulationOptions.SkipDownload = true;
            /*youtubeDL.Options.VerbositySimulationOptions.DumpSingleJson = true;
            youtubeDL.Options.VerbositySimulationOptions.PrintJson = true;
            youtubeDL.Options.GeoRestrictionOptions.GeoBypass = true;
            
            youtubeDL.Options.VerbositySimulationOptions.DumpJson = true;
            */
            StringBuilder sb = new StringBuilder();
            youtubeDL.StandardOutputEvent += (sender, output) => sb.AppendLine(output);
            youtubeDL.StandardErrorEvent += (sender, errorOutput) => sb.AppendLine(errorOutput);
            youtubeDL.PrepareDownload();
            var info = await youtubeDL.GetDownloadInfoAsync(body.URL);
           
            await youtubeDL.DownloadAsync(body.URL);
            
            Console.WriteLine(sb.ToString());
            return new JsonResult(sb.ToString());
        }
    }

}