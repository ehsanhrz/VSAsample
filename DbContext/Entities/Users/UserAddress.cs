using DbContext.Entities.Base;

namespace DbContext.Entities.Users;

public class UserAddress : EntityBase
{
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public long UserId { get; set; }

    public User User { get; set; } = new User();
}