using InventoryRepo.Models;
using InventoryRepo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryRepo.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepo.GetAllAsync();
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            await _customerRepo.AddAsync(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepo.GetAsync(id);
            if (customer != null)
            {
                return View(customer);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            var updatedCustomer = await _customerRepo.UpdateAsync(customer);

            if (updatedCustomer != null)
            {
                // Success message
            }
            else
            {
                // Not success message
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepo.GetAsync(id);

            if (customer != null)
            {
                return View(customer);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var deletedCustomer = await _customerRepo.DeleteAsync(id);

            if (deletedCustomer != null)
            {
                // Show success notification
                TempData["SuccessMessage"] = "Customer deleted successfully.";
            }
            else
            {
                // Show error notification
                TempData["ErrorMessage"] = "Error deleting the customer.";
            }

            return RedirectToAction("Index");
        }
    }
}
