using System;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

namespace Learn.MultipleFrameworks.ViewModels;

public class DialogViewModel : IDialogAware
{
    private readonly IEventAggregator _aggregator;
    public event Action<IDialogResult>? RequestClose;

    public DialogViewModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
        PressCloseCommand = new DelegateCommand(OnRequestClose);
    }
    
    public string Title => "Dialog";
    
    public ICommand PressCloseCommand { get; }
    
    public bool CanCloseDialog() => true;

    public void OnDialogClosed()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
    }

    public void OnDialogOpened(IDialogParameters parameters) { }

    private void OnRequestClose()
    {
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Publish();
        OnDialogClosed();
    }
}