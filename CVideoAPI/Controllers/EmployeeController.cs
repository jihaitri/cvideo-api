using CVideoAPI.Cache;
using CVideoAPI.Context;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.Employee;
using CVideoAPI.Datasets.Recruitment;
using CVideoAPI.Services.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [Route("api/v1/employees")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("current-employee/info")]
        public async Task<ActionResult<EmployeeDataset>> GetCurrentEmployee()
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            EmployeeDataset result = await _employeeService.GetById(id);
            return Ok(result);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpPut("current-employee/info")]
        public async Task<ActionResult<EmployeeDataset>> UpdateCurrentEmployee(EmployeeInfoParam param)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (param.AccountId != id)
            {
                return Forbid();
            }
            var result = await _employeeService.UpdateInfo(param);
            if (result != null)
            {
                return NoContent();
            }
            return BadRequest(new { message = "Update fail" });
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("current-employee/applied-jobs")]
        public Task<List<CommonRecruitmentPostDataset>> GetAppliedJobs([FromQuery] RecruitmentPostRequestParam param)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return _employeeService.GetAppliedJobs(id, param);
        }
        [Authorize(Roles = CVideoConstant.Roles.Employee.Name)]
        [HttpGet("current-employee/cvs")]
        public Task<List<CVDataset>> GetListCVs([FromQuery] CVRequestParam param)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return _employeeService.GetListCVs(id, param);
        }
    }
}
