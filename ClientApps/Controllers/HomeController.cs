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
            var authenticateUserInfo = JsonConvert.DeserializeObject<AuthenticateResponse>(JsonDataHelper.GenerateTokenJsonDataPost(jsonData: JsonConvert.SerializeObject(new { userName = "test", password = "test" }), url: "https://localhost:44382/gateway/users/authenticate"));

            var jsonResponse = JsonDataHelper.GetJsonData(url: "https://localhost:44382/gateway/users/getall", token: authenticateUserInfo.JwtToken);
            var customers = JsonDataHelper.GetJsonData(url: "https://localhost:44382/gateway/customers/getall", token: authenticateUserInfo.JwtToken);






            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri("http://localhost:44382/");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer { authenticateUserInfo.JwtToken}");

            var usersResponse = client.GetAsync("/gateway/customer").Result;
            var users = usersResponse.Content.ReadAsStringAsync().Result;

            var res = client.GetAsync("/gateway/customers/1").Result;
            var responseResultFor = res.Content.ReadAsStringAsync().Result;

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
