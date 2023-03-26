using Learn.PrismFramework.Models.Customers;
using Prism.Events;

namespace Learn.PrismFramework.Infrastructure.Events;

public class SelectCustomerEvent : PubSubEvent<Customer>
{
    
}