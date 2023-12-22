using InventoryRepo.Models;

namespace InventoryRepo.ViewModel
{
    public class StockViewModel
    {
        public int StockId { get; set; }
        public int? QuantityInStock { get; set; }
        public List<Product> Products { get; set; }
    }
}
