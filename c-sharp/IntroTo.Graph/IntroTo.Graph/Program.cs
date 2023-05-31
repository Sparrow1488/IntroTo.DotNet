using System.Text;
using IntroTo.Graph;
using IntroTo.Graph.Algorithms;

Console.WriteLine("Hello, Graph!");

var vertices = Enumerable.Range(1, 9)
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
    new(vertices[5], vertices[6], 7),
    new(vertices[6], vertices[5], 7),
    new(vertices[6], vertices[8], 3),
    new(vertices[6], vertices[9], 3),
    new(vertices[8], vertices[4], 3)
};

var graph = new Graph(edges, vertices.Select(x => x.Value).ToList());
var matrix = graph.GetMatrix();
var adjacentVertices = graph.GetAdjacentVertices(vertices[2]);

Console.WriteLine("Adjacent vertices: " + string.Join(", ", adjacentVertices));
Console.WriteLine(GetMatrixConsoleView(matrix));

var dfs = new GraphDepthFirstSearchAlgorithm();
var dfsRoute = dfs.Execute(graph, new DfsArgs(vertices[1], vertices[9]));

Console.WriteLine(dfsRoute);

static string GetMatrixConsoleView(int[,] matrix)
{
    var builder = new StringBuilder();

    for (var row = 0; row < matrix.GetLength(0); row++)
    {
        for (var column = 0; column < matrix.GetLength(1); column++)
        {
            builder.Append(matrix[row, column] + " ");
        }

        builder.AppendLine();
    }

    return builder.ToString();
}