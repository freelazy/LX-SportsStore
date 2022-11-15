﻿using System.Linq;

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
    }
}