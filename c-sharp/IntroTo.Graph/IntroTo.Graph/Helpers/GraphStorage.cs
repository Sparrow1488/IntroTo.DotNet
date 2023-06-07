using IntroTo.Graph.Contracts;

namespace IntroTo.Graph.Helpers;

public static class GraphStorage
{
    public static IGraph GetType1(bool hashNeighbours)
    {
        var vertices = Enumerable.Range(1, 11).Select(x => new Vertex(x - 1)).ToList();
        var edges = new List<Edge>
        {
            new(vertices[1], vertices[2]),
            new(vertices[1], vertices[3]),
            new(vertices[2], vertices[4]),
            new(vertices[2], vertices[5]),
            new(vertices[3], vertices[6]),
            new(vertices[3], vertices[7]),
            new(vertices[4], vertices[8]),
            new(vertices[4], vertices[5]),
            new(vertices[5], vertices[6]),
            new(vertices[6], vertices[9]),
            new(vertices[6], vertices[10]),
            new(vertices[7], vertices[9]),
            new(vertices[9], vertices[10]),
            new(vertices[10], vertices[8])
        };

        return new Graph(edges, vertices.SkipLast(1).ToList(), hashNeighbours);
    }
}