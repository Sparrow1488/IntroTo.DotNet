using System;
using Learn.PrismFramework.Modules.ViewModels;
using Learn.PrismFramework.Modules.Views;
using Prism.Services.Dialogs;

namespace Learn.PrismFramework.Services.Interaction;

public class KeyboardInteractionService : IKeyboardInteractionService
{
    private readonly IDialogService _dialogService;

    public KeyboardInteractionService(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }
    
    public void ShowKeyboard(Action<string> input)
    {
        _dialogService.ShowDialog(
            nameof(KeyboardDialog),
            result =>
            {
                var text = result.Parameters.GetValue<string>("text");
                input.Invoke(text);
            });
    }
}