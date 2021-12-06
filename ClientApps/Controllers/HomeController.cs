using ClientApps.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
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
            var authenticateUserInfo = JsonConvert.DeserializeObject<AuthenticateResponse>(JsonDataHelper.GetJsonResponseData(model: new { userName = "test", password = "test" }, url: "https://localhost:44382/gateway/users/authenticate", WebRequestMethods.Http.Post));
            var jsonResponse = JsonDataHelper.GetJsonResponseData(model:null, url: "https://localhost:44382/gateway/users/getall", WebRequestMethods.Http.Get, token: authenticateUserInfo.JwtToken);
            var customers = JsonDataHelper.GetJsonResponseData(model: null, url: "https://localhost:44382/gateway/customer", WebRequestMethods.Http.Get, token: authenticateUserInfo.JwtToken);

            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
