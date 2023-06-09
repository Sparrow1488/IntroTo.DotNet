using IntroTo.Graph.Contracts;
using IntroTo.Graph.Helpers;

namespace IntroTo.Graph.Benchmark;

// ReSharper disable file InconsistentNaming

public static class GraphProvider
{
    private const bool HASH_GRAPH_VERTICES = true;
    public static Structures.Graph Graph100 { get; } = GraphGenerator.GenerateCellField(10, HASH_GRAPH_VERTICES);
    public static Structures.Graph Graph10000 { get; } = GraphGenerator.GenerateCellField(100, HASH_GRAPH_VERTICES);
    public static Structures.Graph Graph40000 { get; } = GraphGenerator.GenerateCellField(200, HASH_GRAPH_VERTICES);
}