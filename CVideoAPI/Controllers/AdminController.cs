using CVideoAPI.Datasets.Admin;
using CVideoAPI.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [Route("api/v1/admin")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [AllowAnonymous]
        [HttpGet("daily-register")]
        public Task<List<RegisterInDayDataset>> GetDailyRegister(string from, string to)
        {
            DateTime formatedFrom = new DateTime();
            DateTime formatedTo = new DateTime();
            if (Regex.IsMatch(from, "^\\d{4}\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])$") && Regex.IsMatch(to, "^\\d{4}\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])$"))
            {
                formatedFrom = DateTime.Parse(from);
                formatedTo = DateTime.Parse(to);
            }
            else
            {
                formatedFrom = DateTime.ParseExact(from, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                formatedTo = DateTime.ParseExact(to, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            //return _adminService.GetDailyRegister(from, to);
            return _adminService.GetDailyRegister(formatedFrom, formatedTo);
        }
        [AllowAnonymous]
        [HttpGet("daily-create-cvs")]
        public Task<List<CreatedCVsInDayDataset>> GetDailyCreateCVs(string from, string to)
        {
            DateTime formatedFrom = new DateTime();
            DateTime formatedTo = new DateTime();
            if (Regex.IsMatch(from, "^\\d{4}\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])$") && Regex.IsMatch(to, "^\\d{4}\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])$"))
            {
                formatedFrom = DateTime.Parse(from);
                formatedTo = DateTime.Parse(to);
            }
            else
            {
                formatedFrom = DateTime.ParseExact(from, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                formatedTo = DateTime.ParseExact(to, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            //return _adminService.GetDailyRegister(from, to);
            return _adminService.GetDailyCreatedCVs(formatedFrom, formatedTo);
        }
    }
}
