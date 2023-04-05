using Learn.MultipleFrameworks.Events;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Services.Dialogs;
using Prism.Commands;
using Prism.Events;

namespace Learn.MultipleFrameworks.ViewModels;

public class IntNumericInputViewModel : BindableDialogContentManager
{
    private readonly IEventAggregator _aggregator;
    private string _input;

    public IntNumericInputViewModel(IEventAggregator aggregator)
    {
        _input = DefaultValue;
        _aggregator = aggregator;
        InputSymbolCommand = new DelegateCommand<string>(OnInputSymbol);
        SubmitCommand = new DelegateCommand(SubmitNumericInput);
        ResetCommand = new DelegateCommand(ResetInput);
    }
    
    public string Input
    {
        get => _input;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                SetProperty(ref _input, DefaultValue);
            else
                SetProperty(ref _input, value);
        }
    }

    private static string DefaultValue => "0";

    public ICommand InputSymbolCommand { get; }
    public ICommand SubmitCommand { get; }
    public ICommand ResetCommand { get; }

    private void SubmitNumericInput()
    {
        var input = int.Parse(Input);
        _aggregator.GetEvent<SubmitIntNumberEvent>().Publish(new IntNumberResult(input));
        
        RequestDialogClose();
    }

    private void ResetInput()
    {
        Input = DefaultValue;
    }

    private void OnInputSymbol(string symbol)
    {
        var scopeInput = Input + symbol;
        
        if (int.TryParse(scopeInput, out _))
        {
            if (scopeInput.StartsWith(DefaultValue[0]))
                scopeInput = scopeInput.Remove(0, 1);
            
            Input = scopeInput;
        }
    }
}