using System;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace Learn.MultipleFrameworks.ViewModels;

public class HomeViewModel : BindableBase
{
    private readonly IDialogService _dialogService;
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly IEventAggregator _aggregator;
    private string? _text;

    public HomeViewModel(
        IDialogService dialogService,
        IDialogCoordinator dialogCoordinator,
        IEventAggregator aggregator)
    {
        _dialogService = dialogService;
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

    private async void OnOpenDialog()
    {
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Subscribe(() =>
        {
            Text = "Dialog closed at " + DateTime.Now.ToShortTimeString();
        });
            
        // _dialogService.ShowDialog(Dialogs.Default);
        await _dialogCoordinator.ShowInputAsync(MainWindow.Instance.DataContext, "Title", "My Message");
    }
}