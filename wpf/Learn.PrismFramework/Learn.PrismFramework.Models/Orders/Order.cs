using Learn.PrismFramework.Models.Customers;
using Learn.PrismFramework.Models.Goods;
using Learn.PrismFramework.Models.Identities;

namespace Learn.PrismFramework.Models.Orders;

public class Order : IIdentity
{
    public int Id { get; set; }
    public Customer Customer { get; set; }
    public List<Good> Goods { get; set; }
}