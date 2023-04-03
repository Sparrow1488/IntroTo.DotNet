namespace Learn.MultipleFrameworks.Events.Models;

public interface IValueContains<TValue>
where TValue : notnull
{
    TValue Value { get; }
}