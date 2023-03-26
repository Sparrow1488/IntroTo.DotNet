using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn.PrismFramework.Services.Customer;
using Customer = Models.Customer.Customer;

public interface ICustomerService
{
    ICollection<Customer> GetAll();
    Customer? Create(Customer customer);
}