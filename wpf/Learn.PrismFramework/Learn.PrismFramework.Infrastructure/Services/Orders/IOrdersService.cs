using Learn.PrismFramework.Models.Goods;
using Learn.PrismFramework.Models.Orders;

namespace Learn.PrismFramework.Infrastructure.Services.Orders;

public interface IOrdersService
{
    IEnumerable<Order> GetCustomerOrders(int customerId);
    Order CreateOrder(int customerId, IEnumerable<Good> goods);
}