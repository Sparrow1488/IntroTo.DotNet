namespace IntroTo.Graph.Contracts;

public interface IDataStreamExporter<in TData>
    where TData : class
{
    Stream Export(TData data);
}