using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindTraders.Core.ApplicationService;
using NorthwindTraders.Core.ApplicationService.Services;
using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;
using NorthwindTraders.Infrastructure.Data;
using NorthwindTraders.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;

namespace NorthwindTraders.RESTApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In-memory database
            //services.AddDbContext<NorthwindTradersContext>(
            //    opt => opt.UseInMemoryDatabase("ThaDB")
            //    );

            services.AddDbContext<NorthwindTradersContext>(
                opt => opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NorthwindTraders;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                );

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<NorthwindTradersContext>();
                    //var cust1 = ctx.Customers.Add(new Customer()
                    //{
                    //    CustomerID = "KEENE",
                    //    Address = "123 Fake St",
                    //    City = "Fake City",
                    //    CompanyName = "Keene Incorporated",
                    //    ContactName = "Bob Tester",
                    //    ContactTitle = "CEO",
                    //    Country = "USA",
                    //    Fax = "555-555-5555",
                    //    Phone = "555-555-4444",
                    //    PostalCode = "43434",
                    //    Region = "Ohio",
                    //    Orders = new List<Order>()
                    //}).Entity;
                    //var order1 = ctx.Orders.Add(new Order()
                    //{
                    //    Customer = cust1,
                    //    Employee = new Employee(),
                    //    Freight = 5.65,
                    //    Id = 1,
                    //    OrderDate = DateTime.Now.AddDays(-5),
                    //    RequiredDate = DateTime.Now.AddDays(10),
                    //    ShipAddress = "123 Fake St",
                    //    ShipCity = "Fake City",
                    //    ShipCountry = "USA",
                    //    ShipName = "Bob Tester",
                    //    ShippedDate = DateTime.Now.AddDays(1),
                    //    Shipper = new Shipper(),
                    //    ShipPostalCode = "43434",
                    //    ShipRegion = "Ohio"
                    //}).Entity;
                    //ctx.SaveChanges();
                }
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
