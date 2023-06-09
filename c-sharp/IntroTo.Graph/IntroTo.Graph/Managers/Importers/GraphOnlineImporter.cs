using System.Xml;
using System.Xml.Linq;
using IntroTo.Graph.Contracts;
using IntroTo.Graph.Structures;

namespace IntroTo.Graph.Managers.Importers;

// ReSharper disable file CollectionNeverQueried.Local

public class GraphOnlineImporter : IDataStreamImporter<Structures.Graph>
{
    public Structures.Graph Import(Stream stream)
    {
        using var xmlTextReader = new XmlTextReader(stream);

        var doc = XDocument.Load(xmlTextReader);
        var exEdges = doc
            .Descendants("edge")
            .Select(x => new ExternalGraphEdge
            {
                Source = int.Parse(x.Attribute("source")!.Value),
                Target = int.Parse(x.Attribute("target")!.Value),
            })
            .ToArray();

        var vertices = new List<Vertex>();
        vertices.AddRange(exEdges.Select(x => new Vertex(x.Source + 1)));
        vertices.AddRange(exEdges.Select(x => new Vertex(x.Target + 1)));
        vertices = vertices.DistinctBy(x => x.Id).ToList();

        var verticesMap = vertices.ToDictionary(x => x.Id);
        var edges = new List<Edge>(exEdges.Select(x => new Edge(verticesMap[x.Source + 1], verticesMap[x.Target + 1])));
        
        return new Structures.Graph(edges.ToArray(), vertices.ToArray(), true);
    }

    private class ExternalGraphEdge
    {
        public int Source { get; set; }
        public int Target { get; set; }
    }
}