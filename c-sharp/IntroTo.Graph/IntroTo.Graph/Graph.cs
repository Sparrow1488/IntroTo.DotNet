namespace IntroTo.Graph;

public class Graph
{
    public Graph(IList<Edge> edges, IList<Vertex> vertices, bool hashVerticesList = false)
    {
        Edges = edges;
        Vertices = vertices;
        if (hashVerticesList)
        {
            VerticesList = GetVerticesList();
        }
    }
    
    public IList<Edge> Edges { get; }
    public IList<Vertex> Vertices { get; }
    private Dictionary<Vertex, List<Vertex>>? VerticesList { get; }

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
        if (VerticesList is not null)
            return VerticesList;
        
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

    public List<Vertex> GetAdjacentVertices(Vertex vertex)
    {
        if (VerticesList is null)
        {
            var adjacentEdges = Edges.Where(x => x.To == vertex || x.From == vertex);
            return adjacentEdges.Select(
                x => x.To == vertex
                    ? x.From
                    : x.To
            ).ToList();
        }

        return VerticesList[vertex];
    }
}