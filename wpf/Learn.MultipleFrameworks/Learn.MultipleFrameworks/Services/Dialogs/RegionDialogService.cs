using System;
using System.Windows.Media;
using ControlzEx.Theming;
using Learn.MultipleFrameworks.Services.Resolvers;
using Learn.MultipleFrameworks.ViewModels;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Learn.MultipleFrameworks.Services.Dialogs;

public class RegionDialogService : IRegionDialogService
{
    private readonly MainWindowResolver _windowResolver;
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly Brush _windowOverlayBrush;

    public RegionDialogService(
        MainWindowResolver windowResolver,
        IDialogCoordinator dialogCoordinator)
    {
        _windowResolver = windowResolver;
        _dialogCoordinator = dialogCoordinator;
        _windowOverlayBrush = new SolidColorBrush(Colors.Black);
    }
    
    public void ShowRegionInDialog(string region)
    {
        var dialog = CreateRegionDialog(region);

        var window = _windowResolver.GetRequiredWindow();
        if (window is not MetroWindow metroWindow)
        {
            throw new Exception("Resolved MainWindow is not MetroWindow");
        }
        FixWindowOverlayBrushInDarkMode(metroWindow);
        
        _dialogCoordinator.ShowMetroDialogAsync(metroWindow.DataContext, dialog)
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