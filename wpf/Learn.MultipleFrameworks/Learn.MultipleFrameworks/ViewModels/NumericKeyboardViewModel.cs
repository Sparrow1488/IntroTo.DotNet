namespace Learn.MultipleFrameworks.ViewModels;

public class NumericKeyboardViewModel : KeyboardViewModel
{
    protected override string DefaultValue => "0";

    protected override void InputSymbol(string symbol)
    {
        Input = (Input += symbol).TrimStart('0');
    }
}