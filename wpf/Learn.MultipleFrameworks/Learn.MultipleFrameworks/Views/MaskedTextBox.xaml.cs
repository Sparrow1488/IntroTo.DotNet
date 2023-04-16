using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Learn.MultipleFrameworks.Views;

public partial class MaskedTextBox : UserControl
{
    public event Action<string?>? TextChanged; 

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(MaskedTextBox));

    public MaskedTextBox()
    {
        InitializeComponent();

        TextChanged += text =>
        {
            if (text != null)
            {
                MaskBox.Text = new string('*', text.Length);
            }
        };
    }

    public string? Text
    {
        get => GetValue(TextProperty) as string;
        set
        {
            SetValue(TextProperty, value);
            TextChanged?.Invoke(value);
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        Text = textBox!.Text;
    }
}