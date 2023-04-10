using System.Windows.Media;
using ControlzEx.Theming;
using Learn.MultipleFrameworks.ViewModels;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Learn.MultipleFrameworks.Services.Dialogs;

public class RegionDialogService : IRegionDialogService
{
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly Brush _windowOverlayBrush;

    public RegionDialogService(IDialogCoordinator dialogCoordinator)
    {
        _dialogCoordinator = dialogCoordinator;
        _windowOverlayBrush = new SolidColorBrush(Colors.Black);
    }
    
    public void ShowRegionInDialog(string region)
    {
        var dialog = CreateRegionDialog(region);

        FixWindowOverlayBrushInDarkMode(MainWindow.Instance);
        
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
    
    private void FixWindowOverlayBrushInDarkMode(MetroWindow window)
    {
        var currentTheme = ThemeManager.Current.DetectTheme();
        if (currentTheme != null && currentTheme.Name.Contains("Dark"))
        {
            window.OverlayBrush = _windowOverlayBrush.Clone();
        }
    }
}