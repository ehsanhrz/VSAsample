using Features.Users.Models.Commands;
using Features.Users.Models.Queries;
using Features.Users.Services;

namespace IntegrationalTests;

[TestFixture]
public class GetUserTests : IntegrationalTestsBaseSetUp
{
    private GetUser GetUser { get; set; }
    private UserCud UserCud { get; set; }

    private string EmailForTest { get; set; } = "string14";
    private string PhoneNumberForTest { get; set; } = "string14";
    
    public GetUserTests()
    {
        GetUser = new GetUser(DbContext ?? throw new Exception("A"));
        UserCud = new UserCud(DbContext ?? throw new Exception());
        var command = new CreateUserCommand()
        {
            Email = EmailForTest,
            PhoneNumber = PhoneNumberForTest,
            FirstName = "string14",
            LastName = "string14"
        };
        UserCud.CreateAsync(command).Wait();
    }
    
    
    [Test]
    public async Task GetUserByEmailTest()
    {
        //a
        
        var query = new GetUserByEmailQuery()
        {
            Email = EmailForTest
        };
        
        //a
        var result = await GetUser.GetUserByEmailAsync(query);
        
        //a
        Assert.That(EmailForTest, Is.EqualTo(result.Email));
        
    }

    [Test]
    public async Task GetUserByPhoneNumber()
    {
        //a
        var query = new GetUserByPhoneNumberQuery()
        {
            PhoneNumber = PhoneNumberForTest
        };
        
        //a
        var result = await GetUser.GetUserByPhoneNumberAsync(query);
        
        //a
        Assert.That(PhoneNumberForTest, Is.EqualTo(result.PhoneNumber));
    }
}