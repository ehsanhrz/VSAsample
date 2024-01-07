using DbContext;
using Features.Users.Models.Commands;
using Features.Users.Models.Queries;
using Features.Users.Services;

namespace IntegrationalTests.User;

[TestFixture]
public class UserCudTests : IntegrationalTestsBaseSetUp
{
    
    private UserCud UserCud { get; set; }

    private GetUser GetUser { get; set; }
    
    public UserCudTests()
    {
        GetUser = new GetUser(DbContext ?? throw new Exception());
        UserCud = new UserCud(DbContext ?? throw new Exception());
    }
    
    [Test]
    public async Task TestUserCreateMethod()
    {
        //a
        var command = new CreateUserCommand()
        {
            FirstName = "string",
            LastName = "string",
            Email = "string",
            PhoneNumber = "string"
        };
        var query = new GetUserByEmailQuery()
        {
            Email = command.Email
        };
        
        //a
        await UserCud.CreateAsync(command);
        var result = await GetUser.GetUserByEmailAsync(query);
        
        //a checking two must unique value
        Assert.Multiple(() =>
        {
            Assert.That(command.Email, Is.EqualTo(result.Email));
            Assert.That(command.PhoneNumber, Is.EqualTo(result.PhoneNumber));
        });
    }

    [Test]
    public async Task TestUpdateUserMethod()
    {
        //a
        var createCommand = new CreateUserCommand()
        {
            FirstName = "string2",
            LastName = "string2",
            Email = "string2",
            PhoneNumber = "string2"
        };
        var query = new GetUserByEmailQuery()
        {
            Email = createCommand.Email
        };
        
        await UserCud.CreateAsync(createCommand);
        var createOperationResult = await GetUser.GetUserByEmailAsync(query);
        //a
        const string newEmail = "string23";
        const string newPhone = "string23";
        var updateCommand = new UpdateUserCommand()
        {
            Email = newEmail,
            FirstName = createOperationResult.FirstName,
            PhoneNumber = newPhone,
            LastName = createOperationResult.LastName,
            Id = createOperationResult.Id
        };
        var updateResultQuery = new GetUserByEmailQuery()
        {
            Email = newEmail
        };
        await UserCud.UpdateAsync(updateCommand);
        var updateOperationResult = await GetUser.GetUserByEmailAsync(updateResultQuery);
        //a
        Assert.Multiple(() =>
        {
            Assert.That(updateCommand.Email, Is.EqualTo(updateOperationResult.Email));
            Assert.That(updateCommand.PhoneNumber, Is.EqualTo(updateOperationResult.PhoneNumber));
        });
    }

    [Test]
    public async Task TestDeleteUserMethode()
    {
        //a
        var command = new CreateUserCommand()
        {
            FirstName = "string4",
            LastName = "string4",
            Email = "string4",
            PhoneNumber = "string4"
        };
        var query = new GetUserByEmailQuery()
        {
            Email = command.Email
        };
        
        await UserCud.CreateAsync(command);
        var result = await GetUser.GetUserByEmailAsync(query);
        //a
        var deleteCommand = new DeleteUserCommand()
        {
            Id = result.Id
        };
        await UserCud.DeleteAsync(deleteCommand);
        //a
        Assert.Throws<AggregateException>(() => GetUser.GetUserByEmailAsync(query).Wait());
    }
}