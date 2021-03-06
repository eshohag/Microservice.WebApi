using Customer.Microservice.Application.Manager.Interfaces;
using Customer.Microservice.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Customer.Microservice.Application.Manager.Implementation
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IQueryable<Domain.Models.Customer> All()
        {
            return _customerRepository.All();
        }

        public List<Domain.Models.Customer> GetAll()
        {
            var model = _customerRepository.All().ToList();
            return model;
        }
    }
}
