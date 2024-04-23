using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Pages  //code-behind page for cart.cshtml
{
    public class CartModel : PageModel  //this page controls the logic behind the view.
    {
        private readonly IServiceManager _manager;
        public Cart _cart { get; set; } //IoC
        public string ReturnUrl { get; set; } = "/";

        public CartModel(IServiceManager manager, Cart cartService)
        {
            _manager = manager;
            _cart = cartService;
        }


        public void OnGet(string returnUrl) //to direct the user wherever he came from
        {
            ReturnUrl = returnUrl ?? "/"; //if returnUrl is null, it's /
           // _cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(); //if there's Cart in the session, bring me, if not, create new cart.
        }

        public IActionResult OnPost(int productId, string returnUrl) //to add products. This is OnPost method. "post"
        {
            Product? product = _manager
                .ProductService
                .GetOneProduct(productId, false); //we access service layer to get a product by using the productId coming from the productcard.

            if (product is not null)
            {
                //_cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(); //we read cart object from session, if none, we create new one.
                _cart.AddItem(product, 1); //we add the new item to the cart object.
               // HttpContext.Session.SetJson<Cart>("cart", _cart); //cuz the cart object is changed, now we set it again to session, we write it on session.
            }
            return RedirectToPage(new {returnUrl = returnUrl}); //returnUrl for later.
        }

        public IActionResult OnPostRemove(int id, string returnUrl) //asp-page-handler tag helper will be used to direct the user to this method istead of the one above.
        {
           // _cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(); //we read cart object from session, if none, we create new one. 
            _cart.RemoveLine(_cart.Lines.First(cl => cl.Product.ProductId.Equals(id)).Product);
            //HttpContext.Session.SetJson<Cart>("cart", _cart); //cuz the cart object is changed, now we set it again to session, we write it on session.
            return Page();
        }

    }
}