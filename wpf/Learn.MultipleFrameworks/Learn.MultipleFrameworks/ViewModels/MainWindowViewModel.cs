using Prism.Mvvm;

namespace Learn.MultipleFrameworks.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private string? _text = "Hello Prism!";

    public MainWindowViewModel()
    {
        
    }
    
    public string? Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }
}