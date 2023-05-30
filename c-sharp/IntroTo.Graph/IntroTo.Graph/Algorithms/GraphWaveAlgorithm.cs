namespace IntroTo.Graph.Algorithms;

public class GraphWaveAlgorithm : IAlgorithm<Graph, IList<Vertex>, WaveAlgorithmArgs>
{
    /// <summary>
    /// Выполняет алгоритм волнового поиска
    /// </summary>
    /// <param name="graph">Граф</param>
    /// <param name="args">Указанные вершины A и B</param>
    /// <returns>Маршрут от вершины A до вершины B</returns>
    public IList<Vertex> Execute(Graph graph, WaveAlgorithmArgs args)
    {
        // TODO: переделать, это какая то шляпа
        var route = new List<Vertex>
        {
            args.A
        };

        for (var i = 0; i < route.Count; i++)
        {
            foreach (var vertex in graph.GetAdjacentVertices(route[i]))
                if (!route.Contains(vertex))
                    route.Add(vertex);
        }

        return route;
    }
}

public record WaveAlgorithmArgs(
    Vertex A,
    Vertex B
);