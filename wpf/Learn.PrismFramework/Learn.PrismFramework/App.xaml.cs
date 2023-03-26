using System.Windows;
using Learn.PrismFramework.Modules.News;
using Learn.PrismFramework.Modules.News.Views;
using Learn.PrismFramework.Services.Customer;
using Learn.PrismFramework.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;

namespace Learn.PrismFramework;

public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry registry)
    {
        registry.RegisterScoped<ICustomerService, CustomerService>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog catalog)
    {
        catalog.AddModule<ArticleModule>();
    }
}