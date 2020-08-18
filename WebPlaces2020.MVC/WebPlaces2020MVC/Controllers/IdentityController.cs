using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebPlaces2020.CLI.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        public IActionResult Get()
        {
        
            
            
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value});
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");

            // or

            return SignOut("Cookies", "oidc");
        }
    }
}
