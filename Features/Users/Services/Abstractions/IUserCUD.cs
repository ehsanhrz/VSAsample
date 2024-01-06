using Features.Users.Models.Commands;

namespace Features.Users.Services.Abstractions;

public interface IUserCUD
{
    Task CreateAsync(CreateUserCommand command);

    Task UpdateAsync(UpdateUserCommand command);

    Task DeleteAsync(DeleteUserCommand command);
}