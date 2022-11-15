using System.Linq;

namespace SportsStore.Models.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productId);
    }
}
