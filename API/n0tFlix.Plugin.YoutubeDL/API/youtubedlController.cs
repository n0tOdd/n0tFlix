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
using n0tFlix.Plugin.YoutubeDL.Helpers;
using n0tFlix.Plugin.YoutubeDL.Models;

namespace n0tFlix.Plugin.YoutubeDL.API
{
    /// <summary>
    /// The open subtitles plugin controller.
    /// </summary>
    [Route("n0tFlix.Plugin.YoutubeDL")]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize(Policy = "DefaultAuthorization")]
    public class YoutubeDlController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Get([FromQuery] string? URL)
        {
            byte[] data = Convert.FromBase64String(URL);
            string decodedString = Encoding.UTF8.GetString(data);
            YoutubeDL youtubeDL = new YoutubeDL(Path.Combine("/var/lib/jellyfin/plugins/YoutubeDL_1.0.0.0/", "youtube-dl"));
            youtubeDL.Options.VerbositySimulationOptions.GetUrl = true;
            youtubeDL.Options.VerbositySimulationOptions.Simulate = true;
            youtubeDL.Options.VerbositySimulationOptions.DumpSingleJson = true;
            StringBuilder sb = new StringBuilder();
            youtubeDL.StandardOutputEvent += (sender, output) => sb.AppendLine(output);
            youtubeDL.StandardErrorEvent += (sender, errorOutput) => sb.AppendLine(errorOutput);
            youtubeDL.VideoUrl = decodedString;
            youtubeDL.Download(decodedString);
           
            return Ok(new JsonResult(sb.ToString()));
        }
    }

}