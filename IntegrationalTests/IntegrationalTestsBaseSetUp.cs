using DbContext;
using Microsoft.EntityFrameworkCore;

namespace IntegrationalTests;

public class IntegrationalTestsBaseSetUp
{
    private PostgreSqlContainerTest? Container { get; set; }
    protected AppDbContext? DbContext { get; set; }
    
    [OneTimeSetUp]
    public void Setup()
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
