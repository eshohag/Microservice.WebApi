using ClientApps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApps.Controllers
{
    public class ProductController : BaseController
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = await HttpClientHelper.GetAsync<List<Product>>(url: "https://localhost:44382/gateway/product", token: Settings.JwtToken);
            return View(products);
        }
    }
}
