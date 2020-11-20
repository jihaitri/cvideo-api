using CVideoAPI.Cache;
using CVideoAPI.Datasets.Major;
using CVideoAPI.Services.Major;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/majors")]
    public class MajorController : Controller
    {
        private readonly IMajorService _majorService;
        public MajorController(IMajorService majorService)
        {
            _majorService = majorService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Cached(1200)]
        public async Task<ActionResult<List<MajorDataset>>> GetMajors(string lang = "vi")
        {
            return await _majorService.GetMajors(lang);
        }
    }
}
