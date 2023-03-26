using Learn.PrismFramework.Models.Identities;
using Learn.PrismFramework.Models.Profiles;

namespace Learn.PrismFramework.Models.Customers;

public class Customer : IIdentity, IProfile
{
    public int Id { get; set; }
    public string Name { get; set; }
}