using IntroTo.Graph.Contracts;

namespace IntroTo.Graph.Structures;

public class Graph : IGraph
{
    private Dictionary<Vertex, VertexNeighbours>? _vertexNeighbours;
    
    public Graph(List<Edge> edges, List<Vertex> vertices, bool hashVerticesList = false)
    {
        Edges = edges.AsReadOnly();
        Vertices = vertices.AsReadOnly();

        if (hashVerticesList)
        {
            CreateVerticesNeighbours();
        }
    }
    
    public IReadOnlyList<Edge> Edges { get; }
    public IReadOnlyList<Vertex> Vertices { get; }
    public IReadOnlyDictionary<Vertex, VertexNeighbours>? VertexNeighbours => _vertexNeighbours?.AsReadOnly();

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
        if (VertexNeighbours is null)
            throw new Exception($"Сначала используйте вызов ${nameof(CreateVerticesAdjacentEdges)} ");

        foreach (var vertex in Vertices)
        {
            if (!VertexNeighbours.ContainsKey(vertex)) continue;
            
            var neighbours = VertexNeighbours[vertex];
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
}