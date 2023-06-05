namespace IntroTo.Graph.Algorithms;

public static class BfsAlgorithm
{
    public static List<Vertex> Bfs(Graph graph, Vertex start, Vertex finish)
    {
        var visited = new HashSet<Vertex>(); // black
        var calculated = new Dictionary<Vertex, int>(); // gray
        var history = new Dictionary<Vertex, List<Vertex>>();
        
        var queue = new Queue<Vertex>();
        queue.Enqueue(start);

        visited.Add(start);
        calculated[start] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var adjacent in graph.GetAdjacentVertices(current))
            {
                if (!history.TryAdd(adjacent, new List<Vertex> { current }))
                {
                    var previous = history[adjacent];
                    previous.Add(current);
                }
                
                if (!visited.Contains(adjacent) && !calculated.ContainsKey(adjacent))
                {
                    calculated.Add(adjacent, calculated[current] + 1);
                    visited.Add(adjacent);
                    queue.Enqueue(adjacent);

                    if (adjacent == finish)
                    {
                        return GetPath(start, finish, history);
                    }
                }
            }
        }

        return new List<Vertex>();
    }

    private static List<Vertex> GetPath(Vertex start, Vertex finish, Dictionary<Vertex, List<Vertex>> history)
    {
        var path = new List<Vertex> { finish };
        var current = finish;
        
        while (current != start)
        {
            var visitedList = history[current];
            var previous = visitedList.First();
            current = previous;
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
    
    public static void Bfs(Graph graph, Vertex start)
    {
        var visited = new HashSet<Vertex>(); // black
        var calculated = new Dictionary<Vertex, int>(); // gray
        
        var queue = new Queue<Vertex>();
        queue.Enqueue(start);

        visited.Add(start);
        calculated[start] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var adjacent in graph.GetAdjacentVertices(current))
            {
                if (!visited.Contains(adjacent) && !calculated.ContainsKey(adjacent))
                {
                    calculated.Add(adjacent, calculated[current] + 1);
                    visited.Add(adjacent);
                    queue.Enqueue(adjacent);
                }
            }
        }
    }
}