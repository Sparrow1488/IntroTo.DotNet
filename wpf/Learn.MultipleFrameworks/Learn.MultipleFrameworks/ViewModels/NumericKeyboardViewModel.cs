using Learn.MultipleFrameworks.Services.Utilities;

namespace Learn.MultipleFrameworks.ViewModels;

public class NumericKeyboardViewModel : KeyboardViewModel
{
    protected override string DefaultValue => "0";

    protected override string? InputChanging(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return DefaultValue;
        }

        if (int.TryParse(input, out _))
        {
            return InputUtilities.TrimStartZero(input);
        }

        return Input;
    }

    protected override void InputSymbol(string? symbol)
    {
        if (symbol != null && char.IsDigit(symbol[0]))
        {
            Input = InputUtilities.TrimStartZero(Input + symbol);
        }
    }
}