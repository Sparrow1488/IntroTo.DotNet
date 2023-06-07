using System.Text;
using IntroTo.Graph.Contracts;

namespace IntroTo.Graph.Views;

public class GraphMatrixView : ConsoleDataView<IGraph>
{
    public GraphMatrixView(IGraph graph)
    {
        Graph = graph;
        Matrix = Graph.GetMatrix();
    }

    private IGraph Graph { get; }
    private int[,] Matrix { get; }

    public override string ToString()
    {
        var builder = new StringBuilder();

        const int padding = 1;
        var maxVertexIdLength = Graph.Vertices.Max(x => x.Id).ToString().Length;

        builder.AppendLine(new string(' ', maxVertexIdLength + padding) + string.Join(" ", Graph.Vertices.OrderBy(x => x.Id).Select(x => x.Id)));
        for (var row = 0; row < Matrix.GetLength(0); row++)
        {
            var currentRow = row + 1;
            builder.Append(currentRow + new string(' ', maxVertexIdLength - currentRow.ToString().Length + padding));
            for (var column = 0; column < Matrix.GetLength(1); column++)
            {
                var currentPosition = column + 1;
                var margin = string.Empty;
                if (column - 1 >= 0)
                {
                    var positionLength = currentPosition.ToString().Length;
                    var previousWeightLength = Matrix[row, column - 1].ToString().Length;
                    margin = new string(' ', positionLength - previousWeightLength + 1);
                }
                
                builder.Append(margin + Matrix[row, column]);
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }
}