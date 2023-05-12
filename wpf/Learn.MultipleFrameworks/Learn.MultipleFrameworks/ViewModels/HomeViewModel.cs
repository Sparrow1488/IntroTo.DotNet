using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using Imlight.Hmi.Module.Dialogs.Events;
using Imlight.Hmi.Module.Keyboards.Events;
using Imlight.Hmi.Module.Keyboards.Events.Models;
using Imlight.Hmi.Module.Keyboards.Models;
using Imlight.Hmi.Module.Keyboards.Models.Settings;
using Imlight.Hmi.Module.Keyboards.Services.Keyboards;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Learn.MultipleFrameworks.ViewModels;

public class DelegateValidationRule : ValidationRule
{
    private readonly Func<object, ValidationResult> _validate;

    public DelegateValidationRule(Func<object, ValidationResult> validate)
    {
        _validate = validate;
    }
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return _validate.Invoke(value);
    }
}

public class HomeViewModel : BindableBase, INavigationAware
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
            IsPassword = true,
            InputValidationRule = new DelegateValidationRule(input =>
            {
                var inputText = input as string;
                return inputText!.Length < 5 
                    ? new ValidationResult(false, "Длина пароля меньше пяти")
                    : new ValidationResult(true, null);
            })
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

    public void OnNavigatedTo(NavigationContext context)
    {
        
    }

    public bool IsNavigationTarget(NavigationContext context)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext context)
    {
        
    }
}