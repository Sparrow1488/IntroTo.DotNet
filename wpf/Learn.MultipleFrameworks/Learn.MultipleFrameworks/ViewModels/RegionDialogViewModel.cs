using Prism.Mvvm;

namespace Learn.MultipleFrameworks.ViewModels;

public class RegionDialogViewModel : BindableBase
{
    private string _regionName;
    
    public RegionDialogViewModel(string regionName)
    {
        _regionName = regionName;
    }

    public string RegionName
    {
        get => _regionName;
        set => SetProperty(ref _regionName, value);
    }
}