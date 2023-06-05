using IntroTo.Graph.Helpers;

Console.WriteLine("Hello, Graph!");

var graph = GraphGenerator.GenerateCellField(5);

// var vertices = Enumerable.Range(1, 9)
//     .Select(x => new Vertex(x))
//     .ToDictionary(x => x.Id);
//
// var edges = new List<Edge>
// {
//     new(vertices[1], vertices[2], 3),
//     new(vertices[2], vertices[3], 9),
//     new(vertices[2], vertices[4], 4),
//     new(vertices[3], vertices[4], 1),
//     new(vertices[1], vertices[5], 5),
//     new(vertices[5], vertices[6], 7),
//     new(vertices[6], vertices[8], 3),
//     new(vertices[6], vertices[9], 3),
//     new(vertices[4], vertices[8], 3)
// };
//
// var graph = new Graph(edges, vertices.Select(x => x.Value).ToList());

GraphViewer.ShowVerticesList(graph);
GraphViewer.ShowMatrix(graph);