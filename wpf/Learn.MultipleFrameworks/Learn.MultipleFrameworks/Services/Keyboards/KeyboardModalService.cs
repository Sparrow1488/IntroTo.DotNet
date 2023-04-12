using System;
using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Models.Settings;
using Learn.MultipleFrameworks.Services.Dialogs;
using Prism.Events;

namespace Learn.MultipleFrameworks.Services.Keyboards;

public class KeyboardModalService : IKeyboardModalService
{
    private readonly KeyboardSettingsProvider _keyboardSettings;
    private readonly IRegionDialogService _dialogService;
    private readonly IEventAggregator _aggregator;
    private Action<KeyboardInput>? _onKeyboardInputAction;

    public KeyboardModalService(
        KeyboardSettingsProvider keyboardSettings,
        IRegionDialogService dialogService, 
        IEventAggregator aggregator)
    {
        _keyboardSettings = keyboardSettings;
        _dialogService = dialogService;
        _aggregator = aggregator;
    }

    public void ShowNumericKeyboard(Action<KeyboardInput> onInput, KeyboardSettings? settings = null)
    {
        ShowSubscribedDialogKeyboard(Regions.NumericKeyboardRegion, onInput, settings);
    }
    
    public void ShowLimitsKeyboard(Action<KeyboardInput> onInput, KeyboardSettings? settings = null)
    {
        ShowSubscribedDialogKeyboard(Regions.LimitsKeyboardRegion, onInput, settings);
    }
    
    public void ShowAlphabetKeyboard(Action<KeyboardInput> onInput, KeyboardSettings? settings = null)
    {
        ShowSubscribedDialogKeyboard(Regions.AlphabetKeyboardRegion, onInput, settings);
    }

    private void ShowSubscribedDialogKeyboard(
        string keyboardRegion, 
        Action<KeyboardInput> onInput,
        KeyboardSettings? settings = null)
    {
        _keyboardSettings.ConfigureScopeKeyboardSetting(
            () => settings ?? _keyboardSettings.CreateDefault());
        
        _onKeyboardInputAction = onInput;
        _dialogService.ShowRegionInDialog(keyboardRegion);

        _aggregator.GetEvent<SubmitKeyboardInputEvent>().Subscribe(OnReceiveKeyboardInput);
    }

    private void OnReceiveKeyboardInput(KeyboardInput input)
    {
        _onKeyboardInputAction?.Invoke(input);
        _onKeyboardInputAction = null;
    }
}