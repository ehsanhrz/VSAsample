using DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testcontainers.PostgreSql;

namespace FunctionalTests;

public class CustomWebApiForTests<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly PostgreSqlContainer postgreSqlContainer = new PostgreSqlBuilder().Build();

    private Task InitializeDatabaseContainerAsync()
    {
        return postgreSqlContainer.StartAsync();
    }

    public Task DisposeDatabaseContainerAsync()
    {
        return postgreSqlContainer.DisposeAsync().AsTask();
    }

    public override ValueTask DisposeAsync()
    {
        DisposeDatabaseContainerAsync().Wait();
        return base.DisposeAsync();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        InitializeDatabaseContainerAsync().Wait();
        
        builder.UseEnvironment("Production");
        var host = builder.Build();
        host.Start();

        var serviceProvider = host.Services;
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                
                services.AddDbContext<AppDbContext>(o =>
                {
                    o.UseNpgsql(postgreSqlContainer.GetConnectionString());
                });
                   
            });
        
    }
}