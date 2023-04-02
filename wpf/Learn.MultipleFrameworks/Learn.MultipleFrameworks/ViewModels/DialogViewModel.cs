using System;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace Learn.MultipleFrameworks.ViewModels;

public class DialogViewModel : BindableBase, IDialogAware
{
    private readonly IEventAggregator _aggregator;
    private BaseMetroDialog _dialogView;
    public event Action<IDialogResult>? RequestClose;

    public DialogViewModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
        PressCloseCommand = new DelegateCommand<BaseMetroDialog>(OnRequestClose);
    }
    
    public string Title => "Dialog";

    public BaseMetroDialog DialogView
    {
        get => _dialogView;
        set => SetProperty(ref _dialogView, value);
    }
    
    public ICommand PressCloseCommand { get; }
    
    public bool CanCloseDialog() => true;

    public void OnDialogClosed()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
    }

    public void OnDialogOpened(IDialogParameters parameters) { }

    private void OnRequestClose(BaseMetroDialog dialog)
    {
        var context = new DialogCloseContext
        {
            Host = MainWindow.Instance,
            MetroDialog = dialog
        };
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Publish(context);
        OnDialogClosed();
    }
}