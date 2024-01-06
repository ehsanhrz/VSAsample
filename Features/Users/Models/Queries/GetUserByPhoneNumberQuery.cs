namespace Features.Users.Models.Queries;

public record GetUserByPhoneNumberQuery
{
    public GetUserByPhoneNumberQuery()
    {
        
    }
    public string PhoneNumber { get; set; }    
}