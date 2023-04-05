using MahApps.Metro.Controls;

namespace Learn.MultipleFrameworks.Views;

public partial class MainWindow : MetroWindow
{
    public static MainWindow Instance = null!;
    
    public MainWindow()
    {
        InitializeComponent();
        Instance = this;
    }
}