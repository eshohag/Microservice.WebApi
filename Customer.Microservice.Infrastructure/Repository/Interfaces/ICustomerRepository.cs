using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Microservice.Infrastructure.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Domain.Models.Customer>
    {
    }
}
