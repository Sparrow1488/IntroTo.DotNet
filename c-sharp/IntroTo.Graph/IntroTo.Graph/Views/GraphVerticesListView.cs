using System.Text;

namespace IntroTo.Graph.Views;

public class GraphVerticesListView : ConsoleDataView<Structures.Graph>
{
    public GraphVerticesListView(Structures.Graph graph)
    {
        Graph = graph;
    }
    
    private Structures.Graph Graph { get; }
    
    public override string ToString()
    {
        var builder = new StringBuilder();

        if (Graph.VertexNeighbours is null)
        {
            if (Graph is Structures.Graph graph)
                graph.CreateVerticesNeighbours();
            else throw new Exception("Graph VerticesNeighbours is null");
        }

        var keys = Graph.VertexNeighbours!.Keys.OrderBy(x => x.Id).ToList();
        var maxKeyLength = keys.Max(x => x.Id).ToString().Length;
        foreach (var key in keys)
        {
            var padding = new string(' ', maxKeyLength - key.Id.ToString().Length);
            builder.AppendLine(
                $"{key.Id}{padding} -> " + 
                string.Join(", ", Graph.VertexNeighbours[key].Vertices.OrderBy(x => x.Id).Select(x => x.Id))
            );
        }

        return builder.ToString();
    }
}