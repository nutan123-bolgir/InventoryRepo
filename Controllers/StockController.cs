using InventoryRepo.Models;
using InventoryRepo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Controllers
{
    public class StockController : Controller
    {   
        private readonly InventoryDbContext dbcontext;
        public StockController(InventoryDbContext dbContext) {
         
            this.dbcontext = dbContext;
         
        }
        public async Task<IActionResult> Index()
        {
            var stocks = await GetAllAsync();
            return View(stocks);
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await dbcontext.Stocks.ToListAsync();
        }
    }
}
