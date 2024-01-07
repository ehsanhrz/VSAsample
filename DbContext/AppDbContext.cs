using System.Reflection;
using DbContext.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserAddress> UserAddresses => Set<UserAddress>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}