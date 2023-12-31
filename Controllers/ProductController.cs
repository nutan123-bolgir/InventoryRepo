﻿using InventoryRepo.Models;
using InventoryRepo.Repository;
using InventoryRepo.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo ProductRepo;
        private readonly ICategoryRepo categoryRepo;
        public ProductController(IProductRepo productRepo,ICategoryRepo categoryRepo)
        {
            this.ProductRepo = productRepo;
            this.categoryRepo = categoryRepo;
        }
        
        [HttpGet]
        public async Task< IActionResult> Add()
        {
            var categories = await categoryRepo.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product,IFormFile file)
        {
            var category = await categoryRepo.GetAsync(product.CategoryId);
            var newProducts = new Product
            {
                ProductName = product.ProductName,
                ProductId = product.ProductId,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                CategoryId = category.CategoryId,
                Category=category

            };
           
            await ProductRepo.AddAsync(newProducts, file);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await ProductRepo.GetAllAsync();
            return Json(products);
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var products = await ProductRepo.GetAllAsync();



            var productViewModels = products.Select(p => new ProductView
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                IsActive = p.IsActive,
                CategoryName = p.Category?.CategoryName, 
                ProductImage = p.ProductImage
            }).ToList();

            return View(productViewModels);

        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await ProductRepo.GetAsync(id); ;
            
            if (product != null)
            {
                var categories = await categoryRepo.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
                var model = new Product
                {
                    ProductName = product.ProductName,
                    ProductId = product.ProductId,
                    Price = product.Price,
                    ProductImage=product.ProductImage,
                    StockQuantity = product.StockQuantity,
                    IsActive = product.IsActive,
                    CategoryId = product.CategoryId, 
                    Category = product.Category

                };

                return View(model);
            }

            return View(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile file)
        {
            
            var existingProduct = await ProductRepo.GetAsync(product.ProductId);

            if (existingProduct != null)
            {
               
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.IsActive = product.IsActive;
                existingProduct.CategoryId = product.CategoryId; 
                
                if (file != null && file.Length > 0)
                {
                    existingProduct.ProductImage = await ProductRepo.SaveImageAsync(file);
                }
                else if(product.ProductImage != null) 
                {
                    existingProduct.ProductImage = product.ProductImage;
                }
               else
                {
                    existingProduct.ProductImage = existingProduct.ProductImage;
                }
                    var updatedProduct = await ProductRepo.UpdateAsync(existingProduct);

                if (updatedProduct != null)
                {
                    // Success message
                }
                else
                {
                    // Not success message
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await ProductRepo.GetAsync(id);

            if (product != null)
            {
                var productViewModel = new ProductView
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    IsActive = product.IsActive,
                    CategoryName = product.Category?.CategoryName,
                    ProductImage = product.ProductImage
                };

                return View(productViewModel);
            }

            return NotFound(); // Or handle as appropriate
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var deletedProduct = await ProductRepo.DeleteAsync(id);

            if (deletedProduct != null)
            {
                // Show success notification
                TempData["SuccessMessage"] = "Product deleted successfully.";
            }
            else
            {
                // Show error notification
                TempData["ErrorMessage"] = "Error deleting the product.";
            }

            return RedirectToAction("Index");
        }



    }
}
