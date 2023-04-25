using System.Windows.Controls;
using System.Windows.Input;

namespace Learn.MultipleFrameworks.Views;

public partial class NumericKeyboardView : UserControl
{
    public NumericKeyboardView()
    {
        InitializeComponent();
    }

    private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!long.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }
}