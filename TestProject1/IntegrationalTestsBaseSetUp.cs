using DbContext;
using Microsoft.EntityFrameworkCore;

namespace TestProject1;


public class IntegrationalTestsBaseSetUp
{
    private PostgreSqlContainerTest? Container { get; set; }
    public AppDbContext? DbContext { get; set; }

    public IntegrationalTestsBaseSetUp()
    {
        SetDbContextForTest().Wait();
    }
    
    public async Task SetDbContextForTest()
    {
        
        Container = new PostgreSqlContainerTest();
        await Container.InitializeAsync();
        DbContext = new AppDbContext(Container.CreateNewContextOptions());
        await DbContext.Database.MigrateAsync();    
        
    }
    
    
    public async Task Dispose()
    {
        await Container?.DisposeAsync()!;
        await DbContext?.DisposeAsync().AsTask()!;
    }    
}
