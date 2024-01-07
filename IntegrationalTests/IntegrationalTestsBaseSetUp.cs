using DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationalTests;


public class IntegrationalTestsBaseSetUp
{
    private PostgreSqlContainerTest? Container { get; set; }
    protected AppDbContext? DbContext { get; set; }

    public IntegrationalTestsBaseSetUp()
    {
        Container = new PostgreSqlContainerTest();
        Container.InitializeAsync().Wait();
        DbContext = new AppDbContext(Container.CreateNewContextOptions());
        DbContext.Database.Migrate();    
    }
    
    
    [OneTimeTearDown]
    public void Dispose()
    {
        Container?.DisposeAsync().Wait();
        DbContext?.Dispose();
    }    
}
