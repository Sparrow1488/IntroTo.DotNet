using System.Windows;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Extensions;
using Learn.MultipleFrameworks.Modules;
using Learn.MultipleFrameworks.Services.Keyboards;
using Learn.MultipleFrameworks.Services.Providers;
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
        container.RegisterSingleton<KeyboardLayoutsProvider>();

        container.RegisterScoped<KeyboardSettingsProvider>();
        container.RegisterScoped<IKeyboardModalService, KeyboardModalService>();
        container.AddRegionDialogService();
        
        var aggregator = Container.Resolve<IEventAggregator>();
        aggregator.GetEvent<DialogCloseRequestedEvent>().Subscribe(
            ctx => HideDialog(ctx, Container));
    }

    protected override Window CreateShell() => Container.Resolve<MainWindow>();

    protected override void ConfigureModuleCatalog(IModuleCatalog modules)
    {
        modules.AddModule<HomeModule>();        
        modules.AddModule<LoginModule>();

        modules.AddDialogModule();
        modules.AddKeyboardsModules();
    }
    
    private static void HideDialog(DialogCloseContext context, IContainerProvider container)
    {
        var coordinator = container.Resolve<IDialogCoordinator>();
        coordinator.HideMetroDialogAsync(context.Host.DataContext, context.MetroDialog)
            .ContinueWith(_ => {});
    }
}