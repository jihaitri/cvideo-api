using CVideoAPI.Cache;
using CVideoAPI.Context;
using CVideoAPI.Datasets.Video;
using CVideoAPI.Services.BlobStorage;
using CVideoAPI.Services.Video;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/videos")]
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;
        public VideoController(IVideoService videoService, IBlobService blobService)
        {
            _videoService = videoService;
        }
        /// <summary>
        /// Get list styles of video
        /// </summary>
        /// <response code="200">Return all stylea of video</response>
        [AllowAnonymous]
        [HttpGet("styles")]
        [Cached(600)]
        public async Task<ActionResult<List<VideoStyleDataset>>> GetStyles()
        {
            return Ok(await _videoService.GetStyles());
        }
    }
}
