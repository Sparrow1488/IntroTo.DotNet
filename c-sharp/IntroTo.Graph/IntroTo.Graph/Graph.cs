namespace IntroTo.Graph;

public class Graph
{
    public Graph(IList<Edge> edges, IList<Vertex> vertices)
    {
        Edges = edges;
        Vertices = vertices;
    }
    
    public IList<Edge> Edges { get; }
    public IList<Vertex> Vertices { get; }

    public int[,] GetMatrix()
    {
        var matrix = new int[Vertices.Count, Vertices.Count];

        foreach (var edge in Edges)
        {
            var row = edge.From.Id;
            var column = edge.To.Id;

            matrix[row, column] = edge.Weight;
        }

        return matrix;
    }
}