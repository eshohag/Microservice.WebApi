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
        public ICustomerRepository _customerRepository { get; }

        private readonly ITestManager _testManager;

        public CustomerController(ICustomerRepository customerRepository, ITestManager testManager)
        {
            _customerRepository = customerRepository;
            _testManager = testManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Domain.Models.Customer customer)
        {
            _customerRepository.Add(customer);
            return Ok(customer.Id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var test = _testManager.TestMessage();

            var customers = await _customerRepository.GetAllAsync();
            if (customers == null) return NotFound();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerRepository.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();
            return Ok(customer);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepository.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();
            _customerRepository.Remove(customer);
            return Ok(customer.Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Domain.Models.Customer customerData)
        {
            var customer = _customerRepository.Where(a => a.Id == id).FirstOrDefault();

            if (customer == null) return NotFound();
            else
            {
                customer.City = customerData.City;
                customer.Name = customerData.Name;
                customer.Contact = customerData.Contact;
                customer.Email = customerData.Email;
                return Ok(customer.Id);
            }
        }
    }
}