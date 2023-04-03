namespace Learn.MultipleFrameworks.Events.Models;

public class IntNumberResult : IValueContains<int>
{
    public IntNumberResult(int value)
    {
        Value = value;
    }
    
    public int Value { get; }
}