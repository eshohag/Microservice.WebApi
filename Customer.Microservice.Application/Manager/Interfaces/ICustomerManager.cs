using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Microservice.Application.Manager.Interfaces
{
    public interface ICustomerManager
    {
        List<Domain.Models.Customer> GetAll();
    }
}
