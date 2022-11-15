using System.Linq;

namespace SportsStore.Models.Repository
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext ctx;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<Product> Products => ctx.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                ctx.Products.Add(product);
            }
            else
            {
                var dbEntry = ctx.Products.FirstOrDefault(x => x.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            ctx.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            var dbEntry = ctx.Products.FirstOrDefault(p => p.ProductID == productId);
            if (dbEntry != null)
            {
                ctx.Products.Remove(dbEntry);
                ctx.SaveChanges();
            }
            return dbEntry;
        }
    }
}
