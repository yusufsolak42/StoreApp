using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var model = _manager.ProductService.GetAllProducts(false);
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoriesSelectList();

            return View();
        }


        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(_manager.CategoryService.GetAllCategories(false), //we send the viewbag to create.cshtml
            "CategoryId",
            "CategoryName", "1");             //we take the categories and pass to view, as viewbag

        }



        [HttpPost] //the user entered all the required values, time to take action.
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file) //we use Dto's instead of the entity itself to introduce immutability and validation logic. Seperation of concerns.
        {
            if (ModelState.IsValid)
            {

                //file operations
                string path = Path.Combine(Directory.GetCurrentDirectory(), 
                "wwwroot", "images", file.FileName);

                using(var stream  = new FileStream(path,FileMode.Create)) //we use using to minimize workload, 
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName); //we set ImageUrl here.
                _manager.ProductService.CreateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Update([FromRoute(Name = "id")] int id) //get method. We tell which one will be updated so we get it first.
        {
            ViewBag.Categories = GetCategoriesSelectList();
            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //file operations
                string path = Path.Combine(Directory.GetCurrentDirectory(), 
                "wwwroot", "images", file.FileName);

                using(var stream  = new FileStream(path,FileMode.Create)) //we use using to minimize workload, 
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName); //we set ImageUrl here.
                _manager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            return RedirectToAction("Index");
        }
    }
}