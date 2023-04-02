using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Learn.MultipleFrameworks.ViewModels;

public class DialogViewModel : BindableBase
{
    private readonly IEventAggregator _aggregator;

    public DialogViewModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
        PressCloseCommand = new DelegateCommand(OnRequestClose);
    }
    
    public static string Title => "Dialog";

    public ICommand PressCloseCommand { get; }
    
    private void OnRequestClose()
    {
        var dialog = MainWindow.Instance.FindChild<DialogView>();
        var context = new DialogCloseContext
        {
            Host = MainWindow.Instance,
            MetroDialog = dialog
        };
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Publish(context);
    }
}