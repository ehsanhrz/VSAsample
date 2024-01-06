namespace Features.Users.Models.Commands;

public record UpdateUserCommand
{
    public UpdateUserCommand()
    {
        
    }
    
    public long Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}