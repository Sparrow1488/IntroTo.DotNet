namespace Learn.MultipleFrameworks.Models;

public class KeyButton
{
    public KeyButton(string currentSymbol, int row)
    {
        CurrentSymbol = currentSymbol;
        Row = row;
    }
    
    public string CurrentSymbol { get; set; }
    public int Row { get; set; }
}