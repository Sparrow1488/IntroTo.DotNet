using IntroTo.Graph.Structures;

namespace IntroTo.Graph.Algorithms;

public static class DfsAlgorithm
{
    private static readonly HashSet<Vertex> Visited = new();
    private static readonly Dictionary<Vertex, Vertex> Prior = new();

    public static List<Vertex> DfsStackPath(Structures.Graph graph, Vertex start, Vertex finish)
    {
        DfsStack(graph, start, start, finish);
        return GetPath(start, finish);
    }

    private static void DfsStack(Structures.Graph graph, Vertex previous, Vertex current, Vertex finish)
    {
        if (Visited.Contains(current))
            return;

        Prior[current] = previous;

        if (current == finish)
            return;
        
        Visited.Add(current);

        foreach (var adjacent in graph.GetAdjacentVertices(current))
            DfsStack(graph, current, adjacent, finish);
    }

    private static List<Vertex> GetPath(Vertex start, Vertex finish)
    {
        if (!Prior.ContainsKey(finish) && !Prior.ContainsKey(start))
            return new List<Vertex>();
        
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

    public static bool Dfs(Structures.Graph graph, Vertex start, Vertex finish) 
    {
        var stack = new Stack<Vertex>();
        var visited = new HashSet<Vertex>();
        
        stack.Push(start);
        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (current == finish)
                return true;

            foreach (var adjacent in graph.GetAdjacentVertices(current))
            {
                if (!visited.Contains(adjacent))
                {
                    stack.Push(adjacent);
                    visited.Add(adjacent);
                }
            }
            
            visited.Add(current);
        }

        return false;
    }
}