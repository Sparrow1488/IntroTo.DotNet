using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.Services.Dialogs;

public abstract class BindableDialogContentManager : BindableBase
{
    private static IContainerProvider Container => ContainerLocator.Container;
    protected static IEventAggregator Aggregator => Container.Resolve<IEventAggregator>();
    
    /// <summary>
    /// Отправляет запрос на закрытие диалогового окна 
    /// </summary>
    /// <returns>
    ///     Отправлен запрос на закрытие окна
    ///     (если false, значит скорее всего компонент открыт не в диалоговом окне)
    /// </returns>
    protected static bool RequestDialogClose()
    {
        var dialog = MainWindow.Instance.FindChild<RegionDialogView>() as BaseMetroDialog;

        if (dialog is null) return false;
        
        var context = new DialogCloseContext
        {
            Host = MainWindow.Instance,
            MetroDialog = dialog!
        };
        
        Aggregator.GetEvent<DialogCloseRequestedEvent>().Publish(context);
        
        return true;
    }
}