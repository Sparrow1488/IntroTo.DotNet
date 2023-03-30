using System;
using System.Windows.Input;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace Learn.PrismFramework.Modules.ViewModels;

public class KeyboardDialogViewModel : ViewModel, IDialogAware
{
    private readonly KeyboardViewModel _keyboard;
    public event Action<IDialogResult>? RequestClose;

    public KeyboardDialogViewModel(KeyboardViewModel keyboard)
    {
        _keyboard = keyboard;

        SubmitTextCommand = new DelegateCommand(() =>
        {
            CloseDialog(_keyboard.Text ?? "");
        });
    }

    public string Title => "Keyboard";
    public ICommand SubmitTextCommand { get; }
    
    public bool CanCloseDialog() => true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters) { }

    private void CloseDialog(string submitText)
    {
        var result = new DialogResult(ButtonResult.Ignore);
        result.Parameters.Add("text", submitText);
        
        RequestClose?.Invoke(result);
    }
}