using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers 
{
    [Area("Admin")] //for tag helpers to match endpoints with controller.
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    } 
}