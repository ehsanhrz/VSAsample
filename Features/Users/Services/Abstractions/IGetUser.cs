using Features.Users.Models.DTOs;
using Features.Users.Models.Queries;

namespace Features.Users.Services.Abstractions;

public interface IGetUser
{
    Task<UserDto> GetUserByPhoneNumberAsync(GetUserByPhoneNumberQuery query);
    Task<UserDto> GetUserByEmailAsync(GetUserByEmailQuery query);
}