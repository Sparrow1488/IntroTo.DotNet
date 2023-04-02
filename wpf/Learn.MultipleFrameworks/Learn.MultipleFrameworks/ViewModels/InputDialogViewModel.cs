using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Services.Dialogs;
using Learn.MultipleFrameworks.Views;
using Prism.Commands;

namespace Learn.MultipleFrameworks.ViewModels;

public class InputDialogViewModel : BindableDialogContext<InputDialogView>
{
    private string? _input;

    public InputDialogViewModel()
    {
        InputSymbolCommand = new DelegateCommand<string>(OnInputSymbol);
        SubmitCommand = new DelegateCommand(RequestDialogClose);
    }
    
    public string? Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }

    public ICommand InputSymbolCommand { get; }
    public ICommand SubmitCommand { get; }

    public override void RequestDialogClose()
    {
        base.RequestDialogClose();
        Aggregator.GetEvent<SubmitFormInputEvent>().Publish(new FormInput(Input ?? ""));
    }

    private void OnInputSymbol(string symbol) => Input += symbol;
}