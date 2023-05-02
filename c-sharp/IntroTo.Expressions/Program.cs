Console.WriteLine("Hello, World!");

var query = new EnumerableQuery<int>(new[] { 1, 2, 3 });

var query1 = query.Where(x => x == 1);

var query2 = query.Select(x => x == 2);

var query3 = query.GroupBy(x => x == 2);

Console.WriteLine(query3.ToString());