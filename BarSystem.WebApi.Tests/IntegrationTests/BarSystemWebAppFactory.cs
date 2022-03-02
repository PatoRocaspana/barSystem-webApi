using BarSystem.WebApi.Data;
using BarSystem.WebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarSystem.WebApi.Tests.IntegrationTests
{
    public class BarSystemWebAppFactory<TEntryPoint>
        : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<BarSystemDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<BarSystemDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryBarSystemTests");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<BarSystemDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<BarSystemWebAppFactory<TEntryPoint>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

        private void InitializeDbForTests(BarSystemDbContext db)
        {
            if(db.Dishes.Any())
                db.Dishes.RemoveRange(db.Dishes);

            db.Dishes.AddRange(GetSeedingDishes());
            db.SaveChanges();
        }

        private List<Dish> GetSeedingDishes()
        {
            var dishA = new Dish()
            {
                Id = 1,
                Name = "NameA",
                Description = "Description of dishA",
                Price = 100,
                Stock = 10,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(35),
                IsReady = false
            };

            var dishB = new Dish()
            {
                Id = 2,
                Name = "NameB",
                Description = "Description of dishB",
                Price = 200,
                Stock = 20,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(45),
                IsReady = true
            };

            var dishList = new List<Dish>()
            {
                dishA,
                dishB
            };

            return dishList;
        }
    }
}
