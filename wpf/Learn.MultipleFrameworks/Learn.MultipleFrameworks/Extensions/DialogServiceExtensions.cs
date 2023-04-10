using Learn.MultipleFrameworks.Services.Dialogs;
using MahApps.Metro.Controls.Dialogs;
using Prism.Ioc;

namespace Learn.MultipleFrameworks.Extensions;

public static class DialogServiceExtensions
{
    public static IContainerRegistry AddRegionDialogService(
        this IContainerRegistry container)
    {
        container.AddRegionDialogService<RegionDialogService>();
        return container;
    }
    
    public static IContainerRegistry AddRegionDialogService<TDialog>(
        this IContainerRegistry container)
            where TDialog : IRegionDialogService
    {
        container.RegisterSingleton<IDialogCoordinator>(_ => DialogCoordinator.Instance);
        container.RegisterSingleton<IRegionDialogService, TDialog>();
        
        return container;
    }
}