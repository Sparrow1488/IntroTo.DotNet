using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learn.MultipleFrameworks.Modules;

public class DialogContentModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry) { }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        var manager = containerProvider.Resolve<IRegionManager>();
        manager.RegisterViewWithRegion(Regions.DialogContentRegion, typeof(DialogContentView));
    }
}