using Learn.PrismFramework.Models.Customers;

namespace Learn.PrismFramework.Infrastructure.Services.Customers;

public interface ICustomerService
{
    ICollection<Customer> GetAll();
    Customer? Create(Customer customer);
}