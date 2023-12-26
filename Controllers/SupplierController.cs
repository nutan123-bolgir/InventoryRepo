﻿using InventoryRepo.Models;
using InventoryRepo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryRepo.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepo supplierRepo;
        private readonly IProductRepo _productRepo;
        public SupplierController(ISupplierRepo supplierRepo, IProductRepo _productRepo)
        {
            this.supplierRepo = supplierRepo;
            this._productRepo = _productRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var products = await _productRepo.GetAllAsync();
            ViewBag.Products = new SelectList(products, "ProductId", "ProductName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Supplier supplier, IFormFile file)
        {
            var suppliers = new Supplier
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactPerson = supplier.ContactPerson,
                ContactNumber = supplier.ContactNumber,
                Email = supplier.Email,
                IsActive = supplier.IsActive,
                SupplierPhoto = supplier.SupplierPhoto,
                ProductId = supplier.ProductId

            };

            await supplierRepo.AddAsync(suppliers, file);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var suppliers = await supplierRepo.GetAllAsync();
            return View(suppliers);
        }
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await supplierRepo.GetAsync(id);
            var products = await _productRepo.GetAllAsync();
            ViewBag.Products = new SelectList(products, "ProductId", "ProductName");
            if (supplier != null)
            {
                var model = new Supplier
                {
                    SupplierId = supplier.SupplierId,
                    SupplierName = supplier.SupplierName,
                    ContactNumber = supplier.ContactNumber,
                    ContactPerson = supplier.ContactPerson,
                    Email = supplier.Email,
                    IsActive = supplier.IsActive,
                    SupplierPhoto = supplier.SupplierPhoto,
                    ProductId = supplier.ProductId
                    

            };

                return View(model);
            }

            return View(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Supplier supplier, IFormFile file)
        {

            var existingProduct = await supplierRepo.GetAsync(supplier.SupplierId);
           
            if (existingProduct != null)
            {

                existingProduct.SupplierId = supplier.SupplierId;
                existingProduct.SupplierName = supplier.SupplierName;
                existingProduct.ContactNumber = supplier.ContactNumber;
                existingProduct.ContactPerson = supplier.ContactPerson;
                existingProduct.Email = supplier.Email;
                existingProduct.IsActive = supplier.IsActive;
                existingProduct.ProductId = supplier.ProductId;
                existingProduct.Product = supplier.Product;


                if (file != null && file.Length > 0)
                {
                    existingProduct.SupplierPhoto = await _productRepo.SaveImageAsync(file);
                }
                else if (supplier.SupplierPhoto != null)
                {
                    existingProduct.SupplierPhoto = supplier.SupplierPhoto;
                }
                else
                {
                    existingProduct.SupplierPhoto = existingProduct.SupplierPhoto;
                }
                var updatedProduct = await supplierRepo.UpdateAsync(existingProduct);

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
        [HttpPost]
        public async Task<IActionResult> Delete(Supplier supplier)
        {
            var DeleteTag = await supplierRepo.DeleteAsync(supplier.SupplierId);
            if (DeleteTag != null)
            {
                // show success notification
            }
            else
            {
                //show error notification
            }
            return RedirectToAction("Edit", new { id = supplier.SupplierId });

        }


    }
}


