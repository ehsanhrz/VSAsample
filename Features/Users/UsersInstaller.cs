using Features.Users.Services;
using Features.Users.Services.Abstractions;

namespace Features.Users;

public static class UsersInstaller
{
    public static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGetUser, GetUser>();
        serviceCollection.AddScoped<IUserCUD, UserCud>();
    }
}