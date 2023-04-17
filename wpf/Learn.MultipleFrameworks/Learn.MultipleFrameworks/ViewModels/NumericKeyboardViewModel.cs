namespace Learn.MultipleFrameworks.ViewModels;

public class NumericKeyboardViewModel : KeyboardViewModel
{
    protected override string DefaultValue => "0";

    protected override void InputSymbol(string? symbol)
    {
        if (symbol != null && char.IsDigit(symbol[0]))
        {
            Input = (Input += symbol).TrimStart('0');
        }
    }
}