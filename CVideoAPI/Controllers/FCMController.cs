using CVideoAPI.Datasets.Device;
using CVideoAPI.Services.FCM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/fcm")]
    public class FCMController : Controller
    {
        private readonly IFCMService _fcmService;
        public FCMController(IFCMService fcmService)
        {
            _fcmService = fcmService;
        }
        [AllowAnonymous]
        [HttpPost("devices")]
        public async Task<IActionResult> CheckDevice([FromBody] DeviceDataset device)
        {
            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? userId = null;
            if (user != null)
            {
                userId = int.Parse(user);
            }
            if (await _fcmService.CheckDevice(userId, device.DeviceId))
            {
                return StatusCode(201);
            }
            return BadRequest();
        }
    }
}
