using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Repository;
using System.Linq;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    { 
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        /// <summary>
        /// 显示所有未发货的订单
        /// </summary>
        public IActionResult List() =>
            View(repository.Orders.Where(o => !o.Shipped));

        /// <summary>
        /// 卖家发货之后，将订单设置为已发货状态
        /// </summary>
        public IActionResult MarkShipped(int orderId)
        {
            var order = repository.Orders.FirstOrDefault(o => o.OrderID == orderId);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        /// <summary>
        /// 显示下单页面，在页面上要呈现地址和支付方式
        /// </summary>
        public IActionResult Checkout()
        {
            return View(new Order());
        }

        /// <summary>
        /// 信息填写完毕、支付完成之后，就要生成订单信息了
        /// </summary>
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, cart is empty");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        /// <summary>
        /// 订单成功生成的页面。提示用户耐心等待收货
        /// </summary>
        public IActionResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
