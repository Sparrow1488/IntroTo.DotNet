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
    private string _input = "0";

    public IntNumericInputViewModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
        InputSymbolCommand = new DelegateCommand<string>(OnInputSymbol);
        SubmitCommand = new DelegateCommand(SubmitNumericInput);
    }
    
    public string Input
    {
        get => _input;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                SetProperty(ref _input, "0");
            else
                SetProperty(ref _input, value);
        }
    }

    public ICommand InputSymbolCommand { get; }
    public ICommand SubmitCommand { get; }

    private void SubmitNumericInput()
    {
        var input = int.Parse(Input);
        _aggregator.GetEvent<SubmitIntNumberEvent>().Publish(new IntNumberResult(input));
        
        RequestDialogClose();
    }

    private void OnInputSymbol(string symbol)
    {
        var scopeInput = Input + symbol;
        
        if (int.TryParse(scopeInput, out _))
        {
            if (scopeInput.StartsWith('0'))
                scopeInput = scopeInput.Remove(0, 1);
            
            Input = scopeInput;
        }
    }
}