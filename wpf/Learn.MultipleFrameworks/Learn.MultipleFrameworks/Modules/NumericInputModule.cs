using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learn.MultipleFrameworks.Modules;

public class NumericInputModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry) { }

    public void OnInitialized(IContainerProvider container)
    {
        var manager = container.Resolve<IRegionManager>();
        manager.RegisterViewWithRegion(Regions.IntNumericInputRegion, typeof(IntNumericInputView));
    }
}