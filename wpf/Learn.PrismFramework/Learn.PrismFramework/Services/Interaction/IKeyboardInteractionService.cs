using System;

namespace Learn.PrismFramework.Services.Interaction;

public interface IKeyboardInteractionService
{
    // TODO: specify keyboard type
    void ShowKeyboard(Action<string> input);
}