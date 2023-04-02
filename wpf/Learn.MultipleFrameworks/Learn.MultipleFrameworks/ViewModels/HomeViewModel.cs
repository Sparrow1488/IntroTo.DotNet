using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace Learn.MultipleFrameworks.ViewModels;

public class HomeViewModel : BindableBase
{
    public HomeViewModel(IDialogService dialogService)
    {
        OpenDialogCommand = new DelegateCommand(
            () => dialogService.ShowDialog("Dialog"));
    }
    
    public ICommand OpenDialogCommand { get; }
}