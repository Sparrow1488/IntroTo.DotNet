using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Services.Dialogs;
using Learn.MultipleFrameworks.Views;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Learn.MultipleFrameworks.ViewModels;

public class NumericInputViewModel : BindableBase
{
    private readonly IEventAggregator _aggregator;
    private string? _input;

    public NumericInputViewModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
        InputSymbolCommand = new DelegateCommand<string>(OnInputSymbol);
        SubmitCommand = new DelegateCommand(SubmitNumber);
    }
    
    public string? Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }

    public ICommand InputSymbolCommand { get; }
    public ICommand SubmitCommand { get; }

    private void SubmitNumber()
    {
        var input = int.Parse(Input!);
        _aggregator.GetEvent<SubmitIntNumberEvent>().Publish(new IntNumberResult(input));
    }

    private void OnInputSymbol(string symbol)
    {
        var scopeInput = Input + symbol;
        if (int.TryParse(scopeInput, out _))
        {
            Input = scopeInput;
        }
    }
}