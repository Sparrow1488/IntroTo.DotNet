using Prism.Mvvm;

namespace Learn.MultipleFrameworks.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private string? _title = "Prism application";

    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}