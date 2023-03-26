using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.PrismFramework.Services.Customer;
using Customer = Learn.PrismFramework.Models.Customer.Customer;

public sealed class CustomerService : ICustomerService
{
    private readonly List<Customer> _store;

    public CustomerService()
    {
        _store = new List<Customer>
        {
            new() { Id = 1, Name = "Valentin" },
            new() { Id = 2, Name = "Yuri" },
            new() { Id = 3, Name = "Igor" },
        };
    }

    public ICollection<Customer> GetAll() => _store;

    public Customer? Create(Customer customer)
    {
        var exists = _store.FirstOrDefault(x => x.Id == customer.Id);
        if (exists is null)
        {
            _store.Add(customer);
        }

        return exists ?? customer;
    }
}