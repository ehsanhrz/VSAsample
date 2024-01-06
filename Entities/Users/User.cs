using Entities.Base;

namespace Entities.Users;

public class User : EntityBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool TwoStepVerificationWithEmail { get; set; }
    public bool TwoStepVerificationWithPhoneNumber { get; set; }
    public IEnumerable<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}