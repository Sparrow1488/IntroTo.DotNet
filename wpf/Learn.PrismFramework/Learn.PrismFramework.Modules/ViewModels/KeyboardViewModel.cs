using System;
using System.Windows.Input;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Learn.PrismFramework.Models.Controls.Keyboard;
using Microsoft.Xaml.Behaviors.Core;

namespace Learn.PrismFramework.Modules.ViewModels;

public class KeyboardViewModel : ViewModel
{
    private string? _text;
    
    public KeyboardViewModel()
    {
        PressKeyCommand = new ActionCommand(OnPressKey);
    }

    public string? Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }
    
    public ICommand PressKeyCommand { get; }

    private void OnPressKey(object keyButtonObj)
    {
        if (keyButtonObj is not KeyButton keyButton)
            throw new ArgumentException($"Passed argument {nameof(keyButtonObj)} not cased to {nameof(KeyButton)}");

        Text += keyButton.Symbol;
    }
}