using Learn.PrismFramework.Modules.News.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learn.PrismFramework.Modules.News;

public class ProfileModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry) { }

    public void OnInitialized(IContainerProvider resolver)
    {
        var regionManager = resolver.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion("ProfileRegion", typeof(ProfileView));
    }
}