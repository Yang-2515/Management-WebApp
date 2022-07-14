using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.API.DTOs.Users;
using WebApp.API.Services.Users;

namespace WebApp.API.Controllers
{
    [Route("api/auth")]

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IdentityService _identityService;
        public AuthController(IdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            var result = await _identityService.LoginAsync(loginModel);
            return Ok(result);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenModel request)
        {
            var result = await _identityService.RefreshTokenAsync(request);
            return Ok(result);
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await _identityService.LogoutAsync(GetUserIdFromToken());
            return Ok();
        }
        protected long GetUserIdFromToken()
        {
            long UserId = 0;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string strUserId = identity.FindFirst("UserId").Value;
                        long.TryParse(strUserId, out UserId);

                    }
                }
                return UserId;
            }
            catch
            {
                return UserId;
            }
        }
    }
}
