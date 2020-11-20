using CVideoAPI.Context;
using CVideoAPI.Datasets;
using CVideoAPI.Datasets.Application;
using CVideoAPI.Datasets.Company;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.Recruitment;
using CVideoAPI.Services.Employer;
using CVideoAPI.Services.Recruitment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [Route("api/v1/employers")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EmployerController : Controller
    {
        private readonly IEmployerService _employerService;
        private readonly IRecruitmentService _recruitmentService;
        public EmployerController(IEmployerService employerService, IRecruitmentService recruitmentService)
        {
            _employerService = employerService;
            _recruitmentService = recruitmentService;
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpPut("current-employer/info")]
        public async Task<ActionResult> UpdateInfo(CompanyUpdateParam param)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id != param.CompanyId)
            {
                return Forbid();
            }
            if (await _employerService.UpdateInfo(param))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpGet("current-employer/info")]
        public async Task<ActionResult<CompanyDataset>> GetInfo()
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CompanyDataset result = await _employerService.GetInfo(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpGet("current-employer/statistics")]
        public async Task<ActionResult<CompanyStatisticsDataset>> GetStatistics()
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CompanyStatisticsDataset result = await _employerService.GetStatistics(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpGet("current-employer/recruitment-posts")]
        public async Task<ActionResult<List<PostStaticsDataset>>> GetRecruitmentPosts([FromQuery] CommonParam param)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _recruitmentService.GetPostStatisticsList(id, param));
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpGet("current-employer/recruitment-posts/{postId}")]
        public async Task<ActionResult<PostStaticsDataset>> GetRecruitmentPost([FromRoute]int postId, string lang = "vi")
        {
            PostStaticsDataset result = await _recruitmentService.GetPostStatisticsById(postId, lang);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpGet("current-employer/recruitment-posts/{postId}/applied-cvs")]
        public async Task<ActionResult<List<ApplicationDataset>>> GetAppliedCVs([FromRoute] int postId, [FromQuery] ApplicationParam param)
        {
            return Ok(await _recruitmentService.GetAppliedCVs(postId, param));
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpGet("current-employer/recruitment-posts/{postId}/applied-cvs/{applicationId}")]
        public async Task<ActionResult<ApplicationDataset>> GetAppliedCV([FromRoute] int postId, [FromRoute] int applicationId)
        {
            ApplicationDataset result = await _recruitmentService.GetAppliedCV(applicationId);
            if (result != null)
            {
                if (result.PostId != postId)
                {
                    return NotFound();
                }
                await _employerService.ViewApplication(applicationId);
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employer.Name)]
        [HttpGet("current-employer/applied-cvs")]
        public async Task<ActionResult<List<ApplicationDataset>>> GetAllAppliedCVs([FromQuery] ApplicationParam param)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _recruitmentService.GetAllAppliedCVs(id, param));
        }

    }
}
