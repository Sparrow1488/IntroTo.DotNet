using Learn.MultipleFrameworks.ViewModels;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;

namespace Learn.MultipleFrameworks.Services.Dialogs;

public class DialogService
{
    private readonly IDialogCoordinator _dialogCoordinator;

    public DialogService(IDialogCoordinator dialogCoordinator)
    {
        _dialogCoordinator = dialogCoordinator;
    }
    
    public void ShowRegionInDialog(string region)
    {
        var dialog = CreateRegionDialog(region);        
        _dialogCoordinator.ShowMetroDialogAsync(MainWindow.Instance.DataContext, dialog)
            .ContinueWith(_ => {});
    }

    private static RegionDialogView CreateRegionDialog(string region)
    {
        var viewModel = new RegionDialogViewModel(region);
        return new RegionDialogView
        {
            DataContext = viewModel
        };
    }
}