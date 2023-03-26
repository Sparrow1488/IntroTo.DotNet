using Learn.PrismFramework.Infrastructure.Services.Customers;
using Learn.PrismFramework.Models.Goods;
using Learn.PrismFramework.Models.Orders;

namespace Learn.PrismFramework.Infrastructure.Services.Orders;

public class OrdersService : IOrdersService
{
    private readonly List<Order> _orders = new();
    
    private readonly ICustomerService _customerService;

    public OrdersService(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    public IEnumerable<Order> GetCustomerOrders(int customerId)
    {
        return _orders.Where(x => x.Customer.Id == customerId);
    }

    public Order CreateOrder(int customerId, IEnumerable<Good> goods)
    {
        var customer = _customerService.GetById(customerId)!;
        
        var order = new Order
        {
            Id = _orders.Count + 1,
            Customer = customer,
            Goods = goods.ToList()
        };
        _orders.Add(order);
        
        return order;
    }
}