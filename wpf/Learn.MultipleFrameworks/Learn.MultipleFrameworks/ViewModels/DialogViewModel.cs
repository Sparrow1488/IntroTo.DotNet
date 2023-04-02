using System.Windows.Input;
using Learn.MultipleFrameworks.Services.Dialogs;
using Learn.MultipleFrameworks.Views;
using Prism.Commands;

namespace Learn.MultipleFrameworks.ViewModels;

public class DialogViewModel : BindableDialogContext<DialogView>
{
    public DialogViewModel()
    {
        PressCloseCommand = new DelegateCommand(RequestDialogClose);
    }
    
    public static string Title => "Dialog";

    public ICommand PressCloseCommand { get; }
}