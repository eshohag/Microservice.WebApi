using Customer.Microservice.Application.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerManager.GetAll();
            if (customers == null) return NotFound();
            return Ok(customers);
        }
    }
}