namespace Features.Users.Models.Queries;

public record GetUserByIdQuery
{
    public GetUserByIdQuery()
    {
        
    }

    public long UseId { get; set; }
}