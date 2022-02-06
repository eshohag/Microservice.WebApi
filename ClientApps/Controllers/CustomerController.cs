using ClientApps.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApps.Controllers
{
    public class CustomerController : BaseController
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var customers = await HttpClientHelper.GetAsync<List<Customer>>(url: "https://localhost:44382/gateway/customer", token: Settings.JwtToken);

            return View(customers);
        }
    }
}
