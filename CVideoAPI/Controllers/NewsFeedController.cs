using CVideoAPI.Cache;
using CVideoAPI.Datasets.NewsFeedSection;
using CVideoAPI.Services.NewsFeed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [Route("api/v1/newsfeed")]
    [ApiVersion("1.0")]
    [ApiController]
    public class NewsFeedController : Controller
    {
        private readonly INewsFeedService _newsFeedService;
        public NewsFeedController(INewsFeedService commonService)
        {
            _newsFeedService = commonService;
        }
        [AllowAnonymous]
        [HttpGet("sections")]
        [Cached(600)]
        public async Task<ActionResult<List<NewsFeedSectionDataset>>> GetNewsFeedSections(string lang = "vi")
        {
            return Ok(await _newsFeedService.GetNewsFeedSections(lang));
        }
    }
}
