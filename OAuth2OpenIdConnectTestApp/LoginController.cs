using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth2OpenIdConnectTestApp
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("LoginPage")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("External")]
        public IActionResult External(string provider)
        {
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "/home/HomeIndex"
            };

            return new ChallengeResult(provider, authProperties);
        }
    }
}
