using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models.Repository
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products =>
            new List<Product>
            {
                new Product { Name = "Football", Price = 25 },
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running shoes", Price = 95 }
            }.AsQueryable();

        public Product DeleteProduct(int productId)
        {
            throw new System.NotImplementedException();
        }

        public void SaveProduct(Product product)
        {
            throw new System.NotImplementedException();
        }
    }
}
