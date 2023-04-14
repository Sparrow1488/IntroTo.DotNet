using System.Windows;
using System.Windows.Controls;
using Learn.MultipleFrameworks.ViewModels;

namespace Learn.MultipleFrameworks.Views;

public partial class AlphabetKeyboardView
{
    public AlphabetKeyboardView()
    {
        InitializeComponent();
    }
    
    private Visibility PasswordVisibility => PasswordBox.Visibility;

    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        var passwordBox = (PasswordBox) sender;

        if (DataContext is KeyboardViewModel viewModel)
        {
            viewModel.Input = passwordBox.Password;
        }
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is KeyboardViewModel viewModel)
        {
            viewModel.PressKeyButton += (symbol) =>
            {
                PasswordBox.Password += symbol;
            };
            viewModel.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(viewModel.Input) && PasswordVisibility != Visibility.Visible)
                    PasswordBox.Password = viewModel.Input;
            };
        }
    }
}