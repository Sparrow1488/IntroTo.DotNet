using System.Collections.ObjectModel;
using System.Linq;
using Learn.PrismFramework.Infrastructure.Events;
using Learn.PrismFramework.Infrastructure.Services.Customers;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Learn.PrismFramework.Models.Customers;
using Prism.Commands;
using Prism.Events;

namespace Learn.PrismFramework.Modules.News.ViewModels;

public class CustomersViewModel : ViewModel
{
    private Customer? _selectedCustomer;
    
    private readonly ICustomerService _customers;
    private readonly IEventAggregator _aggregator;

    public CustomersViewModel(
        ICustomerService customers,
        IEventAggregator aggregator)
    {
        _customers = customers;
        _aggregator = aggregator;
        Customers = new ObservableCollection<Customer>();
    }
    
    public ObservableCollection<Customer> Customers { get; }
    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            if (SetProperty(ref _selectedCustomer, value))
                _aggregator.GetEvent<SelectProfileEvent>().Publish(value);
        }
    }

    public DelegateCommand LoadCustomersCommand => new(LoadCustomers);
    
    private void LoadCustomers()
    {
        var customers = _customers.GetAll().ToList();
        customers.ForEach(x =>
        {
            Customers.Remove(x);
            Customers.Add(x);
        });
    }
}