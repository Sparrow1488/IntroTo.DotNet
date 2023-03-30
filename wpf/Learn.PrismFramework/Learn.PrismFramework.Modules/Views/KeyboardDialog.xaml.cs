using Learn.PrismFramework.Modules.ViewModels;

namespace Learn.PrismFramework.Modules.Views;

public partial class KeyboardDialog
{
    public KeyboardDialog(KeyboardDialogViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}