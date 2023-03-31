using System.Windows;
using Microsoft.Practices.Prism.Regions;

namespace Learn.PrismFramework.Modules.Views;

public partial class Keyboard
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(Keyboard),
        new PropertyMetadata("Default text"));
    
    public Keyboard()
    {
        InitializeComponent();
    }

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}