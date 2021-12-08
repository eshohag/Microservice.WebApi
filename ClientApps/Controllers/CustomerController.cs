using ClientApps.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace ClientApps.Controllers
{
    public class CustomerController : BaseController
    {
        [Authorize]
        public IActionResult Index()
        {
            var response = JsonDataHelper.GetJsonResponseData(model: null, url: "https://localhost:44382/gateway/customer", WebRequestMethods.Http.Get, token: Settings.JwtToken);
            var customers = JsonConvert.DeserializeObject<List<Customer>>(response);

            return View(customers);
        }
    }
}
