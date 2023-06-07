namespace IntroTo.Graph.Contracts;

public interface IGraphManager
{
    IGraph Import(Stream stream, IDataStreamImporter<IGraph> importer);
    Stream Export(IGraph graph, IDataStreamExporter<IGraph> exporter);
}