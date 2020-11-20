using CVideoAPI.Cache;
using CVideoAPI.Context;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.Recruitment;
using CVideoAPI.Services.FCM;
using CVideoAPI.Services.Recruitment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [Route("api/v1/recruitment-posts")]
    [ApiVersion("1.0")]
    [ApiController]
    public class RecruitmentController : Controller
    {
        private readonly IRecruitmentService _recruitmentService;
        private readonly IFCMService _fcmService;
        public RecruitmentController(IRecruitmentService recruitmentService, IFCMService fcmService)
        {
            _recruitmentService = recruitmentService;
            _fcmService = fcmService;
        }
        [HttpGet]
        [AllowAnonymous]
        [Cached(600)]
        public async Task<ActionResult<List<CommonRecruitmentPostDataset>>> GetClassificatedRecruitmentPosts([FromQuery] RecruitmentPostRequestParam param)
        {
            if (!param.ValidateSalary)
            {
                return BadRequest(new { message = "Max salary must greater or equal to Min salary" });
            }
            return Ok(await _recruitmentService.GetListRecruitmentPost(param));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Cached(600)]
        public async Task<ActionResult<CommonRecruitmentPostDataset>> GetById(int id, string lang = "vi")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CommonRecruitmentPostDataset result = await _recruitmentService.GetRecruitmentPostById(id, lang);
            if (result == null)
            {
                return NotFound();
            }
            if (userId != null)
            {
                result.IsApplied = await _recruitmentService.IsUserApplied(id, int.Parse(userId));
            }
            return Ok(result);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpPost]
        [CacheClearing]
        public async Task<ActionResult<CommonRecruitmentPostDataset>> CreatePost([FromBody]NewRecruitmentPostParam param) 
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId != param.CompanyId)
            {
                return Forbid();
            }
            CommonRecruitmentPostDataset result = await _recruitmentService.InsertRecruitmentPost(param);
            if (result != null)
            {
                await _fcmService.SendMessage(userId, "Creating post succeed", "Recruitment post " + result.Title + " has created.");
                return Created("", result);
            } else
            {
                await _fcmService.SendMessage(userId, "Creating post failed", "Creating recruitment post failed.");
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpPut("{id}")]
        [CacheClearing]
        public async Task<ActionResult> UpdatePost([FromRoute]int id, [FromBody]RecruitmentPostToUpdateParam param)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (await _recruitmentService.UpdateRecruitmentPost(param))
            {
                await _fcmService.SendMessage(userId, "Updating post succeed", "Recruitment post id " + param.PostId + " updated.");
                return NoContent();
            } else
            {
                await _fcmService.SendMessage(userId, "Updating post failed", "Updating recruitment post id " + param.PostId + " failed.");
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPost("{postId}/applied-cvs")]
        [CacheClearing]
        public async Task<IActionResult> ApplyCV([FromRoute] int postId, [FromBody] AppliedCVParam cv)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (await _recruitmentService.ApplyCV(postId, userId, cv.CVId))
            {
                await _fcmService.SendMessage(userId, "Success", "CV " + cv.CVId.ToString() + " is applied");
                int hr = (await _recruitmentService.GetRecruitmentPostById(postId)).Company.Id;
                await _fcmService.SendMessage(hr, "CV Received", "CV " + cv.CVId.ToString() + " is applied to recruitment post " + postId);
                return Created("", cv);
            }
            else
            {
                await _fcmService.SendMessage(userId, "Failed", "CV " + cv.CVId.ToString() + " is failed");
            }
            return BadRequest();
        }

    }
}
