using IntroTo.Graph;
using IntroTo.Graph.Algorithms;
using IntroTo.Graph.Helpers;
using IntroTo.Graph.Views;

Console.WriteLine("Hello, Graph!");

var graph = GraphGenerator.GenerateCellField(5, true);

var route = BfsAlgorithm.Bfs(
    graph, 
    new List<Vertex>
    {
        graph.Vertices[0],  // first
        graph.Vertices[^1], // last
        graph.Vertices[0],
        graph.Vertices[^1],
        graph.Vertices[0]
    }
);

Console.WriteLine(string.Join(", ", route));
Console.WriteLine();
Console.WriteLine(new GraphMatrixView(graph));
Console.WriteLine();
Console.WriteLine(new GraphVerticesListView(graph));