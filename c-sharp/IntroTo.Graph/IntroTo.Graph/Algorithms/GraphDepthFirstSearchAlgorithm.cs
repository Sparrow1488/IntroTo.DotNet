namespace IntroTo.Graph.Algorithms;

public class GraphDepthFirstSearchAlgorithm : IAlgorithm<Graph, List<Vertex>, DfsArgs>
{
    private static readonly Dictionary<int, Vertex> Visited = new();
    private static readonly List<Vertex> Route = new();
    
    private static bool WasFound { get; set; }
    private static Graph Graph { get; set; } = null!;

    public List<Vertex> Execute(Graph input, DfsArgs args)
    {
        Graph = input;
        
        SearchInDepth(args.Start, args.Finish);
        Route.Add(args.Start);

        Route.Reverse();
        return Route;
    }

    private static void SearchInDepth(Vertex current, Vertex finish)
    {
        if (Visited.ContainsKey(current.Id))
        {
            return;
        }

        if (current == finish)
        {
            WasFound = true;
            return;
        }
        
        Visited.Add(current.Id, current);

        foreach (var adjacent in Graph.GetAdjacentVertices(current))
        {
            SearchInDepth(adjacent, finish);
            
            if (WasFound)
            {
                Route.Add(adjacent);
                return;
            }
        }
    }
}

public record DfsArgs(
    Vertex Start,
    Vertex Finish
);