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
            var row = edge.From.Id - 1;
            var column = edge.To.Id - 1;

            matrix[row, column] = edge.Weight;
            matrix[column, row] = edge.Weight;
        }

        return matrix;
    }

    public Dictionary<Vertex, List<Vertex>> GetVerticesList()
    {
        var result = new Dictionary<Vertex, List<Vertex>>();
        var addToResult = (Vertex v) =>
            result.TryAdd(v, new List<Vertex>(GetAdjacentVertices(v)));
        
        foreach (var edge in Edges)
        {
            var from = edge.From;
            var to = edge.To;
            
            addToResult.Invoke(from);
            addToResult.Invoke(to);
        }

        return result;
    }

    public IList<Vertex> GetAdjacentVertices(Vertex vertex, bool inDepth = false)
    {
        IEnumerable<Edge> adjacentEdges;
        
        if (inDepth)
        {
            adjacentEdges = Edges.Where(x => x.To == vertex);
        }
        adjacentEdges = Edges.Where(x => x.From == vertex || x.To == vertex);
        return adjacentEdges.Select(
            x => x.From == vertex
            ? x.To
            : x.From
        ).ToList();
    }
}