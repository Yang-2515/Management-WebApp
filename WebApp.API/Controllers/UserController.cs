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
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getCurrentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            int UserId = Convert.ToInt32(GetUserIdFromToken());
            var result = await _userService.GetUser(UserId);
            return Ok(result);
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
        protected string GetMethod()
        {
            return HttpContext.Request.Method;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AddUserRequest addUser)
        {
            await _userService.AddUserAsync(addUser);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync([FromBody]UpdateUserRequest updateUser)
        {
            await _userService.UpdateUserAsync(updateUser, GetUserIdFromToken());
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserAsync()
        {
            return Ok(await _userService.GetUserAysnc(GetUserIdFromToken()));
        }
    }
}
