using System.Windows;
using Learn.PrismFramework.Infrastructure.Services.Customers;
using Learn.PrismFramework.Infrastructure.Services.Goods;
using Learn.PrismFramework.Infrastructure.Services.Orders;
using Learn.PrismFramework.Modules;
using Learn.PrismFramework.Modules.ViewModels;
using Learn.PrismFramework.Modules.Views;
using Learn.PrismFramework.Services.Interaction;
using Learn.PrismFramework.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Learn.PrismFramework;

public partial class App
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry registry)
    {
        registry.RegisterScoped<ICustomerService, CustomerService>();
        registry.RegisterScoped<IGoodsService, GoodsService>();
        registry.RegisterScoped<IOrdersService, OrdersService>();
        
        registry.RegisterScoped<IKeyboardInteractionService, KeyboardInteractionService>();
        
        registry.RegisterDialog<QuestionDialog, QuestionDialogViewModel>();
        // registry.RegisterDialog<KeyboardDialog, KeyboardDialogViewModel>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog catalog)
    {
        catalog.AddModule<ProfileModule>();
        catalog.AddModule<KeyboardModule>();
        catalog.AddModule<CustomersModule>();
        catalog.AddModule<CustomerOrdersModule>();
    }
}