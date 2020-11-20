using CVideoAPI.Cache;
using CVideoAPI.Context;
using CVideoAPI.Datasets.Question;
using CVideoAPI.Services.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/question-sets")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _qService;

        public ActionResult<QuestionSetDataset> HttpStatus { get; private set; }

        public QuestionController(IQuestionService qService)
        {
            _qService = qService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Cached(1200)]
        public async Task<ActionResult<List<QuestionSetDataset>>> GetQuestionSets([FromQuery] QuestionSetParam param)
        {
            return Ok(await _qService.GetQuestionSets(param));
        }
        [Authorize(Roles = CVideoConstant.Roles.Admin.Name)]
        [HttpPost]
        [CacheClearing]
        public async Task<ActionResult> CreateSet([FromBody] QuestionSetParam param)
        {
            if (await _qService.GetQuestionSet(param.SetName) != null)
            {
                return Conflict();
            }
            if (await _qService.CreateQuestionSet(param))
            {
                return Created("", null);
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Admin.Name)]
        [HttpPut("{id}")]
        [CacheClearing]
        public async Task<ActionResult> UpdateSet([FromRoute] int id, [FromBody] QuestionSetParam param)
        {
            if (id != param.SetId)
            {
                return Forbid();
            }
            QuestionSetDataset temp = await _qService.GetQuestionSet(param.SetName);
            if (temp != null && temp.SetId != param.SetId)
            {
                return Conflict();
            }
            if (await _qService.UpdateQuestionSet(param))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Admin.Name)]
        [HttpDelete("{id}")]
        [CacheClearing]
        public async Task<ActionResult> DeleteSet([FromRoute] int id)
        {
            if (await _qService.DeleteQuestionSet(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Cached(1200)]
        public async Task<ActionResult<QuestionSetDataset>> GetQuestionSet([FromRoute] int id)
        {
            QuestionSetDataset result = await _qService.GetQuestionSet(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = CVideoConstant.Roles.Admin.Name)]
        [HttpPost("{setId}/questions")]
        [CacheClearing]
        public async Task<ActionResult> CreateQuestion([FromRoute]int setId, [FromBody] QuestionParam param)
        {
            if (setId != param.SetId)
            {
                return Forbid();
            }
            if (await _qService.CreateQuestion(param))
            {
                return Created("", null);
            }
            return BadRequest();
        }
        [Authorize(Roles = CVideoConstant.Roles.Admin.Name)]
        [HttpPut("{setId}/questions/{id}")]
        [CacheClearing]
        public async Task<ActionResult> UpdateQuestion([FromRoute]int setId, [FromRoute] int id, [FromBody] QuestionParam param)
        {
            if (id != param.QuestionId || setId != param.SetId)
            {
                return Forbid();
            }
            if (await _qService.UpdateQuestion(param))
            {
                return NoContent();
            }
            return BadRequest();
        }

        [Authorize(Roles = CVideoConstant.Roles.Admin.Name)]
        [HttpDelete("{setId}/questions/{id}")]
        [CacheClearing]
        public async Task<ActionResult> DeleteQuestion([FromRoute] int setId, [FromRoute] int id)
        {
            QuestionDataset question = await _qService.GetQuestion(id);
            if (question == null)
            {
                return NotFound();
            }
            if (await _qService.DeleteQuestion(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
