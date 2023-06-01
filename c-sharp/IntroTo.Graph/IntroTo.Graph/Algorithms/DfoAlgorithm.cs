using System.Transactions;

namespace IntroTo.Graph.Algorithms;

public static class DfoAlgorithm
{
    private static readonly HashSet<Vertex> Visited = new();
    private static readonly Dictionary<Vertex, Vertex> Prior = new();

    public static List<Vertex> DfoStackPath(Graph graph, Vertex start, Vertex finish)
    {
        DfoStack(graph, start, start, finish);
        return GetPath(start, finish);
    }

    private static void DfoStack(Graph graph, Vertex previous, Vertex current, Vertex finish)
    {
        if (Visited.Contains(current))
            return;

        if (Prior.TryAdd(current, previous))
            Prior[current] = previous;

        if (current == finish)
            return;
        
        Visited.Add(current);

        foreach (var adjacent in graph.GetAdjacentVertices(current))
            DfoStack(graph, current, adjacent, finish);
    }

    private static List<Vertex> GetPath(Vertex start, Vertex finish)
    {
        var path = new List<Vertex> { finish };
        var current = finish;
        while (current != start)
        {
            current = Prior[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
    
    public static bool Dfo(Graph graph, Vertex start, Vertex finish) 
    {
        var stack = new Stack<Vertex>();
        var visited = new HashSet<Vertex>();
        
        stack.Push(start);
        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (current == finish)
                return true;
            
            if (visited.Contains(current))
                continue;

            foreach (var adjacent in graph.GetAdjacentVertices(current))
                if (!visited.Contains(adjacent))
                    stack.Push(adjacent);
            
            visited.Add(current);
        }

        return false;
    }
}