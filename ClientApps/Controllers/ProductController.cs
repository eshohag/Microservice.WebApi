using ClientApps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace ClientApps.Controllers
{
    public class ProductController : BaseController
    {
        [Authorize]
        public IActionResult Index()
        {
            var response = JsonDataHelper.GetJsonResponseData(model: null, url: "https://localhost:44382/gateway/product", WebRequestMethods.Http.Get, token: Settings.JwtToken);
            var products = JsonConvert.DeserializeObject<List<Product>>(response);

            return View(products);
        }
    }
}
