using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Onyx.Contracts.Data.Entities;
using Onyx.Infrastructure.Tests;
using Onyx.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Onyx.EndToEndTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                   .AddEntityFrameworkInMemoryDatabase()
                   .BuildServiceProvider();

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DatabaseContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DatabaseContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        db.Products.AddRange(Products());
                        db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

        public static IEnumerable<Product> Products()
        {
            return new List<Product>
            {
               new Product("Product 1", "Red", "Category 1"),
               new Product("Product 1.1", "Red", "Category 1.1"),
               new Product("Product 2", "Blue", "Category 2"),
               new Product("Product 2", "Black", "Category 2")
            };
        }

        
    }
}
