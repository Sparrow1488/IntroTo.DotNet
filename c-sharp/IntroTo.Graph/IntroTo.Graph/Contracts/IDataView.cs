namespace IntroTo.Graph.Contracts;

public interface IDataView<TData>
    where TData : class
{
    object View();
}