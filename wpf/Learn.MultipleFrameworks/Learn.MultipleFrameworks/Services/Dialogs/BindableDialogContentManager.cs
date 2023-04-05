using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.Services.Dialogs;

public abstract class BindableDialogContentManager : BindableBase
{
    protected IContainerProvider Container => ContainerLocator.Container;
    protected IEventAggregator Aggregator => Container.Resolve<IEventAggregator>();
    
    public virtual void RequestDialogClose()
    {
        var dialog = MainWindow.Instance.FindChild<RegionDialogView>() as BaseMetroDialog;

        if (dialog is null) return;
        
        var context = new DialogCloseContext
        {
            Host = MainWindow.Instance,
            MetroDialog = dialog!
        };
        
        Aggregator.GetEvent<DialogCloseRequestedEvent>().Publish(context);
    }
}