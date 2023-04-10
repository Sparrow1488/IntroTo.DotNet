using System;
using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Services.Dialogs;
using Prism.Events;

namespace Learn.MultipleFrameworks.Services.Keyboards;

public class KeyboardModalService : IKeyboardModalService
{
    private readonly IRegionDialogService _dialogService;
    private readonly IEventAggregator _aggregator;
    private Action<KeyboardInput>? _onKeyboardInputAction;

    public KeyboardModalService(IRegionDialogService dialogService, IEventAggregator aggregator)
    {
        _dialogService = dialogService;
        _aggregator = aggregator;
    }

    public void ShowNumericKeyboard(Action<KeyboardInput> onInput)
    {
        ShowSubscribedDialogKeyboard(Regions.NumericKeyboardRegion, onInput);
    }
    
    public void ShowLimitsKeyboard(Action<KeyboardInput> onInput)
    {
        ShowSubscribedDialogKeyboard(Regions.LimitsKeyboardRegion, onInput);
    }
    
    public void ShowAlphabetKeyboard(Action<KeyboardInput> onInput)
    {
        ShowSubscribedDialogKeyboard(Regions.AlphabetKeyboardRegion, onInput);
    }

    private void ShowSubscribedDialogKeyboard(string keyboardRegion, Action<KeyboardInput> onInput)
    {
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