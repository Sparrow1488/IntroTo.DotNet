namespace IntroTo.Graph.Structures;

public class Edge
{
    public Edge(Vertex from, Vertex to, int weight = 1)
    {
        From = from;
        To = to;
        Weight = weight;
    }

    public Vertex From { get; }

    public Vertex To { get; }

    public int Weight { get; }

    public override string ToString() => $"({From.Id}; {To.Id})";
}