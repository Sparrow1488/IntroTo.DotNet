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
        
        SearchInDepth(args.Start, args.Finish, Route);

        return Route;
    }


    private static void SearchInDepth(Vertex current, Vertex finish, List<Vertex> route)
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
        
        var depthRoute = new List<Vertex>();
        Visited.Add(current.Id, current);
        depthRoute.Add(current);

        foreach (var adjacent in Graph.GetAdjacentVertices(current))
        {
            SearchInDepth(adjacent, finish, depthRoute);
            
            if (WasFound)
            {
                if (adjacent == finish)
                {
                    depthRoute.Add(adjacent);
                }
                route.AddRange(depthRoute);
                return;
            }
        }
    }
}

public record DfsArgs(
    Vertex Start,
    Vertex Finish
);