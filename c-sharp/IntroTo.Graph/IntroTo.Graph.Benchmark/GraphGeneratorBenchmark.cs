using BenchmarkDotNet.Attributes;
using IntroTo.Graph.Helpers;

namespace IntroTo.Graph.Benchmark;

[RankColumn]
[MemoryDiagnoser]
public class GraphGeneratorBenchmark
{
    [Benchmark(Description = "Generate100Cells")]
    public void Generate100Cells()
    {
        GraphGenerator.GenerateCellField(10);
    }
    
    [Benchmark(Description = "Generate10000Cells")]
    public void Generate10000Cells()
    {
        GraphGenerator.GenerateCellField(100);
    }
    
    [Benchmark(Description = "Generate40000Cells")]
    public void Generate40000Cells()
    {
        GraphGenerator.GenerateCellField(200);
    }
}