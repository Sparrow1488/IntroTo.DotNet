using IntroTo.Graph;
using IntroTo.Graph.Algorithms;
using IntroTo.Graph.Helpers;

Console.WriteLine("Hello, Graph!");

// var graph = GraphGenerator.GenerateCellField(5);

var vertices = Enumerable.Range(1, 11).Select(x => new Vertex(x - 1)).ToList();
var edges = new List<Edge>
{
    new(vertices[1], vertices[2]),
    new(vertices[1], vertices[3]),
    new(vertices[2], vertices[4]),
    new(vertices[2], vertices[5]),
    new(vertices[3], vertices[6]),
    new(vertices[3], vertices[7]),
    new(vertices[4], vertices[8]),
    new(vertices[4], vertices[5]),
    new(vertices[5], vertices[6]),
    new(vertices[6], vertices[9]),
    new(vertices[6], vertices[10]),
    new(vertices[7], vertices[9]),
    new(vertices[9], vertices[10]),
    new(vertices[10], vertices[8]),
};

var graph = new Graph(edges, vertices, true);

BfsAlgorithm.Bfs(graph, graph.Vertices.First(x => x.Id == 9));
var route = BfsAlgorithm.Bfs(
    graph, 
    new List<Vertex> { vertices[1], vertices[8] , vertices[6], vertices[1] }
);
Console.WriteLine(string.Join(", ", route));
GraphViewer.ShowVerticesList(graph);