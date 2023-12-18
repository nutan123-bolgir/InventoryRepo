using Microsoft.AspNetCore.Mvc;

namespace InventoryRepo.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
