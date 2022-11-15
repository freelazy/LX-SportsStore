using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.Repository;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private readonly Cart cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            this.repository = repository;
            this.cart = cartService;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                returnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
