using System.Collections.Generic;
using System.Linq;

namespace Customer.Microservice.Application.Manager.Interfaces
{
    public interface ICustomerManager
    {
        List<Domain.Models.Customer> GetAll();
        IQueryable<Domain.Models.Customer> All();
    }
}
