namespace IntroTo.Graph;

public class Graph
{
    public Graph(IList<Edge> edges, IList<Vertex> vertices, bool hashVerticesList = false)
    {
        Edges = edges;
        Vertices = vertices;
        if (hashVerticesList)
        {
            CreateVerticesAdjacentEdges();
            CreateVerticesAdjacentVertices();
        }
    }
    
    public IList<Edge> Edges { get; }
    public IList<Vertex> Vertices { get; }
    private Dictionary<Vertex, VertexNeighbours>? VerticesList { get; set; }

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

    public Dictionary<Vertex, VertexNeighbours> CreateVerticesAdjacentEdges()
    {
        VerticesList ??= new Dictionary<Vertex, VertexNeighbours>();
        var addAdjacent = (Vertex v, Edge e) =>
        {
            if (!VerticesList.ContainsKey(v))
                VerticesList.Add(v, new VertexNeighbours());
            
            var edges = VerticesList[v].Edges;
            edges.Add(e);
        };
        
        foreach (var edge in Edges)
        {
            addAdjacent.Invoke(edge.From, edge);
            addAdjacent.Invoke(edge.To, edge);
        }

        return VerticesList;
    }

    public Dictionary<Vertex, VertexNeighbours> CreateVerticesAdjacentVertices()
    {
        if (VerticesList is null)
            throw new Exception($"Сначала используйте вызов ${nameof(CreateVerticesAdjacentEdges)} ");

        VerticesList ??= new Dictionary<Vertex, VertexNeighbours>();
        
        foreach (var vertex in Vertices)
        {
            if (!VerticesList.ContainsKey(vertex)) continue;
            
            var neighbours = VerticesList[vertex];
            var edges = neighbours.Edges;
            
            var vertices = GetAdjacentVertices(vertex, edges);
            neighbours.Vertices.AddRange(vertices);
        }

        return VerticesList;
    }

    public static IEnumerable<Vertex> GetAdjacentVertices(Vertex vertex, List<Edge> edges)
        => edges.Select(edge => edge.To == vertex
            ? edge.From
            : edge.To);

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

        return VerticesList[vertex].Vertices;
    }
}