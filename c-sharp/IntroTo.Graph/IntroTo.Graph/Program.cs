using IntroTo.Graph.Algorithms;
using IntroTo.Graph.Helpers;
using IntroTo.Graph.Managers;
using IntroTo.Graph.Managers.Importers;
using IntroTo.Graph.Structures;
using IntroTo.Graph.Views;

Console.WriteLine("Hello, Graph!");

var manager = new GraphManager();
// var graph = manager.Import(
//     File.OpenRead(@"C:\Users\ilyao\OneDrive\Desktop\Docs\Practice\Graphs\Type1.graphml"),
//     new GraphOnlineImporter()
// );

var graph = GraphGenerator.GenerateCellField(200, true);

var first = graph.Vertices[0];
var last = graph.Vertices[^1];
foreach (var i in Enumerable.Range(0, 5000))
{
    BfsAlgorithm.Bfs(
        graph, 
        first,
        last
    );

    // Console.WriteLine(string.Join(", ", route));
    Console.WriteLine(i);
}