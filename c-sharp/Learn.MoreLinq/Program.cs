using Learn.MoreLinq;
using MoreLinq;
using MoreLinq.Extensions;

Console.WriteLine("Hello, World!");

// More docs: https://www.nuget.org/packages/morelinq

var items = Initializer.SeedItems();
var numbers = Initializer.SeedNumbers();
var rows = Initializer.SeedRows();

var random = MoreEnumerable.RandomSubset(items, subsetSize: items.Count - 1).ToList();

var aggregateNums = numbers.Aggregate((x, y) => x + y);
var aggregateRows = rows.Aggregate((x, y) => x + ", " + y);

var appendRows = AppendExtension.Append(rows,"tail element");

var excluded = ExcludeExtension.Exclude(rows, 3, 1);

Console.WriteLine("End");