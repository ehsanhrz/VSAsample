using System.Linq.Expressions;
using DbContext;
using Entities.Users;
using Features.Users.Exceptions;
using Features.Users.Models.Commands;
using Features.Users.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Features.Users.Services;

public class UserCud : IUserCUD
{
    private readonly AppDbContext context;

    public UserCud(AppDbContext context)
    {
        this.context = context;
    }
    public async Task CreateAsync(CreateUserCommand command)
    {
        var user = new User()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber
        };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateUserCommand command)
    {
       await context.Users.Where(u => u.Id == command.Id)
            .ExecuteUpdateAsync(u => 
                u.SetProperty(i => i.FirstName, command.FirstName)
                    .SetProperty(i => i.LastName, command.LastName)
                    .SetProperty(i => i.PhoneNumber, command.PhoneNumber)
                    .SetProperty(i => i.Email, command.Email));
    }
    public async Task DeleteAsync(DeleteUserCommand command)
    {
        await context.Users.Where(u => u.Id == command.Id).ExecuteDeleteAsync();
    }
}