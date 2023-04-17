using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Layout;

namespace Learn.MultipleFrameworks.Views;

public partial class NumericKeyboardView : UserControl
{
    public NumericKeyboardView()
    {
        InitializeComponent();
    }

    private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!int.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }
}