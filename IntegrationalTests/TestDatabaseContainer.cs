using DbContext;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace IntegrationalTests;

public sealed class PostgreSqlContainerTest 
{
    private readonly PostgreSqlContainer postgreSqlContainer = new PostgreSqlBuilder().Build();

    public Task InitializeAsync()
    {
        return postgreSqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return postgreSqlContainer.DisposeAsync().AsTask();
    }
    
    public DbContextOptions<AppDbContext> CreateNewContextOptions()
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseNpgsql(postgreSqlContainer.GetConnectionString());
        
        return builder.Options;
    }
}