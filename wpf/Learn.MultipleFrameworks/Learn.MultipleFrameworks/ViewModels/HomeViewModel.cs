using System;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Models.Settings;
using Learn.MultipleFrameworks.Services.Keyboards;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.ViewModels;

public class HomeViewModel : BindableBase
{
    private readonly IEventAggregator _aggregator;
    private string? _dialogClosureTime;
    private string? _keyboardOneValue;
    private string? _keyboardTwoValue;

    public HomeViewModel(
        IEventAggregator aggregator,
        IKeyboardModalService keyboardService)
    {
        _aggregator = aggregator;

        ConfigureEventsHandlers();
        var passwordSettings = new KeyboardSettings()
        {
            IsPassword = true
        };
        
        ShowLimitsKeyboardDialogCommand = new DelegateCommand(
            () => keyboardService.ShowLimitsKeyboard(OnModalKeyboardReceiveValue));
        ShowNumericKeyboardDialogCommand = new DelegateCommand(
            () => keyboardService.ShowNumericKeyboard(OnModalKeyboardReceiveValue, passwordSettings));
        ShowAlphabetKeyboardDialogCommand = new DelegateCommand(
            () => keyboardService.ShowAlphabetKeyboard(OnModalKeyboardReceiveValue, passwordSettings));
    }

    public string? DialogClosureTime
    {
        get => _dialogClosureTime;
        set => SetProperty(ref _dialogClosureTime, value);
    }
    public string? KeyboardOneValue
    {
        get => _keyboardOneValue;
        set => SetProperty(ref _keyboardOneValue, value);
    }
    public string? KeyboardTwoValue
    {
        get => _keyboardTwoValue;
        set => SetProperty(ref _keyboardTwoValue, value);
    }

    public ICommand ShowLimitsKeyboardDialogCommand { get; }
    public ICommand ShowNumericKeyboardDialogCommand { get; }
    public ICommand ShowAlphabetKeyboardDialogCommand { get; }

    private void OnModalKeyboardReceiveValue(KeyboardInput input)
    {
        KeyboardOneValue = input.Value;
    }

    private void ConfigureEventsHandlers()
    {
        _aggregator.GetEvent<DialogCloseRequestedEvent>()
            .Subscribe(_ =>
                DialogClosureTime = "Dialog closed at " + DateTime.Now.ToShortTimeString());

        _aggregator.GetEvent<SubmitKeyboardInputEvent>()
            .Subscribe(input =>
            {
                if (!input.OpenedInDialog)
                    KeyboardTwoValue = input.Value;
            });
    }
}