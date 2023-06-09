using IntroTo.Graph.Contracts;

namespace IntroTo.Graph.Managers;

public class GraphManager : IGraphManager
{
    public Structures.Graph Import(Stream stream, IDataStreamImporter<Structures.Graph> importer)
        => importer.Import(stream);

    public Stream Export(Structures.Graph graph, IDataStreamExporter<Structures.Graph> exporter)
        => exporter.Export(graph);
}