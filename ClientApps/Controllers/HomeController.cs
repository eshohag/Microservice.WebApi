using ClientApps.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
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
            var generateToken = JsonDataHelper.GenerateTokenJsonDataPost(jsonData: JsonConvert.SerializeObject(new { userName = "test", password = "test" }), url: "https://localhost:44382/gateway/users/authenticate");

            var jsonResponse = JsonDataHelper.GetJsonData(url: "https://localhost:44382/gateway/users/getall", token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MzY4ODI4NDUsImV4cCI6MTYzNjg4Mzc0NSwiaWF0IjoxNjM2ODgyODQ1fQ.6GZp7nmMbLt1TGP_svCF8S9ibgVengpY9bfZKO-UKRo");

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri("http://localhost:9000");
            var jwt = "";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");


            var usersResponse = client.GetAsync("/customers").Result;
            var users = usersResponse.Content.ReadAsStringAsync().Result;

            var res = client.GetAsync("/customers/1").Result;
            var responseResultFor = res.Content.ReadAsStringAsync().Result;

            return View();
        }

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
