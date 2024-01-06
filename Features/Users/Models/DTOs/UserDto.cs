namespace Features.Users.Models.DTOs;

public struct UserDto
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}