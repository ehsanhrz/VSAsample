using System.Net;
using System.Net.Http.Json;
using System.Text;
using Features;
using Features.Users.Models.Commands;
using Features.Users.Models.DTOs;
using Newtonsoft.Json;

namespace FunctionalTests.User;

[Collection("FTests")]
public class UsersControllerTests
{
    private readonly HttpClient client;

    public UsersControllerTests(CustomWebApiForTests<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task Test_Create_User_Command()
    {
        var httpContent = CreateUserCreateCommandDummyObject();
        
        var result = await client.PostAsync("Users/Create",httpContent);
        
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    private static StringContent CreateUserCreateCommandDummyObject()
    {
        var command = new CreateUserCommand()
        {
            FirstName = "string",
            LastName = "string",
            Email = "string",
            PhoneNumber = "string"
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
        return httpContent;
    }

    [Fact]
    public async Task Test_Update_User_Command()
    {
        // a
        var actualEmail = "string2";
        var changedEmail = "string2356";
        await CreateUserForUpdateMethodTest(actualEmail);

        var resultGetUserByEmail = await GetCreatedUserByEmail(actualEmail);

        // a
        var updatedUserId = await UpdateCreatedUserAndReturnId(resultGetUserByEmail, changedEmail);

        var updatedUser = await GetUpdatedUserById(updatedUserId);

        // a
        Assert.Equal(updatedUser.Email, changedEmail);


    }

    private async Task<UserDto> GetUpdatedUserById(long updatedUserId)
    {
        var updatedUserRequest =
            await client.GetAsync($"{UserControllersEndPoints.GetUserById}?UserId={updatedUserId}");
        var updatedUser = await updatedUserRequest.Content.ReadFromJsonAsync<UserDto>();
        return updatedUser;
    }

    private async Task<long> UpdateCreatedUserAndReturnId(UserDto resultGetUserByEmail, string changedEmail)
    {
        var updateUserCommand = new UpdateUserCommand()
        {
            Id = resultGetUserByEmail.Id,
            Email = changedEmail,
            PhoneNumber = resultGetUserByEmail.PhoneNumber,
            LastName = resultGetUserByEmail.LastName,
            FirstName = resultGetUserByEmail.FirstName
        };
        
        var updateUserCommandHttpContent = new StringContent(JsonConvert.SerializeObject(updateUserCommand),Encoding.UTF8, "application/json");
        var updateUserCommandResult = await client.PutAsync(UserControllersEndPoints.Update,updateUserCommandHttpContent);
        Assert.Equal(HttpStatusCode.OK, updateUserCommandResult.StatusCode);
        var updatedUserId = updateUserCommand.Id;
        return updatedUserId;
    }

    private async Task<UserDto> GetCreatedUserByEmail(string actualEmail)
    {
        var resultGetUserByEmailRequest = await client.GetAsync($"{UserControllersEndPoints.GetUserByEmail}?Email={actualEmail}");
        var resultGetUserByEmail = await resultGetUserByEmailRequest.Content.ReadFromJsonAsync<UserDto>();
        
        Assert.Equal(resultGetUserByEmail.Email, actualEmail);
        return resultGetUserByEmail;
    }

    private async Task CreateUserForUpdateMethodTest(string actualEmail)
    {
        var createUserCommand = new CreateUserCommand()
        {
            FirstName = "string2",
            LastName = "string2",
            Email = actualEmail,
            PhoneNumber = "string2"
        };

        var createUserCommandHttpContent = new StringContent(JsonConvert.SerializeObject(createUserCommand), Encoding.UTF8, "application/json");
        
        var resultCreateUserCommand = await client.PostAsync(UserControllersEndPoints.Create,createUserCommandHttpContent);
        
        Assert.Equal(HttpStatusCode.OK, resultCreateUserCommand.StatusCode);
    }

    [Fact]
    public async Task Test_Delete_User_Command()
    {
        // a
        var email = "string3";
        var resultCreateUserCommand = await CreateUserForDeleteUserTest(email);

        var createdUser = await GetCreatedUserByEmail(email);
        // a
        var resultDeleteUserCommandRequest = await client.DeleteAsync($"{UserControllersEndPoints.Delete}?Id={createdUser.Id}");
        
        // a
        Assert.Equal(HttpStatusCode.OK, resultCreateUserCommand.StatusCode);
    }

    private async Task<HttpResponseMessage> CreateUserForDeleteUserTest(string email)
    {
        var createUserCommand = new CreateUserCommand()
        {
            FirstName = "string3",
            LastName = "string3",
            Email = email,
            PhoneNumber = "string3"
        };

        var createUserCommandHttpContent = new StringContent(JsonConvert.SerializeObject(createUserCommand), Encoding.UTF8, "application/json");
        
        var resultCreateUserCommand = await client.PostAsync(UserControllersEndPoints.Create,createUserCommandHttpContent);
        
        Assert.Equal(HttpStatusCode.OK, resultCreateUserCommand.StatusCode);
        return resultCreateUserCommand;
    }
}