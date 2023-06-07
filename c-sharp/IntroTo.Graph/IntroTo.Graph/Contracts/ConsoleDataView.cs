namespace IntroTo.Graph.Contracts;

public abstract class ConsoleDataView<TData> : IDataView<TData>
    where TData : class
{
    public object View() => ToString() ?? throw new Exception();
    public abstract override string? ToString();
}