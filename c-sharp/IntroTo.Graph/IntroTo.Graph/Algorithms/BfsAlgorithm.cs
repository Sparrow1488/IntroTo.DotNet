namespace IntroTo.Graph.Algorithms;

public static class BfsAlgorithm
{
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