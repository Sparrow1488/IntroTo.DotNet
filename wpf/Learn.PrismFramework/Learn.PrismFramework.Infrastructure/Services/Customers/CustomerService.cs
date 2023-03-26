using Learn.PrismFramework.Models.Customers;

namespace Learn.PrismFramework.Infrastructure.Services.Customers;

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

    public Customer? GetById(int customerId) =>
        _store.FirstOrDefault(x => x.Id == customerId);

    public IEnumerable<Customer> GetAll() => _store;

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