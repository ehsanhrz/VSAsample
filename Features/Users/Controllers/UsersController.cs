﻿using Features.Users.Models.Commands;
using Features.Users.Models.DTOs;
using Features.Users.Models.Queries;
using Features.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace Features.Users.Controllers;

[ApiController]
[Route("[Controller]/[Action]")]
public class UsersController : ControllerBase
{
    private readonly GetUser getUser;
    private readonly UserCud userCud;

    public UsersController(GetUser getUser, UserCud userCud)
    {
        this.getUser = getUser;
        this.userCud = userCud;
    }
    
    [HttpGet(Name = "GetUserByEmail")]
    public async Task<ActionResult<UserDto>> GetUserByEmail([FromQuery] GetUserByEmailQuery query)
    {
        var result = await getUser.GetUserByEmailAsync(query);
        return Ok(result);
    }

    [HttpGet(Name = "GetUserByPhoneNumber")]
    public async Task<ActionResult<UserDto>> GetUserByPhoneNumber([FromQuery] GetUserByPhoneNumberQuery query)
    {
        var result = await getUser.GetUserByPhoneNumberAsync(query);
        return Ok(result);
    }

    [HttpGet(Name = "GetUserById")]
    public async Task<ActionResult<UserDto>> GetUserById([FromQuery] GetUserByIdQuery query)
    {
        var result = await getUser.GetUserByIdAsync(query);
        return Ok(result);
    }
    
    [HttpPost(Name = "Create")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        await userCud.CreateAsync(command);
        return Ok();
    }
    
    [HttpPut(Name = "Update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        await userCud.UpdateAsync(command);
        return Ok();
    }

    [HttpDelete(Name = "Delete")] 
    public async Task<IActionResult> Delete([FromQuery] DeleteUserCommand command)
    {
        await userCud.DeleteAsync(command);
        return Ok();
    }
}