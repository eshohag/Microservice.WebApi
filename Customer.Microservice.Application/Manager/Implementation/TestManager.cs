using Customer.Microservice.Application.Manager.Interfaces;
using System;

namespace Customer.Microservice.Application.Manager.Implementation
{
    public class TestManager : ITestManager
    {
        public string TestMessage()
        {
            return "Successfully Calling";
        }
    }
}
