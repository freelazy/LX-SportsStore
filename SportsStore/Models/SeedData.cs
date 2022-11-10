using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<ApplicationDbContext>();
                ctx.Database.Migrate();

                if (!ctx.Products.Any())
                {
                    ctx.Products.AddRange(
                        new Product { Name = "Kayak", Description = "A boat for one person", Price = 275, Category = "Watersports" },
                        new Product { Name = "Lifejacket", Description = "Protective and fashionable", Price = 48.95M, Category = "Watersports" },
                        new Product { Name = "Soccer Ball", Description = "FIFA-approved size and weight", Price = 19.50M, Category = "Soccer" },
                        new Product { Name = "Corner Flags", Description = "Give your playing field a professional touch", Price = 34.95M, Category = "Soccer" },
                        new Product { Name = "Stadium", Description = "Flat-packed 35,000-seat stadium", Price = 79500, Category = "Soccer" },
                        new Product { Name = "Thinking Cap", Description = "Improve brain efficiency by 75%", Price = 16, Category = "Chess" },
                        new Product { Name = "Unsteady Chair", Description = "Secretly give your opponent a disadvantage", Price = 29.95M, Category = "Chess" },
                        new Product { Name = "Human Chess Board", Description = "A fun game for the family", Price = 75, Category = "Chess" },
                        new Product { Name = "Bling-Bling King", Description = "Gold-plated, diamond-studded King", Price = 1200, Category = "Chess" }
                    );
                    ctx.SaveChanges();
                }
            }
        }
    }
}
