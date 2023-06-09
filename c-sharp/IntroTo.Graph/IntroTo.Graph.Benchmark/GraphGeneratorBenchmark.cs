using BenchmarkDotNet.Attributes;
using IntroTo.Graph.Helpers;

namespace IntroTo.Graph.Benchmark;

// ReSharper disable file InconsistentNaming

[RankColumn]
[MemoryDiagnoser]
public class GraphGeneratorBenchmark
{
    private const bool HASH_GRAPH_VERTICES = true;
    
    [Benchmark]
    public void Generate100Cells()
    {
        GraphGenerator.GenerateCellField(10, HASH_GRAPH_VERTICES);
    }
    
    [Benchmark]
    public void Generate10000Cells()
    {
        GraphGenerator.GenerateCellField(100, HASH_GRAPH_VERTICES);
    }
    
    [Benchmark]
    public void Generate40000Cells()
    {
        GraphGenerator.GenerateCellField(200, HASH_GRAPH_VERTICES);
    }
    
    [Benchmark]
    public void Generate1000000Cells()
    {
        GraphGenerator.GenerateCellField(1000, HASH_GRAPH_VERTICES);
    }
}