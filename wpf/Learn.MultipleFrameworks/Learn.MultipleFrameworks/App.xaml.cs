using System.Windows;
using Imlight.Hmi.Base.Core.Services;
using Imlight.Hmi.Module.Dialogs;
using Imlight.Hmi.Module.Dialogs.Events;
using Imlight.Hmi.Module.Dialogs.Events.Models;
using Imlight.Hmi.Module.Dialogs.Extensions;
using Imlight.Hmi.Module.Dialogs.Models;
using Imlight.Hmi.Module.Dialogs.Modules;
using Imlight.Hmi.Module.Dialogs.Services.Providers;
using Imlight.Hmi.Module.Dialogs.Services.Resolvers;
using Imlight.Hmi.Module.Keyboards.Events;
using Imlight.Hmi.Module.Keyboards.Events.Models;
using Imlight.Hmi.Module.Keyboards.Extensions;
using Imlight.Hmi.Module.Keyboards.Models.Settings;
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
    private IEventAggregator _aggregator = null!;

    protected override void RegisterTypes(IContainerRegistry container)
    {
        container.RegisterSingleton<KeyboardLayoutsProvider>();

        container.RegisterScoped<MainWindowResolver>(_ => new MainWindowResolver(Current.MainWindow));
        container.RegisterScoped<ScopedInstanceProvider<KeyboardSettings>, KeyboardSettingsProvider>();
        container.RegisterScoped<ScopedInstanceProvider<SubmitWrapperSettings>, SubmitWrapperSettingsProvider>();
        container.RegisterScoped<IKeyboardModalService, KeyboardModalService>();
        container.AddRegionDialogService();
        
        _aggregator = Container.Resolve<IEventAggregator>();
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Subscribe(
            ctx => HideDialog(ctx, Container));
        _aggregator.GetEvent<KeyboardValidationErrorEvent>().Subscribe(OnKeyboardValidationErrors);
    }

    protected override Window CreateShell() => Container.Resolve<MainWindow>();

    protected override void ConfigureModuleCatalog(IModuleCatalog modules)
    {
        modules.AddModule<HomeModule>();        

        modules.AddModule<DialogModule>();
        modules.AddModule<SubmitWrapperModule>();
        modules.AddKeyboardsModules();
    }
    
    private static void HideDialog(DialogCloseContext context, IContainerProvider container)
    {
        var coordinator = container.Resolve<IDialogCoordinator>();
        coordinator.HideMetroDialogAsync(context.Host.DataContext, context.MetroDialog)
            .ContinueWith(_ => {});
    }

    private void OnKeyboardValidationErrors(KeyboardValidationErrorArgs args)
    {
        _aggregator.GetEvent<SubmitWrapperEnableEvent>().Publish(new SubmitWrapperEnableArgs(args.IsValid));
    }
}