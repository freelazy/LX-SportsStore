using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Components;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"}
            }).AsQueryable());
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            var results = controller.List(null, 2).ViewData.Model as ProductListViewModel;

            // Assert
            var prodArray = results.Products.ToArray();
            Assert.True(2 == prodArray.Length);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model ()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"}
            }).AsQueryable());

            var controller = new ProductController(mock.Object) { PageSize = 3 };

            var results = controller.List(null, 2).ViewData.Model as ProductListViewModel;
            var pageInfo = results.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
            }).AsQueryable());

            var controller = new ProductController(mock.Object) { PageSize = 3 };

            var results = (controller.List("Cat2", 1).ViewData.Model as ProductListViewModel)
                .Products.ToArray();

            Assert.Equal(2, results.Length);
            Assert.True(results[0].Name == "P2" && results[0].Category == "Cat2");
            Assert.True(results[1].Name == "P4" && results[1].Category == "Cat2");
        }
        [Fact]
        public void Can_Select_Categories()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                new Product { ProductID = 4, Name = "P4", Category = "Oranges" }
            }).AsQueryable());

            var target = new NavigationMenuViewComponent(mock.Object);

            var results = ((IEnumerable<string>)(target.Invoke() as ViewComponentResult)
                .ViewData.Model).ToArray();

            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums"}, results));
        }
    }
}
