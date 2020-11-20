using CVideoAPI.Datasets.Account;
using CVideoAPI.Datasets.Login;
using CVideoAPI.Helpers;
using CVideoAPI.Services.Authen;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CVideoAPI.Controllers
{
    [Route("api/v1/auth")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthenService _service;
        public AuthController(IAuthenService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<ActionResult> LoginGoogle([FromBody] LoginDataset loginDataset)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(loginDataset.Token);
                AccountDataset account = await _service.Login(decodedToken, loginDataset.Flg);
                if (account != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, account.AccountId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, account.Email),
                        new Claim(ClaimTypes.Role, account.Role.RoleName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.Settings.JwtSecret));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(AppSettings.Settings.Issuer,
                                                        AppSettings.Settings.Audience,
                                                        claims,
                                                        // expires: DateTime.Now.AddSeconds(55 * 60),
                                                        signingCredentials: creds);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
                else
                {
                    return Forbid();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
