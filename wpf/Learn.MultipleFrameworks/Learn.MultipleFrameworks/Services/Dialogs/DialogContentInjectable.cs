using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Services.Resolvers;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.Services.Dialogs;

public abstract class DialogContentInjectable : BindableBase
{
    protected static IContainerProvider Container => ContainerLocator.Container;
    protected static IEventAggregator Aggregator => Container.Resolve<IEventAggregator>();
    private static MainWindowResolver WindowResolver => Container.Resolve<MainWindowResolver>();
    
    /// <summary>
    /// Отправляет запрос на закрытие диалогового окна 
    /// </summary>
    /// <returns>
    ///     Отправлен запрос на закрытие окна
    ///     (если false, значит скорее всего компонент открыт не в диалоговом окне)
    /// </returns>
    protected static bool RequestDialogClose()
    {
        var window = WindowResolver.GetRequiredWindow();
        var dialog = window.FindChild<RegionDialogView>() as BaseMetroDialog;

        if (dialog is null) return false;
        
        var context = new DialogCloseContext
        {
            Host = window,
            MetroDialog = dialog
        };
        
        Aggregator.GetEvent<DialogCloseRequestedEvent>().Publish(context);
        
        return true;
    }
}