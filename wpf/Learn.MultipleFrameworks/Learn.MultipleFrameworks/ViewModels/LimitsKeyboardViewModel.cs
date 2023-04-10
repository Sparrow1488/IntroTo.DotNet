using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;

namespace Learn.MultipleFrameworks.ViewModels;

public class LimitsKeyboardViewModel : KeyboardViewModel
{
    private const string PositiveSymbol = "+/-";
    private const string PlusOneHundredSymbol = "+100";
    private const string MinusOneHundredSymbol = "-100";
    private const string TopLimitSymbol = "↑";
    private const string BottomLimitSymbol = "↓";

    public LimitsKeyboardViewModel()
    {
        // TODO: узнаем точное предназначение и будет реализовано
        PressMagicCommand = new DelegateCommand(() => InputSymbol(BottomLimitSymbol));
    }
    
    protected override string DefaultValue => "0";

    public ICommand PressMagicCommand { get; }

    protected override void InputSymbol(string symbol)
    {
        if (IsSpecial(symbol))
        {
            var isZeroFirst = Input.StartsWith('0');
            var isMinusFirst = Input.StartsWith('-');

            #region Positive Symbol Handle

            Input = symbol switch
            {
                PositiveSymbol when !isZeroFirst && !isMinusFirst => "-" + Input,
                PositiveSymbol when !isZeroFirst && isMinusFirst => Input.TrimStart('-'),
                _ => Input
            };

            #endregion

            #region Add Value Handle

            Input = symbol switch
            {
                PlusOneHundredSymbol => (double.Parse(Input) + 100).ToString(CultureInfo.CurrentCulture),
                MinusOneHundredSymbol => (double.Parse(Input) - 100).ToString(CultureInfo.CurrentCulture),
                _ => Input
            };

            #endregion

            #region Limits Symbol Handle

            Input = symbol switch
            {
                TopLimitSymbol => "15000",
                BottomLimitSymbol => DefaultValue,
                _ => Input
            };

            #endregion
        }
        else
        {
            Input = (Input += symbol).TrimStart('0');
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