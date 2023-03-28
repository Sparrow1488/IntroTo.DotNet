using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Learn.WPF.DependencyProps.Views;

public partial class Keyboard
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(Keyboard),
        new PropertyMetadata(
            string.Empty,
            OnTextPropertyChanged));

    public Keyboard()
    {
        InitializeComponent();
    }

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Debug.WriteLine("TextProperty changed in Keyboard ");
    }

    // private void OnKeyButtonPressed(object sender, MouseButtonEventArgs e)
    // {
    //     var keyButton = sender as KeyButton;
    //     TextBox.Text += keyButton!.Text;
    // }

    private void KeyButtonPressed(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var buttonKeyText = button!.Content as string;
        TextBox.Text += buttonKeyText;
    }
}