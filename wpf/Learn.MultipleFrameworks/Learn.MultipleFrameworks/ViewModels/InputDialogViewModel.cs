using System.Threading.Tasks;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.ViewModels;

public class InputDialogViewModel : BindableBase
{
    private readonly IEventAggregator _aggregator;
    private string? _input;

    public InputDialogViewModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
        InputSymbolCommand = new DelegateCommand<string>(OnInputSymbol);
        SubmitCommand = new DelegateCommand(OnSubmitFormInput);
    }
    
    public string? Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }

    public ICommand InputSymbolCommand { get; }
    public ICommand SubmitCommand { get; }

    private void OnSubmitFormInput()
    {
        if (string.IsNullOrWhiteSpace(Input)) return;

        var dialog = MainWindow.Instance.FindChild<InputDialogView>();
        
        var formInput = new FormInput(Input);
        var context = new DialogCloseContext
        {
            Host = MainWindow.Instance,
            MetroDialog = dialog
        };
        
        _aggregator.GetEvent<SubmitFormInputEvent>().Publish(formInput);
        _aggregator.GetEvent<DialogCloseRequestedEvent>().Publish(context);
    }
    
    private void OnInputSymbol(string symbol) => Input += symbol;
}