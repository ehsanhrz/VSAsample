using Features;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FunctionalTests;

public class FunctionalTestBaseSetup
{
    protected static WebApplicationFactory<Program> Factory { get; set; } = new();

    [OneTimeTearDown]
    public async Task ShutDownWebApplication()
    {
        await Factory.DisposeAsync();
    }
}