using Features.Users.Services;

namespace Features.Users;

public static class UsersInstaller
{
    public static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<GetUser>();
        serviceCollection.AddScoped<UserCud>();
    }
}