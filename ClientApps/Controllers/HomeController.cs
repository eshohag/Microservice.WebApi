using ClientApps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace ClientApps.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Customers()
        {
            ViewBag.Customers = JsonDataHelper.GetJsonResponseData(model: null, url: "https://localhost:44382/gateway/customer", WebRequestMethods.Http.Get, token: Settings.JwtToken);
            //var authenticateUserInfo = JsonConvert.DeserializeObject<AuthenticateResponse>(authResponse);

            return View();
        }
        [Authorize]
        public IActionResult Products()
        {
            ViewBag.Products = JsonDataHelper.GetJsonResponseData(model: null, url: "https://localhost:44382/gateway/product", WebRequestMethods.Http.Get, token: Settings.JwtToken);

            return View();
        }

        [Authorize]
        public IActionResult Confidential()
        {
            return View();
        }
    }
}
