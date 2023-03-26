using Learn.PrismFramework.Modules.News.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learn.PrismFramework.Modules.News;

public class ArticleModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry) { }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        var regionManager = containerProvider.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion("ArticleRegion", typeof(ArticleView));
    }
}