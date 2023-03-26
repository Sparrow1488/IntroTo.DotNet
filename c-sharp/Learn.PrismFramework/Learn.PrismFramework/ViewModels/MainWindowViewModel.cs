using System.Collections.ObjectModel;
using System.Linq;
using Learn.PrismFramework.Models.Customer;
using Learn.PrismFramework.Modules.News;
using Learn.PrismFramework.Services.Customer;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;

namespace Learn.PrismFramework.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private string _selectedCustomer;

    private readonly IModuleManager _modules;
    private readonly ICustomerService _service;

    public MainWindowViewModel(IModuleManager modules, ICustomerService service)
    {
        _modules = modules;
        _service = service;
        Customers = new ObservableCollection<Customer>();
    }

    public ObservableCollection<Customer> Customers { get; }
    public string SelectedCustomer
    {
        get => _selectedCustomer;
        set => SetProperty(ref _selectedCustomer, value);
    }
    
    public DelegateCommand LoadCommand => new(LoadCustomers);
    public DelegateCommand LoadArticleModuleCommand => new(_modules.LoadModule<ArticleModule>);

    private void LoadCustomers()
    {
        var customers = _service.GetAll().ToList();
        customers.ForEach(x =>
        {
            Customers.Remove(x);
            Customers.Add(x);
        });
    }
}