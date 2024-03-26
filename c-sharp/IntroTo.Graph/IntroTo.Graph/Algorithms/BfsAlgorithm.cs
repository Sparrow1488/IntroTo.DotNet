using IntroTo.Graph.Structures;

namespace IntroTo.Graph.Algorithms;

public static class BfsAlgorithm
{
    public static List<Vertex> Bfs2(Structures.Graph graph, Vertex start, Vertex finish)
    {
        var visited = new HashSet<Vertex>();
        var history = new Dictionary<Vertex, LinkedList<Vertex>>();
        
        var queue = new Queue<Vertex>();
        queue.Enqueue(start);

        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var adjacent in graph.GetAdjacentEdges(current))
            {
                var adjacentVertex = adjacent.To != current
                    ? adjacent.To
                    : adjacent.From;
                
                if (history.TryGetValue(adjacentVertex, out var previous))
                {
                    previous.AddLast(current);
                }
                else
                {
                    var list = new LinkedList<Vertex>();
                    list.AddLast(current);
                    history.Add(adjacentVertex, list);
                }
                
                if (!visited.Contains(adjacentVertex))
                {
                    visited.Add(adjacentVertex);
                    queue.Enqueue(adjacentVertex);

                    if (adjacentVertex == finish)
                        return GetPath(start, finish, history);
                }
            }
        }

        return new List<Vertex>();
    }
    
    public static List<Vertex> Bfs(Structures.Graph graph, List<Vertex> vertices)
    {
        var path = new List<Vertex> { vertices[0] };

        var finishIndex = 1;
        while (finishIndex < vertices.Count)
        {
            var start = vertices[finishIndex - 1];
            var finish = vertices[finishIndex - 0];
            path.AddRange(Bfs(graph, start, finish).Skip(1));
            
            finishIndex++;
        }

        return path;
    }
    
    public static List<Vertex> Bfs(Structures.Graph graph, Vertex start, Vertex finish)
    {
        var visited = new HashSet<Vertex>(); // black
        var history = new Dictionary<Vertex, LinkedList<Vertex>>();
        
        var queue = new Queue<Vertex>();
        queue.Enqueue(start);

        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var adjacent in graph.GetAdjacentVertices(current))
            {
                if (history.TryGetValue(adjacent, out var previous))
                {
                    previous.AddLast(current);
                }
                else
                {
                    var list = new LinkedList<Vertex>();
                    list.AddLast(current);
                    history.Add(adjacent, list);
                }
                
                if (!visited.Contains(adjacent))
                {
                    visited.Add(adjacent);
                    queue.Enqueue(adjacent);

                    if (adjacent == finish)
                        return GetPath(start, finish, history);
                }
            }
        }

        return new List<Vertex>();
    }

    private static List<Vertex> GetPath(Vertex start, Vertex finish, Dictionary<Vertex, LinkedList<Vertex>> history)
    {
        var path = new List<Vertex> { finish };
        var current = finish;
        
        while (current != start)
        {
            var visitedList = history[current];
            var previous = visitedList.First!.Value;
            current = previous;
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
    
    public static void Bfs(Structures.Graph graph, Vertex start)
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