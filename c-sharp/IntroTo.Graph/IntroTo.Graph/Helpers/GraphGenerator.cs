using IntroTo.Graph.Contracts;
using IntroTo.Graph.Structures;

namespace IntroTo.Graph.Helpers;

public static class GraphGenerator
{
    public static IGraph GenerateCellField(int size, bool hashVerticesList = false)
    {
        var field = new Vertex[size, size];
        var edges = new List<Edge>();

        field[0, 0] = new Vertex(1);
        
        var counter = 2;
        for (var rowI = 0; rowI < size; rowI++)
        {
            for (var columnI = 0; columnI < size; columnI++)
            {
                var current = field[rowI, columnI];

                if (columnI + 1 < size) // next
                {
                    field[rowI, columnI + 1] ??= new Vertex(counter);
                    edges.Add(new Edge(current, field[rowI, columnI + 1], 1));
                }

                if (rowI + 1 < size) // bottom
                {
                    field[rowI + 1, columnI] ??= new Vertex(counter + size - 1);
                    edges.Add(new Edge(current, field[rowI + 1, columnI], 1));
                }

                counter++;
            }
        }

        var verticesList = new List<Vertex>(size * size);
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                var vertex = field[i, j];
                verticesList.Add(vertex);
            }
        }
        return new Structures.Graph(edges, verticesList, hashVerticesList);
    }
}