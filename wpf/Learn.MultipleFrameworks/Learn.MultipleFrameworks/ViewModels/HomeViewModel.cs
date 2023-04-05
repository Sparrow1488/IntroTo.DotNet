using System;
using System.Windows.Input;
using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Services.Dialogs;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.ViewModels;

public class HomeViewModel : BindableBase
{
    private readonly IEventAggregator _aggregator;
    private string? _dialogClosureTime;
    private string? _formInputValue;

    public HomeViewModel(DialogService dialogService, IEventAggregator aggregator)
    {
        _aggregator = aggregator;

        ConfigureEventsHandlers();
        
        OpenInputDialogCommand = new DelegateCommand(
            () => dialogService.ShowRegionInDialog(Regions.LimitsInputRegion));
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

    public ICommand OpenInputDialogCommand { get; }

    private void ConfigureEventsHandlers()
    {
        _aggregator.GetEvent<DialogCloseRequestedEvent>()
            .Subscribe(_ =>
                DialogClosureTime = "Dialog closed at " + DateTime.Now.ToShortTimeString());

        _aggregator.GetEvent<SubmitKeyboardInputEvent>()
            .Subscribe(input => FormInputValue = input.Value);
    }
}