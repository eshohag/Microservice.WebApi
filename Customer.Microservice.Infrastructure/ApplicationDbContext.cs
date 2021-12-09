using Microsoft.EntityFrameworkCore;
using System;

namespace Customer.Microservice.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Domain.Models.Customer> Customers { get; set; } 
    }
}
