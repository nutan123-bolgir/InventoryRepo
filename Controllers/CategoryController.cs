using Azure;
using InventoryRepo.Models;
using InventoryRepo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryRepo.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepo categoryRepo;
        public CategoryController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            var categorys = new Category
            {
                CategoryName = category.CategoryName,
                Description = category.Description,
            };
            await categoryRepo.AddAsync(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories= await categoryRepo.GetAllAsync();
            return View(categories);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryRepo.GetAsync(id);

            if (category != null)
            {
                var model = new Category
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };

                return View(model);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var tag = new Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
               Description = category.Description,
            };
            var UpdatedTag = await categoryRepo.UpdateAsync(tag);
            if (UpdatedTag != null)
            {
                //success message
            }
            else
            {
                //not success message
            }
            return RedirectToAction("Index", new { id = category.CategoryId });

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryRepo.GetAsync(id);

            if (category != null)
            {
                return View(category);
            }

            return NotFound(); // Or handle as appropriate
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var deletedCategory = await categoryRepo.DeleteAsync(id);

            if (deletedCategory != null)
            {
                // Show success notification
                TempData["SuccessMessage"] = "Category deleted successfully.";
            }
            else
            {
                // Show error notification
                TempData["ErrorMessage"] = "Error deleting the category.";
            }

            return RedirectToAction("Index");
        }



    }
}
