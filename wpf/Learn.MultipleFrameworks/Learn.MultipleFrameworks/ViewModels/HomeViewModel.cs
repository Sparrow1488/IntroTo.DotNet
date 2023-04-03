using System;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Services.Dialogs;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.ViewModels;

public class HomeViewModel : BindableBase
{
    private readonly DialogService _dialogService;
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly IEventAggregator _aggregator;
    private string? _dialogClosureTime;
    private string? _formInputValue;

    public HomeViewModel(
        DialogService dialogService,
        IDialogCoordinator dialogCoordinator, 
        IEventAggregator aggregator)
    {
        _dialogService = dialogService;
        _dialogCoordinator = dialogCoordinator;
        _aggregator = aggregator;

        ConfigureEventsHandlers();
        
        OpenDialogCommand = new DelegateCommand(_dialogService.OpenDialog);
        OpenInputDialogCommand = new DelegateCommand(_dialogService.OpenInputDialog);
    }

    public string? DialogClosureTime
    {
        get => _dialogClosureTime;
        set => SetProperty(ref _dialogClosureTime, value);
    }
    public string? FormInputValue
    {
        get => _formInputValue;
        set => SetProperty(ref _formInputValue, value);
    }

    public ICommand OpenDialogCommand { get; }
    public ICommand OpenInputDialogCommand { get; }

    private void ConfigureEventsHandlers()
    {
        _aggregator.GetEvent<DialogCloseRequestedEvent>()
            .Subscribe(_ =>
                DialogClosureTime = "Dialog closed at " + DateTime.Now.ToShortTimeString());

        _aggregator.GetEvent<SubmitFormInputEvent>()
            .Subscribe(input => FormInputValue = input.Value);
        
        _aggregator.GetEvent<SubmitIntNumberEvent>()
            .Subscribe(input => FormInputValue = input.Value.ToString());
    }
}