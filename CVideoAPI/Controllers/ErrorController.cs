using Microsoft.AspNetCore.Mvc;

namespace CVideoAPI.Controllers
{
    [ApiController]
    public class ErrorController : Controller
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error() => Problem();
    }
}
