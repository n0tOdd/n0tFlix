using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Querying;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n0tYoutubeDL.Core;
using n0tFlix.Plugin.YoutubeDL;

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
        public async Task<string> Get([FromBody] CollectInfo body)
        {

            if(!System.IO.File.Exists(Plugin.Instance.Configuration.YoutubeDlFilePath))
            {
                return "ERROR: Youtubedl can not be found, try restarting jellyfin it should download it for you then";
            }
            n0tYoutubeDL.Core.n0tYoutubeDL youtubeDL = new n0tYoutubeDL.Core.n0tYoutubeDL();
            youtubeDL.YoutubeDLPath = Plugin.Instance.Configuration.YoutubeDlFilePath;
            youtubeDL.PythonPath =  Plugin.Instance.Configuration.PythonPath;


            var res = await youtubeDL.RunVideoDataFetch(body.URL);
         

            return res.Data.PlayerUrl + " " + res.Data.Url;
        }
    }

}