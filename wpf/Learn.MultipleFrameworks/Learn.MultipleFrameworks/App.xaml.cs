using System.Windows;
using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Modules;
using Learn.MultipleFrameworks.ViewModels;
using Learn.MultipleFrameworks.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Learn.MultipleFrameworks;

public partial class App
{
    protected override void RegisterTypes(IContainerRegistry container)
    {
        container.RegisterDialog<DialogView, DialogViewModel>(Dialogs.Default);
    }

    protected override Window CreateShell() => Container.Resolve<MainWindow>();

    protected override void ConfigureModuleCatalog(IModuleCatalog modules)
    {
        modules.AddModule<DialogModule>();
        modules.AddModule<HomeModule>();
    }
}