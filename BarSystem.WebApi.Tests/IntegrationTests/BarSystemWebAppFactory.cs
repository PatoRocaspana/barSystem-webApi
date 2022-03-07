using BarSystem.WebApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
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
            if (db.Dishes.Any())
                db.Dishes.RemoveRange(db.Dishes);

            if (db.Drinks.Any())
                db.Drinks.RemoveRange(db.Drinks);

            if (db.Employees.Any())
                db.Employees.RemoveRange(db.Employees);

            db.Dishes.AddRange(SeedsDbTests.GetSeedingDishes());
            db.Drinks.AddRange(SeedsDbTests.GetSeedingDrinks());
            db.Employees.AddRange(SeedsDbTests.GetSeedingEmployees());

            db.SaveChanges();
        }
    }
}
