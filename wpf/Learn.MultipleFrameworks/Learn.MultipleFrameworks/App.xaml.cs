using System.Windows;
using Imlight.Hmi.Module.Dialogs;
using Imlight.Hmi.Module.Dialogs.Events;
using Imlight.Hmi.Module.Dialogs.Events.Models;
using Imlight.Hmi.Module.Dialogs.Extensions;
using Imlight.Hmi.Module.Dialogs.Services.Resolvers;
using Imlight.Hmi.Module.Keyboards.Extensions;
using Imlight.Hmi.Module.Keyboards.Services.Keyboards;
using Imlight.Hmi.Module.Keyboards.Services.Providers;
using Learn.MultipleFrameworks.Modules;
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

        container.RegisterScoped<MainWindowResolver>(_ => new MainWindowResolver(Current.MainWindow));
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

        modules.AddModule<DialogModule>();
        modules.AddKeyboardsModules();
    }
    
    private static void HideDialog(DialogCloseContext context, IContainerProvider container)
    {
        var coordinator = container.Resolve<IDialogCoordinator>();
        coordinator.HideMetroDialogAsync(context.Host.DataContext, context.MetroDialog)
            .ContinueWith(_ => {});
    }
}