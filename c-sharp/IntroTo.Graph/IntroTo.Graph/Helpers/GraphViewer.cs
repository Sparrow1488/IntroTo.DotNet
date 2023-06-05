using System.Text;

namespace IntroTo.Graph.Helpers;

public static class GraphViewer
{
    public static void ShowMatrix(Graph graph)
    {
        var matrix = graph.GetMatrix();
        var builder = new StringBuilder();

        const int padding = 1;
        var maxVertexIdLength = graph.Vertices.Max(x => x.Id).ToString().Length;

        builder.AppendLine(new string(' ', maxVertexIdLength + padding) + string.Join(" ", graph.Vertices.OrderBy(x => x.Id).Select(x => x.Id)));
        for (var row = 0; row < matrix.GetLength(0); row++)
        {
            var currentRow = row + 1;
            builder.Append(currentRow + new string(' ', maxVertexIdLength - currentRow.ToString().Length + padding));
            for (var column = 0; column < matrix.GetLength(1); column++)
            {
                var currentPosition = column + 1;
                var margin = string.Empty;
                if (column - 1 >= 0)
                {
                    var positionLength = currentPosition.ToString().Length;
                    var previousWeightLength = matrix[row, column - 1].ToString().Length;
                    margin = new string(' ', positionLength - previousWeightLength + 1);
                }
                
                builder.Append(margin + matrix[row, column]);
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