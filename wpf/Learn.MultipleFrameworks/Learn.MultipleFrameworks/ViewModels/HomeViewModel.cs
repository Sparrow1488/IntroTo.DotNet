using System;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
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
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly IEventAggregator _aggregator;
    private string? _text;

    public HomeViewModel(IDialogCoordinator dialogCoordinator,
        IEventAggregator aggregator)
    {
        _dialogCoordinator = dialogCoordinator;
        _aggregator = aggregator;
        
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Subscribe(_ =>
            Text = "Dialog closed at " + DateTime.Now.ToShortTimeString());
        
        OpenDialogCommand = new DelegateCommand(OnOpenDialog);
    }

    public string? Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public ICommand OpenDialogCommand { get; }

    private BaseMetroDialog _dialogView;
    private void OnOpenDialog()
    {
        var viewModel = new DialogViewModel(_aggregator);
        _dialogView = new DialogView
        {
            DataContext = viewModel
        };
        
        _dialogCoordinator.ShowMetroDialogAsync(MainWindow.Instance.DataContext, _dialogView)
            .ContinueWith(_ => {});
    }
}