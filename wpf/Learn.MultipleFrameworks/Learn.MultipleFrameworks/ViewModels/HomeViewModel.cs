using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Imlight.Hmi.Module.Dialogs.Events;
using Imlight.Hmi.Module.Dialogs.Models;
using Imlight.Hmi.Module.Dialogs.Services.Dialogs;
using Imlight.Hmi.Module.Keyboards.Constants;
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
        IRegionDialogService dialogService,
        IKeyboardModalService keyboardService)
    {
        _aggregator = aggregator;

        ConfigureEventsHandlers();
        
        var settings = new KeyboardSettings
        {
            // IsPassword = true,
            Limit = new Limit(-1100, 5600),
            // StartValue = "790"
        };

        settings.InputValidationRule = new DelegateValidationRule(input =>
        {
            if (!long.TryParse((string?) input, out var numInput))
            {
                return ValidationResult.ValidResult;
            }
            
            var inLimits = settings.Limit.Value.IsInLimit(numInput);
            var isValid = true;
            var error = $"Конечная позиция должна быть в пределах [{settings.Limit.Value.Min};{settings.Limit.Value.Max}], а иначего Lorem inpsum text fish";

            if (!inLimits)
            {
                isValid = false;
            }

            return new ValidationResult(isValid, error);
        });

        var dialogSettings = new DialogSettings
        {
            Title = "Пример собственного заголовка"
        };
        
        ShowHomeDialogCommand = new DelegateCommand(
            () => dialogService.ShowRegionInDialog(Regions.HomeRegion));
        ShowLimitsKeyboardDialogCommand = new DelegateCommand(
            () => keyboardService.ShowLimitsKeyboard(OnModalKeyboardReceiveValue, settings, dialogSettings));
        ShowNumericKeyboardDialogCommand = new DelegateCommand(
            () => keyboardService.ShowNumericKeyboard(OnModalKeyboardReceiveValue, settings, dialogSettings));
        ShowAlphabetKeyboardDialogCommand = new DelegateCommand(
            () => keyboardService.ShowAlphabetKeyboard(OnModalKeyboardReceiveValue, settings, dialogSettings));
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

    public ICommand ShowHomeDialogCommand { get; }
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