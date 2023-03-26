using System.Collections.ObjectModel;
using System.Linq;
using Learn.PrismFramework.Infrastructure.Events;
using Learn.PrismFramework.Infrastructure.Services.Goods;
using Learn.PrismFramework.Infrastructure.Services.Orders;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Learn.PrismFramework.Models.Orders;
using Prism.Events;

namespace Learn.PrismFramework.Modules.ViewModels;

public class CustomerOrdersViewModel : ViewModel
{
    private int _customerId;
    
    private readonly IOrdersService _orders;

    public CustomerOrdersViewModel(
        IOrdersService orders,
        IOrdersService ordersService,
        IGoodsService goodsService,
        IEventAggregator aggregator)
    {
        _orders = orders;
        Orders = new ObservableCollection<Order>();

        ordersService.CreateOrder(1, goodsService.GetAll());

        aggregator.GetEvent<SelectCustomerEvent>().Subscribe(customer =>
        {
            CustomerId = customer?.Id ?? 0;
            LoadOrders();
        });
    }

    public int CustomerId
    {
        get => _customerId;
        set => SetProperty(ref _customerId, value);
    }
    public ObservableCollection<Order> Orders { get; }

    private void LoadOrders()
    {
        FlushOrders();
        
        if (_customerId > 0)
        {
            var orders = _orders.GetCustomerOrders(_customerId).ToList();
            orders.ForEach(x => Orders.Add(x));
        }
    }

    private void FlushOrders()
    {
        var length = Orders.Count;
        for (var i = 0; i < length; i++)
            Orders.Remove(Orders[i]);
    }
}