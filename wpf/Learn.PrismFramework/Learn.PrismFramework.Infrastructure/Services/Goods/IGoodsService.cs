using Learn.PrismFramework.Models.Goods;

namespace Learn.PrismFramework.Infrastructure.Services.Goods;

public interface IGoodsService
{
    IEnumerable<Good> GetAll();
}