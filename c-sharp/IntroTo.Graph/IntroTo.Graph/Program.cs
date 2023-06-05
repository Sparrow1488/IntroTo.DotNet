using IntroTo.Graph.Helpers;

Console.WriteLine("Hello, Graph!");

var graph = GraphGenerator.GenerateCellField(10);

GraphViewer.ShowVerticesList(graph);
// GraphViewer.ShowMatrix(graph);