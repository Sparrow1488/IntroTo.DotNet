using System.Windows;
using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Modules;
using Learn.MultipleFrameworks.ViewModels;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;

namespace Learn.MultipleFrameworks;

public partial class App
{
    protected override void RegisterTypes(IContainerRegistry container)
    {
        container.RegisterDialog<DialogView, DialogViewModel>(Dialogs.Default);
        container.RegisterSingleton<IDialogCoordinator>(_ => DialogCoordinator.Instance);

        #region Handle dialog closure

        var aggregator = Container.Resolve<IEventAggregator>();
        aggregator.GetEvent<DialogCloseRequestedEvent>().Subscribe(HideDialog);

        #endregion
    }

    private void HideDialog(DialogCloseContext context)
    {
        var coordinator = Container.Resolve<IDialogCoordinator>();
        coordinator.HideMetroDialogAsync(context.Host.DataContext, context.MetroDialog)
            .ContinueWith(_ => {});
    }
    
    protected override Window CreateShell() => Container.Resolve<MainWindow>();

    protected override void ConfigureModuleCatalog(IModuleCatalog modules)
    {
        modules.AddModule<DialogContentModule>();
        modules.AddModule<DialogModule>();
        modules.AddModule<HomeModule>();
    }
}