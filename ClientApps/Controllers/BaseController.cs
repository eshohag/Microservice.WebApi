using ClientApps.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ClientApps.Controllers
{
    public class BaseController : Controller
    {
        protected UserPrincipal Settings
        {
            get
            {
                return new UserPrincipal()
                {
                    UserId =Convert.ToInt32(User.Claims?.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value),  
                    UserName = User.Claims?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value,
                    JwtToken = User.Claims?.FirstOrDefault(x => x.Type.Equals("JwtToken", StringComparison.OrdinalIgnoreCase))?.Value,
                    FullName = User.Claims?.FirstOrDefault(x => x.Type.Equals("FullName", StringComparison.OrdinalIgnoreCase))?.Value,
                    Role = User.Claims?.FirstOrDefault(x => x.Type.Equals("Role", StringComparison.OrdinalIgnoreCase))?.Value,
                };
            }
        }
    }
}
