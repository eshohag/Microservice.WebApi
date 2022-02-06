using Customer.Microservice.Filters;
using System;

namespace Customer.Microservice.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(FilterOption filter, string route);
    }
}
