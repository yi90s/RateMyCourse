using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using cRegis.Core.Data;
using cRegis.IntegrationTest.Infrastructure;

namespace cRegis.Tests
{
    public class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DataContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<DataContextTest>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DataContextTest>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<TestApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    db.SaveChanges();

                }
            });
        }
    }
}
