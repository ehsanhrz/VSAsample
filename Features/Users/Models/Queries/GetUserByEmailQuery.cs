namespace Features.Users.Models.Queries;

public record GetUserByEmailQuery
{
    public GetUserByEmailQuery()
    {
        
    }
    public string Email { get; set; }
}