using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace Learn.MultipleFrameworks.ViewModels;

public class DialogViewModel : IDialogAware
{
    public event Action<IDialogResult>? RequestClose;

    public DialogViewModel()
    {
        PressCloseCommand = new DelegateCommand(OnDialogClosed);
    }
    
    public string Title => "Dialog";
    
    public ICommand PressCloseCommand { get; }
    
    public bool CanCloseDialog() => true;

    public void OnDialogClosed()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
    }

    public void OnDialogOpened(IDialogParameters parameters) { }
}