using System.Linq;

namespace SportsStore.Models.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
