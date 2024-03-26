namespace IntroTo.Graph.Structures;

public class Graph
{
    private Dictionary<Vertex, VertexNeighbours>? _vertexNeighbours;
    
    public Graph(Edge[] edges, Vertex[] vertices, bool hashVerticesList = false)
    {
        Edges = edges;
        Vertices = vertices;

        if (hashVerticesList)
        {
            CreateVerticesNeighbours();
        }
    }
    
    public Edge[] Edges { get; }
    public Vertex[] Vertices { get; }
    public IReadOnlyDictionary<Vertex, VertexNeighbours>? VertexNeighbours => _vertexNeighbours?.AsReadOnly();

    public int[,] GetMatrix()
    {
        var matrix = new int[Vertices.Length, Vertices.Length];

        foreach (var edge in Edges)
        {
            var row = edge.From.Id - 1;
            var column = edge.To.Id - 1;

            matrix[row, column] = edge.Weight;
            matrix[column, row] = edge.Weight;
        }

        return matrix;
    }

    public void CreateVerticesNeighbours()
    {
        CreateVerticesAdjacentEdges();
        CreateVerticesAdjacentVertices();
    }

    private void CreateVerticesAdjacentEdges()
    {
        _vertexNeighbours ??= new Dictionary<Vertex, VertexNeighbours>();
        var addAdjacent = (Vertex v, Edge e) =>
        {
            if (!_vertexNeighbours.ContainsKey(v))
                _vertexNeighbours.Add(v, new VertexNeighbours());
            
            var edges = _vertexNeighbours[v].Edges;
            edges.Add(e);
        };
        
        foreach (var edge in Edges)
        {
            addAdjacent.Invoke(edge.From, edge);
            addAdjacent.Invoke(edge.To, edge);
        }
    }

    private void CreateVerticesAdjacentVertices()
    {
        if (_vertexNeighbours is null)
            throw new Exception($"Сначала используйте вызов ${nameof(CreateVerticesAdjacentEdges)} ");

        foreach (var vertex in Vertices)
        {
            if (!_vertexNeighbours.ContainsKey(vertex)) continue;
            
            var neighbours = _vertexNeighbours[vertex];
            var edges = neighbours.Edges;
            
            var vertices = GetAdjacentVertices(vertex, edges);
            neighbours.Vertices.AddRange(vertices);
        }
    }

    private static IEnumerable<Vertex> GetAdjacentVertices(Vertex vertex, List<Edge> edges)
        => edges.Select(edge => edge.To == vertex
            ? edge.From
            : edge.To);

    public IEnumerable<Vertex> GetAdjacentVertices(Vertex vertex)
    {
        if (_vertexNeighbours is null)
        {
            var adjacentEdges = Edges.Where(x => x.To == vertex || x.From == vertex);
            return adjacentEdges.Select(
                x => x.To == vertex
                    ? x.From
                    : x.To
            ).ToList();
        }

        return _vertexNeighbours[vertex].Vertices;
    }
    
    public IEnumerable<Edge> GetAdjacentEdges(Vertex vertex)
    {
        if (_vertexNeighbours is null)
        {
            return Edges.Where(x => x.To == vertex || x.From == vertex).ToList();
        }

        return _vertexNeighbours[vertex].Edges;
    }
}