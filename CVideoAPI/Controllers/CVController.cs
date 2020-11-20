using CVideoAPI.Cache;
using CVideoAPI.Context;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.CV.Section;
using CVideoAPI.Datasets.CV.Section.Field;
using CVideoAPI.Datasets.CV.SectionType;
using CVideoAPI.Datasets.Video;
using CVideoAPI.Services.CV;
using CVideoAPI.Services.Video;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [Route("api/v1/cvs")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CVController : Controller
    {
        private readonly ICVService _cvService;
        private readonly IVideoService _videoService;
        public CVController(ICVService cvService, IVideoService videoService)
        {
            _cvService = cvService;
            _videoService = videoService;
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name + "," +
                            CVideoConstant.Roles.Employer.Name + "," +
                            CVideoConstant.Roles.Admin.Name + ",")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CVDataset>> GetCV(int id, string lang = "vi")
        {
            CVDataset result = await _cvService.GetCVById(id, lang);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPost]
        public async Task<IActionResult> CreateCV([FromBody] NewCVParam cv)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (await _cvService.GetCVByName(cv.Title, userId) != null)
            {
                return BadRequest(new { message = "CV title is existed" });
            }
            CVDataset result = await _cvService.CreateCV(userId, cv.Title, cv.MajorId);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCV([FromRoute] int id, [FromBody] NewCVParam cv)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id != cv.CVId)
            {
                return Forbid();
            }
            if (await _cvService.GetCVById(cv.CVId) == null)
            {
                return BadRequest();
            }
            bool result = await _cvService.UpdateCV(cv);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCV(int id)
        {
            if (await _cvService.GetCVById(id) == null)
            {
                return NotFound();
            }
            await _cvService.DeleteCV(id);
            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet("section-types")]
        [Cached(1200)]
        public async Task<IActionResult> GetCVSectionTypes(string lang = "vi")
        {
            return Ok(await _cvService.GetCVSectionTypes(lang));
        }
        [AllowAnonymous]
        [HttpGet("section-types/{id}")]
        public async Task<IActionResult> GetCVSectionTypes(int id, string lang = "vi")
        {
            CVSectionTypeDataset result = await _cvService.GetCVSectionType(id, lang);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPost("{cvId}/sections")]
        public async Task<IActionResult> CreateSection([FromRoute] int cvId, [FromBody] NewSectionParam section)
        {
            if (cvId != section.CVId)
            {
                return Forbid();
            }
            try
            {
                CVSectionDataset result = await _cvService.CreateSection(section);
                if (result != null)
                {
                    return Created("", result);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("confict"))
                {
                    return Conflict();
                }
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("{cvId}/sections")]
        public async Task<ActionResult<List<CVSectionDataset>>> GetCVSections([FromRoute] int cvId)
        {
            if (await _cvService.GetCVById(cvId) == null)
            {
                return NotFound();
            }
            return await _cvService.GetCVSections(cvId);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("{cvId}/sections/{sectionId}")]
        public async Task<ActionResult<CVSectionDataset>> GetCVSection([FromRoute] int cvId, [FromRoute] int sectionId)
        {
            if (await _cvService.GetCVById(cvId) == null)
            {
                return NotFound();
            }
            return await _cvService.GetCVSection(cvId, sectionId);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPut("{cvId}/sections/{sectionId}")]
        public async Task<IActionResult> UpdateSection([FromRoute] int cvId, [FromRoute] int sectionId, [FromBody] NewSectionParam section)
        {
            if (cvId != section.CVId || sectionId != section.SectionId)
            {
                return Forbid();
            }
            if (await _cvService.GetCVSection(cvId, sectionId) == null)
            {
                return NotFound();
            }
            if (await _cvService.UpdateSection(section))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpDelete("{cvId}/sections/{sectionId}")]
        public async Task<IActionResult> DeleteSection([FromRoute] int cvId, [FromRoute] int sectionId)
        {
            if (await _cvService.GetCVSection(cvId, sectionId) == null)
            {
                return NotFound();
            }
            if (await _cvService.DeleteSection(sectionId))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("{cvId}/sections/{sectionId}/fields")]
        public async Task<ActionResult<List<CVFieldDataset>>> GetCVFields([FromRoute] int cvId, [FromRoute] int sectionId)
        {
            if (await _cvService.GetCVSection(cvId, sectionId) == null)
            {
                return NotFound();
            }
            return await _cvService.GetSectionFields(sectionId);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("{cvId}/sections/{sectionId}/fields/{fieldId}")]
        public async Task<ActionResult<CVFieldDataset>> GetCVField([FromRoute] int cvId, [FromRoute] int sectionId, [FromRoute] int fieldId)
        {
            if (await _cvService.GetCVSection(cvId, sectionId) == null)
            {
                return NotFound();
            }
            return await _cvService.GetSectionField(sectionId, fieldId);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPost("{cvId}/sections/{sectionId}/fields")]
        public async Task<ActionResult<CVFieldDataset>> CreateSectionField([FromRoute] int cvId, [FromRoute] int sectionId, [FromBody] CVFieldDataset field)
        {
            if (await _cvService.GetCVSection(cvId, sectionId) == null)
            {
                return NotFound();
            }
            CVFieldDataset result = await _cvService.CreateField(sectionId, field);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPut("{cvId}/sections/{sectionId}/fields/{fieldId}")]
        public async Task<IActionResult> UpdateField([FromRoute] int cvId, [FromRoute] int sectionId, [FromRoute] int fieldId, [FromBody] CVFieldDataset updateField)
        {
            CVSectionDataset section = await _cvService.GetCVSection(cvId, sectionId);
            CVFieldDataset field = await _cvService.GetSectionField(sectionId, fieldId);
            if (section == null && field == null)
            {
                return NotFound();
            }
            if (await _cvService.UpdateField(updateField))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpDelete("{cvId}/sections/{sectionId}/fields/{fieldId}")]
        public async Task<IActionResult> DeleteField([FromRoute] int cvId, [FromRoute] int sectionId, [FromRoute] int fieldId)
        {
            if (await _cvService.GetCVSection(cvId, sectionId) == null &&
                await _cvService.GetSectionField(sectionId, fieldId) == null)
            {
                return NotFound();
            }
            if (await _cvService.DeleteField(fieldId))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("{cvId}/sections/{sectionId}/videos")]
        public async Task<ActionResult<List<VideoDataset>>> GetSectionVideos([FromRoute] int cvId, [FromRoute] int sectionId, [FromQuery] int? styleId)
        {
            if (await _cvService.GetCVSection(cvId, sectionId) == null)
            {
                return NotFound();
            }
            return await _videoService.GetVideos(cvId, sectionId, styleId);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("{cvId}/sections/{sectionId}/videos/{videoId}")]
        public async Task<ActionResult<List<VideoDataset>>> GetSectionVideos([FromRoute] int cvId, [FromRoute] int sectionId, [FromRoute] int videoId)
        {
            if (await _cvService.GetCVSection(cvId, sectionId) == null)
            {
                return NotFound();
            }
            VideoDataset result = await _videoService.GetVideoById(videoId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPost("{cvId}/sections/{sectionId}/videos")]
        public async Task<ActionResult<VideoDataset>> CreateVideo([FromRoute] int cvId, [FromRoute] int sectionId, [FromBody] VideoUploadParam video)
        {
            if (await _cvService.GetCVSection(cvId, sectionId) == null)
            {
                return NotFound();
            }
            if (video.SectionId != sectionId)
            {
                return Forbid();
            }
            VideoDataset result = await _videoService.InsertVideo(video);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest(new { message = "Upload video failed" });
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpDelete("{cvId}/sections/{sectionId}/videos/{videoId}")]
        public async Task<ActionResult<VideoDataset>> CreateVideo([FromRoute] int cvId, [FromRoute] int sectionId, [FromRoute] int videoId)
        {
            VideoDataset video = await _videoService.GetVideoById(videoId);
            if (video == null)
            {
                return NotFound();
            }
            if (video.CVId != cvId || video.SectionId != sectionId)
            {
                return Forbid();
            }
            if (await _videoService.DeleteVideo(videoId))
            {
                return NoContent();
            }
            return BadRequest(new { message = "Deleting video failed" });
        }
    }
}
