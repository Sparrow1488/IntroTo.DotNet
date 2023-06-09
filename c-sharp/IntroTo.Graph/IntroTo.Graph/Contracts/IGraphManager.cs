namespace IntroTo.Graph.Contracts;

public interface IGraphManager
{
    Structures.Graph Import(Stream stream, IDataStreamImporter<Structures.Graph> importer);
    Stream Export(Structures.Graph graph, IDataStreamExporter<Structures.Graph> exporter);
}