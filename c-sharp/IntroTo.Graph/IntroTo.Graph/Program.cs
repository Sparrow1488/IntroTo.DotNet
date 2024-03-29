﻿using IntroTo.Graph.Algorithms;
using IntroTo.Graph.Helpers;
using IntroTo.Graph.Structures;
using IntroTo.Graph.Views;

Console.WriteLine("Hello, Graph!");

// var manager = new GraphManager();
// var graph = manager.Import(
//     File.OpenRead(@"C:\Users\ilyao\OneDrive\Desktop\Docs\Practice\Graphs\Type1.graphml"),
//     new GraphOnlineImporter()
// );

var graph = GraphGenerator.GenerateCellField(4, true);

var route = BfsAlgorithm.Bfs(
    graph, 
    new List<Vertex>
    {
        graph.Vertices[0],  // first
        graph.Vertices[^1]  // last
    }
);

Console.WriteLine(string.Join(", ", route));
Console.WriteLine();
Console.WriteLine(new GraphMatrixView(graph));
Console.WriteLine();
Console.WriteLine(new GraphVerticesListView(graph));