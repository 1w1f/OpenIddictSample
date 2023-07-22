using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Controllers
{
    [Route("[User]")]
    public class UserController : ControllerBase
    {
        [HttpPost("/Create")]
        public async Task<ActionResult> CreateUser([FromServices] UserManager<AppUser> userManager)
        {
            var newUser = new AppUser()
            {
                UserName = "testUser"
            };
            await userManager.CreateAsync(newUser);
            return Ok();
        }
    }
}
