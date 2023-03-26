using Learn.PrismFramework.Models.Profiles;
using Prism.Events;

namespace Learn.PrismFramework.Infrastructure.Events;

public class SelectProfileEvent : PubSubEvent<IProfile>
{
    
}