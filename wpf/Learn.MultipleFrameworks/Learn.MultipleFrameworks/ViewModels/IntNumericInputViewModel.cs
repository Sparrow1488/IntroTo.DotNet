using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;

namespace Learn.MultipleFrameworks.ViewModels;

public class IntNumericInputViewModel : KeyboardViewModel
{
    protected override string DefaultValue => "0";

    protected override void OnSubmitInput()
    {
        var input = int.Parse(Input);
        Aggregator.GetEvent<SubmitIntNumberEvent>().Publish(new IntNumberResult(input));
        
        base.OnSubmitInput();
    }

    protected override void InputSymbol(string symbol)
    {
        Input = symbol.TrimStart('0');
    }
}