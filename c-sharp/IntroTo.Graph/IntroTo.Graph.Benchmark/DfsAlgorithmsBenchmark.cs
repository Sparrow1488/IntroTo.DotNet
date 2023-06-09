using BenchmarkDotNet.Attributes;
using IntroTo.Graph.Algorithms;

namespace IntroTo.Graph.Benchmark;

[RankColumn]
[MemoryDiagnoser]
public class DfsAlgorithmsBenchmark
{
    [Benchmark]
    public void DfsStack100()
    {
        DfsAlgorithm.DfsStackPath(GraphProvider.Graph100, GraphProvider.Graph100.Vertices[0], GraphProvider.Graph100.Vertices[^1]);
    }
    
    [Benchmark]
    public void Dfs100()
    {
        DfsAlgorithm.Dfs(GraphProvider.Graph100, GraphProvider.Graph100.Vertices[0], GraphProvider.Graph100.Vertices[^1]);
    }
    
    [Benchmark]
    public void Dfs10000()
    {
        DfsAlgorithm.Dfs(GraphProvider.Graph10000, GraphProvider.Graph10000.Vertices[0], GraphProvider.Graph10000.Vertices[^1]);
    }
    
    [Benchmark]
    public void Dfs40000()
    {
        DfsAlgorithm.Dfs(GraphProvider.Graph40000, GraphProvider.Graph40000.Vertices[0], GraphProvider.Graph40000.Vertices[^1]);
    }
}