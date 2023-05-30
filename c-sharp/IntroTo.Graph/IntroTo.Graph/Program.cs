using System.Text;
using IntroTo.Graph;
using IntroTo.Graph.Algorithms;

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
var adjacentVertices = graph.GetAdjacentVertices(vertices[2]);

Console.WriteLine("Adjacent vertices: " + string.Join(", ", adjacentVertices));
Console.WriteLine(GetMatrixConsoleView(matrix));

var waveAlgorithm = new GraphWaveAlgorithm();
var route = waveAlgorithm.Execute(graph, new WaveAlgorithmArgs(vertices[5], vertices[7]));

Console.WriteLine(route);

static string GetMatrixConsoleView(int[,] matrix)
{
    var builder = new StringBuilder();

    for (var row = 1; row < matrix.GetLength(0); row++) // пропускаю ноль, так как Id у вершин идет с 1
    {
        for (var column = 1; column < matrix.GetLength(1); column++)
        {
            builder.Append(matrix[row, column] + " ");
        }

        builder.AppendLine();
    }

    return builder.ToString();
}