namespace Learn.MoreLinq;

public static class Initializer
{
    public static List<Item> SeedItems()
    {
        return new List<Item>
        {
            new("A"),
            new("B"),
            new("C"),
            new("1"),
            new("2"),
            new("3"),
        };
    }
    
    public static List<int> SeedNumbers()
    {
        var list = new List<int>();
        for (var i = 0; i < 5; i++)
        {
            list.Add(Random.Shared.Next(0, 10));
        }

        return list;
    }
    
    public static List<string> SeedRows()
    {
        return new List<string>
        {
            "a",
            "b",
            "c",
            "1",
            "2",
            "3"
        };
    }
}