using System.Windows;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.Services.Dialogs;

public abstract class BindableDialogContext<TView> : BindableBase, IDialogContext 
    where TView : DependencyObject
{
    protected IContainerProvider Container => ContainerLocator.Container;
    protected IEventAggregator Aggregator => ContainerLocator.Container.Resolve<IEventAggregator>();
    
    public bool OpenedInDialog()
    {
        // TODO: check open in dialog
        return MainWindow.Instance.FindChild<TView>() is not null;
    }

    public virtual void RequestDialogClose()
    {
        if (!OpenedInDialog()) return;
        
        var dialog = MainWindow.Instance.FindChild<TView>() as BaseMetroDialog;
        var context = new DialogCloseContext
        {
            Host = MainWindow.Instance,
            MetroDialog = dialog!
        };
        
        Aggregator.GetEvent<DialogCloseRequestedEvent>().Publish(context);
    }
}