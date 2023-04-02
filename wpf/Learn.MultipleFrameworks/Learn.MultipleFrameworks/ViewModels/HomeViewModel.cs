using System;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Services;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Learn.MultipleFrameworks.ViewModels;

public class HomeViewModel : BindableBase
{
    private readonly IRegionManager _manager;
    private readonly IDialogService _dialogService;
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly IEventAggregator _aggregator;
    private string? _text;

    public HomeViewModel(
        CustomDialogService dialogService,
        IRegionManager manager,
        IDialogCoordinator dialogCoordinator,
        IEventAggregator aggregator)
    {
        _manager = manager;
        _dialogCoordinator = dialogCoordinator;
        _aggregator = aggregator;
        OpenDialogCommand = new DelegateCommand(OnOpenDialog);
    }

    public string? Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public ICommand OpenDialogCommand { get; }

    private BaseMetroDialog _dialogView;
    private async void OnOpenDialog()
    {
        var viewModel = new DialogViewModel(_aggregator);
        _dialogView = new DialogView
        {
            DataContext = viewModel
        };
        
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Subscribe(async () =>
        {
            Text = "Dialog closed at " + DateTime.Now.ToShortTimeString();
            await _dialogCoordinator.HideMetroDialogAsync(MainWindow.Instance.DataContext, _dialogView);
        }); 
        
        await _dialogCoordinator.ShowMetroDialogAsync(MainWindow.Instance.DataContext, _dialogView);
    }
}