using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private Cart cart;

        public OrderController(IOrderRepository orderRepository, Cart cart)
        {
            _orderRepository = orderRepository;
            this.cart = cart;
        }

        public IActionResult CheckOut()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            if (cart.Lines.Count == 0)
                ModelState.AddModelError("", "Cart is empty");

            if(!ModelState.IsValid)
                return View();

            order.Lines = cart.Lines.ToArray();
            _orderRepository.SaveOrder(order);
            cart.Clear();
            return RedirectToPage("/Completed", new {orderId = order.OrderID});
        }
    }
}
