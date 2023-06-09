using BenchmarkDotNet.Attributes;
using IntroTo.Graph.Algorithms;

namespace IntroTo.Graph.Benchmark;

[RankColumn]
[MemoryDiagnoser]
public class BfsAlgorithmsBenchmark
{
    [Benchmark]
    public void Bfs100()
    {
        BfsAlgorithm.Bfs(GraphProvider.Graph100, GraphProvider.Graph100.Vertices[0], GraphProvider.Graph100.Vertices[^1]);
    }

    [Benchmark]
    public void Bfs10000()
    {
        BfsAlgorithm.Bfs(GraphProvider.Graph10000, GraphProvider.Graph10000.Vertices[0], GraphProvider.Graph10000.Vertices[^1]);
    }
    
    [Benchmark]
    public void Bfs40000()
    {
        BfsAlgorithm.Bfs(GraphProvider.Graph40000, GraphProvider.Graph40000.Vertices[0], GraphProvider.Graph40000.Vertices[^1]);
    }
}