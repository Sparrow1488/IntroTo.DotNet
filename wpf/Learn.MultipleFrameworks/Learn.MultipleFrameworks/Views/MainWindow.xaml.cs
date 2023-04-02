using System.Windows;

namespace Learn.MultipleFrameworks.Views;

public partial class MainWindow
{
    public static MainWindow Instance = null!;
    
    public MainWindow()
    {
        InitializeComponent();
        Instance = this;
    }
}