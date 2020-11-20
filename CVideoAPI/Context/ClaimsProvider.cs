using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CVideoAPI.Context
{
    public class ClaimsProvider : IGetClaimsProvider
    {
        public int UserId { get; private set; }
        public ClaimsProvider(IHttpContextAccessor accessor)
        {
            string userId = accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                UserId = int.Parse(userId);
            }
        }
    }
}
