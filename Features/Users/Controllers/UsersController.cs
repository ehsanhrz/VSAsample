using Features.Users.Models.Commands;
using Features.Users.Models.DTOs;
using Features.Users.Models.Queries;
using Features.Users.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Features.Users.Controllers;

[ApiController]
[Route("[Controller]/[Action]")]
public partial class UsersController : ControllerBase
{
    private readonly IGetUser getUser;
    private readonly IUserCUD userCud;

    public UsersController(IGetUser getUser, IUserCUD userCud)
    {
        this.getUser = getUser;
        this.userCud = userCud;
    }
    
    [HttpGet(Name = "GetUserByEmail")]
    public async Task<ActionResult<UserDto>> GetUserByEmail([FromQuery]GetUserByEmailQuery query)
    {
        var result = await getUser.GetUserByEmailAsync(query);
        return Ok(result);
    }

    [HttpGet(Name = "GetUserByPhoneNumber")]
    public async Task<ActionResult<UserDto>> GetUserByPhoneNumber([FromQuery] GetUserByPhoneNumberQuery query)
    {
        var result = await getUser.GetUserByPhoneNumberAsync(query);
        return Ok(query);
    }
    
    [HttpPost(Name = "Create")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        await userCud.CreateAsync(command);
        return Ok();
    }
    
    [HttpPost(Name = "Update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        await userCud.UpdateAsync(command);
        return Ok();
    }

    [HttpPost(Name = "Delete")] 
    public IActionResult Delete([FromBody] DeleteUserCommand command)
    {
        userCud.DeleteAsync(command);
        return Ok();
    }
}