using IntroTo.Graph.Structures;

namespace IntroTo.Graph.Contracts;

public interface IGraph
{
    IReadOnlyList<Edge> Edges { get; }
    IReadOnlyList<Vertex> Vertices { get; }
    IReadOnlyDictionary<Vertex, VertexNeighbours>? VertexNeighbours { get; }

    int[,] GetMatrix();
    IEnumerable<Vertex> GetAdjacentVertices(Vertex vertex);
}