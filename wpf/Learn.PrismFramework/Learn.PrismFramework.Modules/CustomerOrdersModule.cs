using Learn.PrismFramework.Modules.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learn.PrismFramework.Modules;

public class CustomerOrdersModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry) { }

    public void OnInitialized(IContainerProvider container)
    {
        var regionManager = container.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion("CustomerOrdersRegion", typeof(CustomerOrdersView));
    }
}