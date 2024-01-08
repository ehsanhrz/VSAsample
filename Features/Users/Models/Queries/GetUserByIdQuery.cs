namespace Features.Users.Models.Queries;

public record GetUserByIdQuery
{
    public GetUserByIdQuery()
    {
        
    }

    public long UserId { get; set; }
}