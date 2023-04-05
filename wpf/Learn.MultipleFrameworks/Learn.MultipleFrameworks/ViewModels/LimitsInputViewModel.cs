using System.Linq;

namespace Learn.MultipleFrameworks.ViewModels;

public class LimitsInputViewModel : KeyboardViewModel
{
    private const string PositiveSymbol = "+/-";
    private const string PlusOneHundredSymbol = "+100";
    private const string MinusOneHundredSymbol = "-100";
    private const string TopLimitSymbol = "↑";
    private const string BottomLimitSymbol = "↓";
    
    protected override string DefaultValue => "0";

    protected override void InputSymbol(string symbol)
    {
        if (IsSpecial(symbol))
        {
            var isZeroFirst = Input.StartsWith('0');
            var isMinusFirst = Input.StartsWith('-');
            
            if (symbol == PositiveSymbol && !isZeroFirst && !isMinusFirst)
                Input = "-" + Input;
            else if (symbol == PositiveSymbol && !isZeroFirst && isMinusFirst)
                Input = Input.TrimStart('-');
        }
        else
        {
            var bondedInput = Input += symbol;
            Input = bondedInput.TrimStart('0');
        }
    }

    private static bool IsSpecial(string symbol)
    {
        var special = new[]
        {
            PositiveSymbol,
            PlusOneHundredSymbol,
            MinusOneHundredSymbol,
            TopLimitSymbol,
            BottomLimitSymbol
        };

        return special.Contains(symbol);
    }
}