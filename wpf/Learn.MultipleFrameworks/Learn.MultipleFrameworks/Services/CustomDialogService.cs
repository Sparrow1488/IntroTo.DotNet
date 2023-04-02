using System;
using System.Threading.Tasks;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.ViewModels;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;

namespace Learn.MultipleFrameworks.Services;

public class CustomDialogService
{
    private static BaseMetroDialog? _dialogView;
    private readonly IEventAggregator _aggregator;
    private readonly IDialogCoordinator _dialogCoordinator;

    public CustomDialogService(
        IEventAggregator aggregator,
        IDialogCoordinator dialogCoordinator)
    {
        _aggregator = aggregator;
        _dialogCoordinator = dialogCoordinator;
    }
    
    public async void ShowDialogAsync(Action handler)
    {
        var viewModel = new DialogViewModel(_aggregator);
        _dialogView = new DialogView
        {
            DataContext = viewModel
        };
        
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Subscribe(async () =>
        {
            handler.Invoke();
            await _dialogCoordinator.HideMetroDialogAsync(MainWindow.Instance.DataContext, _dialogView);
        }); 
        
        await _dialogCoordinator.ShowMetroDialogAsync(MainWindow.Instance.DataContext, _dialogView);
    }
}