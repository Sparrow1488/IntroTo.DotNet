using System.Text;

namespace IntroTo.Graph.Helpers;

public static class GraphViewer
{
    public static void ShowMatrix(Graph graph)
    {
        var matrix = graph.GetMatrix();
        var builder = new StringBuilder();

        builder.AppendLine("  " + string.Join(" ", graph.Vertices.OrderBy(x => x.Id).Select(x => x.Id)));
        for (var row = 0; row < matrix.GetLength(0); row++)
        {
            builder.Append(row + 1 + " ");
            for (var column = 0; column < matrix.GetLength(1); column++)
            {
                builder.Append(matrix[row, column] + " ");
            }

            builder.AppendLine();
        }

        Console.WriteLine(builder.ToString());
    }

    public static void ShowVerticesList(Graph graph)
    {
        var list = graph.GetVerticesList();
        var builder = new StringBuilder();

        var keys = list.Keys.OrderBy(x => x.Id).ToList();
        foreach (var key in keys)
        {
            builder.AppendLine($"{key.Id} -> " + string.Join(", ", list[key].OrderBy(x => x.Id).Select(x => x.Id)));
        }
        
        Console.WriteLine(builder.ToString());
    }
}