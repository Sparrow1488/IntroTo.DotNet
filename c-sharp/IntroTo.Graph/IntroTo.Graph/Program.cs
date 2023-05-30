using System.Text;
using IntroTo.Graph;

Console.WriteLine("Hello, Graph!");

var vertices = Enumerable.Range(1, 7)
    .Select(x => new Vertex(x))
    .ToDictionary(x => x.Id);

var edges = new List<Edge>
{
    new(vertices[1], vertices[2], 3),
    new(vertices[2], vertices[3], 9),
    new(vertices[2], vertices[4], 4),
    new(vertices[3], vertices[4], 1),
    new(vertices[4], vertices[3], 1),
    new(vertices[1], vertices[5], 5),
    new(vertices[5], vertices[6], 7)
};

var graph = new Graph(edges, vertices.Select(x => x.Value).ToList());
var matrix = graph.GetMatrix();

Console.WriteLine(GetMatrixConsoleView(matrix));
// Console.WriteLine(GetGraphConsoleView(graph));

static string GetGraphConsoleView(Graph graph)
{
    var builder = new StringBuilder();

    builder.AppendLine($"Edges {graph.Edges.Count}, Vertices {graph.Vertices.Count}");
    
    foreach (var edge in graph.Edges)
        builder.AppendLine($"Edge, From: {edge.From.Id}; To: {edge.To.Id}");

    return builder.ToString();
}

static string GetMatrixConsoleView(int[,] matrix)
{
    var builder = new StringBuilder();

    for (var row = 1; row < matrix.GetLength(0); row++)
    {
        for (var column = 1; column < matrix.GetLength(1); column++)
        {
            builder.Append(matrix[row, column] + " ");
        }

        builder.AppendLine();
    }

    return builder.ToString();
}