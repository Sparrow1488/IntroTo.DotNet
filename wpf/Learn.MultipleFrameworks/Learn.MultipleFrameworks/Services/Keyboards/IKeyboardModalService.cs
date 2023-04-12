using System;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Models.Settings;

namespace Learn.MultipleFrameworks.Services.Keyboards;

public interface IKeyboardModalService
{
    public void ShowNumericKeyboard(Action<KeyboardInput> onInput, KeyboardSettings? settings = null);
    public void ShowLimitsKeyboard(Action<KeyboardInput> onInput, KeyboardSettings? settings = null);
    public void ShowAlphabetKeyboard(Action<KeyboardInput> onInput, KeyboardSettings? settings = null);
}