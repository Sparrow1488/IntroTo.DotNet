using System;
using Learn.MultipleFrameworks.Events.Models;

namespace Learn.MultipleFrameworks.Services.Keyboards;

public interface IKeyboardModalService
{
    public void ShowNumericKeyboard(Action<KeyboardInput> onInput);
    public void ShowLimitsKeyboard(Action<KeyboardInput> onInput);
    public void ShowAlphabetKeyboard(Action<KeyboardInput> onInput);
}