using Learn.PrismFramework.Infrastructure.Events;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Learn.PrismFramework.Models.Profiles;
using Prism.Events;

namespace Learn.PrismFramework.Modules.News.ViewModels;

public class ProfileViewModel : ViewModel
{
    private IProfile? _profile;
    
    private readonly IEventAggregator _aggregator;

    public ProfileViewModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;

        _aggregator.GetEvent<SelectProfileEvent>().Subscribe(
            profile => Profile = profile);
    }

    public IProfile? Profile
    {
        get => _profile;
        set => SetProperty(ref _profile, value);
    }
}