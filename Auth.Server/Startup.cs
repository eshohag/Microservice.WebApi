using Auth.Server.Controllers;
using Auth.Server.Entities;
using Auth.Server.Helpers;
using Auth.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Text.Json.Serialization;

namespace Auth.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.IgnoreNullValues = true;
            });

            //services.AddDbContext<DataContext>();   //For InMemory Database
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthServerDB")));
            services.AddCors();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth.Server", Version = "v1" });
            });
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
            if (env.IsDevelopment())
            {
                if (context.Users.Count() == 0)
                    CreateTestUser(context);

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth.Server v1"));
            }

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(x => x.MapControllers());
        }

        private void CreateTestUser(DataContext context)
        {
            // add hardcoded test user to db on startup
            var testUser = new User
            {
                FirstName = "Test",
                LastName = "User",
                Username = "test",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("test")
            };
            context.Users.Add(testUser);
            context.SaveChanges();
        }
    }
}
