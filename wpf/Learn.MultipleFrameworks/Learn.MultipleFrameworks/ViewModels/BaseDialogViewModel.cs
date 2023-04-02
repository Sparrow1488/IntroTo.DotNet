using Prism.Events;

namespace Learn.MultipleFrameworks.ViewModels;

public abstract class BaseDialogViewModel
{
    public BaseDialogViewModel(IEventAggregator aggregator)
    {
        Aggregator = aggregator;
    }
    
    public IEventAggregator Aggregator { get; }
}