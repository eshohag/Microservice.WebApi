using ClientApps.Entities;
using ClientApps.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientApps.Controllers
{
    public class CustomerController : BaseController
    {
        [Authorize]
        public async Task<IActionResult> Index([FromQuery] FilterOption filter)
        {
            var models = await HttpClientHelper.GetAsync<PaginatedList<Customer>>(url: "https://localhost:44382/gateway/customer/all?pageNumber=" + filter.PageNumber + "&pageSize=" + filter.PageSize, token: Settings.JwtToken);
            var userData = await HttpClientHelper.GetAsync<PaginatedList<Customer>>(url: "https://localhost:44382/gateway/customer/all?pageNumber=" + filter.PageNumber + "&pageSize=" + filter.PageSize, token: Settings.JwtToken);

            return View(models);
        }
    }
}
