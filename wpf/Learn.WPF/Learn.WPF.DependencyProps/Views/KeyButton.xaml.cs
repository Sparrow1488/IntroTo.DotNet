using System.Windows;

namespace Learn.WPF.DependencyProps.Views;

public partial class KeyButton
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(KeyButton),
        new PropertyMetadata(string.Empty)
    );
    
    public KeyButton()
    {
        InitializeComponent();
    }

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}