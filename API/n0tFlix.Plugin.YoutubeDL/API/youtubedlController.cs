using System;
using System.Collections.Generic;
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
using n0tFlix.Plugin.YoutubeDL.Models;

namespace n0tFlix.Plugin.YoutubeDL.API
{
    /// <summary>
    /// The open subtitles plugin controller.
    /// </summary>
    [Route("n0tFlix.Plugin.YoutubeDL")]
    public class YoutubeDlController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Get([FromQuery] string? URL)
        {
            return new JsonResult(URL);
        }
    }

}