using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Tools;
using System.Globalization;
using System.Security.Claims;

namespace server.Controllers
{
    [Authorize]
    [Route("[controller]/")]
    public class UserController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromServices] UserManager<AppUser> userManager, [FromBody] UserVo vo)
        {
            if (string.IsNullOrEmpty(vo.UserName)||string.IsNullOrEmpty(vo.Password)) return Ok("参数不合法");
            var user = await userManager.FindByIdAsync(vo.UserName);
            var newUser = new AppUser
            {
                UserName = vo.UserName,
            };
            if (user==null)
            {
                var identityResult = await userManager.CreateAsync(newUser, vo.Password);
                if (identityResult.Succeeded)
                {
                    var userSave = await userManager.FindByIdAsync(newUser.Id);
                    return Ok("创建成功");
                }
            }
            return Ok("用户已存在");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromServices] UserManager<AppUser> userManager, [FromServices] SignInManager<AppUser> signInManager, [FromBody] UserVo vo)
        {
            var result = await signInManager.PasswordSignInAsync(vo.UserName, vo.Password, true, false);
            if (result.Succeeded)
            {
                return Ok("success");
            }
            return Ok("fail");
        }
    }
}
