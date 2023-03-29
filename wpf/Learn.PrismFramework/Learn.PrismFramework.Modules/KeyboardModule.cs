using Learn.PrismFramework.Modules.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learn.PrismFramework.Modules;

public class KeyboardModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry) { }

    public void OnInitialized(IContainerProvider container)
    {
        var manager = container.Resolve<IRegionManager>();
        manager.RegisterViewWithRegion("KeyboardRegion", typeof(Keyboard));
    }
}