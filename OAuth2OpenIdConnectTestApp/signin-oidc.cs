using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth2OpenIdConnectTestApp
{
    public class signin_oidc : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
