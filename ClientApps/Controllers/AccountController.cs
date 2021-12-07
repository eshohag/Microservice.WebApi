using ClientApps.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClientApps.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {

        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = "/")
        {
            var model = new LoginModel();
            model.ReturnUrl = ReturnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel() { UserId = 1, Username = model.UserName, Role = "User", Password = model.Password };

                if (user == null)
                {
                    //Add logic here to display some message to user
                    ViewBag.Message = "Invalid Credential";
                    return View(model);
                }
                else
                {
                    //A claim is a statement about a subject by an issuer and
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.
                    var claims = new List<Claim>() {
                        new Claim("UserId",Convert.ToString(user.UserId)),
                        new Claim("UserName",user.Username),
                        new Claim("Role",user.Role),
                        new Claim("Developer","Shohag")
                    };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = model.RememberLogin });

                    return LocalRedirect(model.ReturnUrl);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //HttpContext.Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);

            //Redirect to home page
            return LocalRedirect("/");
        }
    }
}
