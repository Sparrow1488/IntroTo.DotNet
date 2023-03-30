using System;
using System.Windows.Input;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Commands;

namespace Learn.PrismFramework.Modules.ViewModels;

public class KeyboardViewModel : ViewModel
{
    private string? _text;

    public event Action<string> OnPressButton;

    public KeyboardViewModel()
    {
        PressKeyCommand = new DelegateCommand<string>(OnPressKey);
    }

    public string? Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }
    
    public ICommand PressKeyCommand { get; }

    private void OnPressKey(string symbol)
    {
        Text += symbol;
        
        OnPressButton?.Invoke(symbol);
    }
}