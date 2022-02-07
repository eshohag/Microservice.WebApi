using Customer.Microservice.Application.Manager.Interfaces;
using Customer.Microservice.Filters;
using Customer.Microservice.Helpers;
using Customer.Microservice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly IUriService _uriService;

        public CustomerController(ICustomerManager customerManager, IUriService uriService)
        {
            _customerManager = customerManager;
            _uriService = uriService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var customers = _customerManager.GetAll();
            if (customers == null) return NotFound();
            return Ok(customers);
        }

        [HttpGet("all")]
        public async Task<IActionResult> All([FromQuery] FilterOption filter)
        {
            var data = _customerManager.All();
            if (data == null) return NotFound();

            var pagedData = await data
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();
            var totalRecords = await data.CountAsync();

            var pagedReponse = PaginationHelper.CreatePagedReponse<Domain.Models.Customer>(pagedData, filter, totalRecords, _uriService, Request.Path.Value);
            return Ok(pagedReponse);
        }
    }
}