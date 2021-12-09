using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Microservice.Application.Manager.Implementation;
using Customer.Microservice.Application.Manager.Interfaces;
using Customer.Microservice.Infrastructure;
using Customer.Microservice.Infrastructure.Repository;
using Customer.Microservice.Infrastructure.Repository.Implementation;
using Customer.Microservice.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Customer.Microservice
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<DbContext, ApplicationDbContext>();

            services.AddScoped<ITestManager, TestManager>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\Customer.Microservice.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Customer Microservice API",
                });
            });
            #endregion
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                if (context.Customers.Count() == 0)
                    CreateInitialTestCustomers(context);

                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer.Microservice");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void CreateInitialTestCustomers(ApplicationDbContext context)
        {
            // add hardcoded test user to db on startup
            var customers = new List<Domain.Models.Customer>() {
                new Domain.Models.Customer() { Name = "Shohag", Contact = "0192837376484", City="Dhaka", Email="shohag@gmail.com" },
                new Domain.Models.Customer() { Name = "Arif", Contact = "0192837376484", City="Dhaka", Email="arif@gmail.com" },
                new Domain.Models.Customer() { Name = "Masud", Contact = "0192837376484", City="Dhaka", Email="masud@gmail.com" }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
