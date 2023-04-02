using System;
using System.Windows.Input;
using Learn.MultipleFrameworks.Constants;
using Learn.MultipleFrameworks.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace Learn.MultipleFrameworks.ViewModels;

public class HomeViewModel : BindableBase
{
    private string? _text;

    public HomeViewModel(
        IDialogService dialogService,
        IEventAggregator aggregator)
    {
        OpenDialogCommand = new DelegateCommand(
            () =>
            {
                aggregator.GetEvent<DialogCloseRequestedEvent>().Subscribe(() =>
                {
                    Text = "Dialog closed at " + DateTime.Now.ToShortTimeString();
                });
                dialogService.ShowDialog(Dialogs.Default);
            });
    }

    public string? Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }
    
    public ICommand OpenDialogCommand { get; }
}