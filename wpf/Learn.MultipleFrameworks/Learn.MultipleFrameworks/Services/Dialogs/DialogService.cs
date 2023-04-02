using Learn.MultipleFrameworks.ViewModels;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;

namespace Learn.MultipleFrameworks.Services.Dialogs;

public class DialogService
{
    private readonly IEventAggregator _aggregator;
    private readonly IDialogCoordinator _dialogCoordinator;

    public DialogService(IEventAggregator aggregator, IDialogCoordinator dialogCoordinator)
    {
        _aggregator = aggregator;
        _dialogCoordinator = dialogCoordinator;
    }
    
    public void OpenDialog()
    {
        var viewModel = new DialogViewModel(_aggregator);
        var dialogView = new DialogView
        {
            DataContext = viewModel
        };
        
        _dialogCoordinator.ShowMetroDialogAsync(MainWindow.Instance.DataContext, dialogView)
            .ContinueWith(_ => {});
    }
    
    public void OpenInputDialog()
    {
        var viewModel = new InputDialogViewModel(_aggregator);
        var dialogView = new InputDialogView
        {
            DataContext = viewModel
        };
        
        _dialogCoordinator.ShowMetroDialogAsync(MainWindow.Instance.DataContext, dialogView)
            .ContinueWith(_ => {});
    }
}