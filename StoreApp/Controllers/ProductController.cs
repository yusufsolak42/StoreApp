using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly IServiceManager _manager; //We connect Controller to service manager.

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(ProductRequestParameters p) 
        {

            var products = _manager.ProductService.GetAllProductsWithDetails(p);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
            };

            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            }); 
            
            
            //we send the "model" variable to View file "index.cshtml"(then we display all products by using foreach loop)
        }

        public IActionResult Get([FromRoute(Name = "id")] int id) //takes Id as argument.
        {
            var model = _manager.ProductService.GetOneProduct(id, false);
            return View(model);
        }

    }
}