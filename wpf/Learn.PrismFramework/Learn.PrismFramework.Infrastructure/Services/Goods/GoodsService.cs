using Learn.PrismFramework.Models.Goods;

namespace Learn.PrismFramework.Infrastructure.Services.Goods;

public class GoodsService : IGoodsService
{
    private readonly List<Good> _goods;
    
    public GoodsService()
    {
        _goods = new List<Good>
        {
            new() { Id = 1, Name = "Banana", Price = 20.0 },
            new() { Id = 2, Name = "Apple", Price = 15.0 },
            new() { Id = 3, Name = "Orange", Price = 25.0 },
        };
    }

    public IEnumerable<Good> GetAll() => _goods;
}