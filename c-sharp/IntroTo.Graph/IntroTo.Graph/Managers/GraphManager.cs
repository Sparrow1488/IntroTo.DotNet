using IntroTo.Graph.Contracts;

namespace IntroTo.Graph.Managers;

public class GraphManager : IGraphManager
{
    public IGraph Import(Stream stream, IDataStreamImporter<IGraph> importer)
        => importer.Import(stream);

    public Stream Export(IGraph graph, IDataStreamExporter<IGraph> exporter)
        => exporter.Export(graph);
}