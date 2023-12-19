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
            return RedirectToAction("Edit", new { id = category.CategoryId });

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            var DeleteTag = await categoryRepo.DeleteAsync(category.CategoryId);
            if (DeleteTag != null)
            {
                // show success notification
            }
            else
            {
                //show error notification
            }
            return RedirectToAction("Edit", new { id = category.CategoryId });

        }


    }
}
