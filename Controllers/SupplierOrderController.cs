using InventoryRepo.Models;
using InventoryRepo.Models.ViewModel;
using InventoryRepo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryRepo.Controllers
{
    public class SupplierOrderController : Controller
    {
        private readonly ISupplierOrderRepo supplierOrderRepo;
        private readonly ISupplierRepo supplierRepo;
        private readonly IProductRepo productRepo;
        private readonly PdfService pdfService;
        public SupplierOrderController(ISupplierOrderRepo supplierOrderRepo,ISupplierRepo supplierRepo,IProductRepo productRepo,PdfService pdfService) { 
        
            this.supplierOrderRepo = supplierOrderRepo;
            this.supplierRepo = supplierRepo;
            this.productRepo = productRepo;
            this.pdfService = pdfService;  
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var products = await productRepo.GetAllAsync();
            var supplier = await supplierRepo.GetAllAsync();
            ViewBag.supplier = new SelectList(supplier, "SupplierId", "SupplierName");
            ViewBag.supplierName = new SelectList(supplier, "SupplierId", "ContactPerson");
            ViewBag.productList = new SelectList(products, "ProductId", "ProductName");
            ViewBag.productDetails = products.ToDictionary(p => p.ProductId, p => new { Price = p.Price, GST = p.Gstrate});
            var model = new BillViewModel
            {
                Items = new List<Item>
               {
                 new Item {   },
               }
            };
            return View(model);
            
        }
       
        [HttpPost]
        public async Task<IActionResult> Add(BillViewModel billViewModel)
        {
            var validItems = billViewModel.Items.Where(item => item.ProductId != null).ToList();
            var newItems = validItems.Select(item => new Item
            {
                
               TotalPrice = item.TotalPrice,
                Price = item.Price,
                Quantity = item.Quantity,
                ProductId = item.ProductId ,
                ProductName = supplierOrderRepo.GetProductNameById(item.ProductId)
            }).ToList();

            var supplierOrder = new Supplierorder
            {
                SupplierId = billViewModel.supplierId,
                GrandTotal = billViewModel.Grandtaotal,
                Items = newItems,
            };
            

            await supplierOrderRepo.AddAsync(supplierOrder, newItems);
            return RedirectToAction("Add");
        }
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var suppliers = await supplierOrderRepo.GetAllAsync();
			return View(suppliers);
		}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await supplierOrderRepo.GetAsync(id);

            if (supplier != null)
            {
                return View(supplier);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var deletedSupplier = await supplierOrderRepo.DeleteAsync(id);

            if (deletedSupplier != null)
            {
                // Show success notification
                TempData["SuccessMessage"] = "Supplier deleted successfully.";
            }
            else
            {
                // Show error notification
                TempData["ErrorMessage"] = "Error deleting the supplier.";
            }

            return RedirectToAction("Index", new { id });
        }
        [HttpGet]
        public IActionResult GenerateInvoice(int orderId)
        {
            
            var order = supplierOrderRepo.GetAsync(orderId).Result; 

            if (order == null)
            {
               
                return NotFound();
            }

            byte[] pdfBytes = pdfService.GenerateInvoice(order);

            
            return File(pdfBytes, "application/pdf", $"invoice_{orderId}.pdf");
        }

        public async Task<IActionResult> GetProductDetails(int productId)
            {
                try
                {
                // Fetch product details from your repository or service
                Product? product = await productRepo.GetAsync(productId);

                    // Check if the product is found
                    if (product == null)
                    {
                        return NotFound(); // Product not found
                    }

                    // Return product details as JSON
                    return Json(new { Price = product.Price, GST = product.Gstrate });
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }

    }


