using IntroTo.Graph.Algorithms;
using IntroTo.Graph.Helpers;

Console.WriteLine("Hello, Graph!");

var graph = GraphGenerator.GenerateCellField(3);

BfsAlgorithm.Bfs(graph, graph.Vertices.First(x => x.Id == 9));
var route = BfsAlgorithm.Bfs(
    graph, 
    graph.Vertices.First(x => x.Id == 1), 
    graph.Vertices.First(x => x.Id == 8)
);
GraphViewer.ShowVerticesList(graph);