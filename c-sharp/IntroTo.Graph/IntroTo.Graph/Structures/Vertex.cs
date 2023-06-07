namespace IntroTo.Graph.Structures;

public class Vertex
{
    public Vertex(int id)
    {
        Id = id;
    }
    
    public int Id { get; }

    public override string ToString() => Id.ToString();
}