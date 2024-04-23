using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly Cart _cart;

        public OrderController(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            _cart = cart;
        }

        public ViewResult CheckOut() => View(new Order());  // this smybol => means return the coming expression. this is the get request. We send empty order object so the customer will fill the form and be able to send it as HttpPost method.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut([FromForm] Order order)
        {
            if(_cart.Lines.Count()==0)
            {
              ModelState.AddModelError("","Sorry, your cart is empty.");  
            }
            if(ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray(); // I carry the lines to order.
                _manager.OrderService.SaveOrder(order); //i send the coming order as parameter.
                _cart.Clear(); //both class and cart in session will be cleaned.
                return RedirectToPage("/Complete", new {OrderId = order.OrderId});
            }
            else //if model fails, this part will work, will return to the existing page.
            {
                return View();
            }
        }
    }   
}