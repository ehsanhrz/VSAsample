using DbContext;
using Features.Users.Models.DTOs;
using Features.Users.Models.Queries;
using Microsoft.EntityFrameworkCore;

namespace Features.Users.Services;

public class GetUser
{
    private readonly AppDbContext context;

    public GetUser(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<UserDto> GetUserByPhoneNumberAsync(GetUserByPhoneNumberQuery query)
    {
        return await context.Users.AsNoTracking().Select(u => new UserDto()
        {
            Id = u.Id,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            FirstName = u.FirstName,
            LastName = u.LastName
        }).SingleAsync(u => u.PhoneNumber == query.PhoneNumber);
        
    }

    public async Task<UserDto> GetUserByEmailAsync(GetUserByEmailQuery query)
    {
        return await context.Users.AsNoTracking().Select(u => new UserDto()
        {
            Id = u.Id,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            FirstName = u.FirstName,
            LastName = u.LastName
        }).SingleAsync(u => u.Email == query.Email);
    }
}