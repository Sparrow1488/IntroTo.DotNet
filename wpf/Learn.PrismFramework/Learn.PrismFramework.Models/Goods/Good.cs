using Learn.PrismFramework.Models.Identities;

namespace Learn.PrismFramework.Models.Goods;

public class Good : IIdentity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}