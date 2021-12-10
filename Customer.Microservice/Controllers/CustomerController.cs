using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Microservice.Application;
using Customer.Microservice.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Customer.Microservice.Application.Manager.Interfaces;
using Customer.Microservice.Infrastructure.Repository.Interfaces;

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
        public async Task<IActionResult> GetAll()
        {
            var customers = _customerManager.GetAll();
            if (customers == null) return NotFound();
            return Ok(customers);
        }
    }
}