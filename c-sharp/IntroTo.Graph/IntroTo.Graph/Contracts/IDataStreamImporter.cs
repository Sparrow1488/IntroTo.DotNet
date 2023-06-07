namespace IntroTo.Graph.Contracts;

public interface IDataStreamImporter<out TData>
    where TData : class
{
    TData Import(Stream stream);
}