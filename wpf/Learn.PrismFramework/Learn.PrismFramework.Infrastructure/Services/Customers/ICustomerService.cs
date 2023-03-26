using Learn.PrismFramework.Models.Customers;

namespace Learn.PrismFramework.Infrastructure.Services.Customers;

public interface ICustomerService
{
    Customer? GetById(int customerId);
    IEnumerable<Customer> GetAll();
    Customer? Create(Customer customer);
}