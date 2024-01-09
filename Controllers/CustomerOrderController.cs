using Humanizer;
using InventoryRepo.Models;
using InventoryRepo.Models.ViewModel;
using InventoryRepo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Controllers
{
    public class CustomerOrderController : Controller
    {
        
        private readonly ICustomerOrderRepo _customerOrderRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IProductRepo _productRepo;
        private readonly IItemRepo _itemRepo;

        public CustomerOrderController(ICustomerOrderRepo customerOrderRepo, ICustomerRepo customerRepo, IProductRepo productRepo, IItemRepo itemRepo)
        {
            _customerOrderRepo = customerOrderRepo;
            _customerRepo = customerRepo;
            _productRepo = productRepo;
            _itemRepo = itemRepo;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customerOrders = await _customerOrderRepo.GetAllAsync();
            return View(customerOrders);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
            {
            var vm = new CustomerBillViewModel();

           
            var items = await _itemRepo.GetAllAsync();
            vm.ItemList = items.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            vm.Items = new List<OrderedProductViewModel>
            {
                new OrderedProductViewModel { /* Populate properties for the first item */ },
                new OrderedProductViewModel { /* Populate properties for the second item */ }
                // Add more items as needed
            };

            var customers = await _customerRepo.GetAllAsync();
            ViewBag.Customers = new SelectList(customers, "CustomerId", "FirstName");

            var products = await _productRepo.GetAllAsync();

            
            ViewBag.Products = products.Select(x => new ProductDropdownViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitPrice = x.Price ?? 0,
                GSTRate = x.Gstrate ?? 0 // Add GSTRate property
            }).ToList();

            return View(vm); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerBillViewModel viewModel, List<OrderedProductViewModel> Items)
        {
            
                
                
                    // Create CustomerOrder entity from ViewModel
                    var customerOrder = new CustomerOrder
                    {
                        CustomerId = viewModel.CustomerId,
                        GrandTotal = viewModel.GrandTotal
                        // Add other properties as needed
                    };

                    // Create OrderedProduct entities from ViewModel
                    var orderedProducts = Items.Select(item => new OrderedProduct
                    {
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        GstRate = item.GSTRate,
                        Total = item.Total
                        // Add other properties as needed
                    }).ToList();

                    // Add customer order and associated products to the database
                    await _customerOrderRepo.AddOrderWithDetailsAsync(customerOrder, orderedProducts);

                    return RedirectToAction("Index");
                
            

            // If the execution reaches here, there are ModelState errors or null parameters
            // Repopulate dropdowns and redisplay the form with errors
            var items = await _itemRepo.GetAllAsync();
            viewModel.ItemList = items.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            viewModel.Items = Items; // Assuming Items is the collection of OrderedProductViewModels

            var customers = await _customerRepo.GetAllAsync();
            ViewBag.Customers = new SelectList(customers, "CustomerId", "FirstName");

            var products = await _productRepo.GetAllAsync();
            ViewBag.Products = products.Select(x => new ProductDropdownViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitPrice = x.Price ?? 0,
                GSTRate = x.Gstrate ?? 0 // Add GSTRate property
            }).ToList();

            return View("Add", viewModel);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _customerOrderRepo.GetAsync(id);
            if (order != null)
            {
                var customers = await _customerRepo.GetAllAsync();
                var products = await _productRepo.GetAllAsync();

                ViewBag.Customers = new SelectList(customers, "CustomerId", "FullName", order.CustomerId);
                ViewBag.Products = new SelectList(products, "ProductId", "ProductName", order.ProductId);

                return View(order);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                // Validate and process the customerOrder data
                // ...

                var updatedOrder = await _customerOrderRepo.UpdateAsync(customerOrder);
                if (updatedOrder != null)
                {
                    TempData["SuccessMessage"] = "Customer order updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Error updating customer order.";
                }
            }

            var customers = await _customerRepo.GetAllAsync();
            var products = await _productRepo.GetAllAsync();

            ViewBag.Customers = new SelectList(customers, "CustomerId", "FullName", customerOrder.CustomerId);
            ViewBag.Products = new SelectList(products, "ProductId", "ProductName", customerOrder.ProductId);

            return View(customerOrder);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _customerOrderRepo.GetAsync(id);
            if (order != null)
            {
                return View(order);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var deletedOrder = await _customerOrderRepo.DeleteAsync(id);
            if (deletedOrder != null)
            {
                TempData["SuccessMessage"] = "Customer order deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting customer order.";
            }

            return RedirectToAction("Index");
        }



    }
}

