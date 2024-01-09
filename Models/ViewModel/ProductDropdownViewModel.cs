namespace InventoryRepo.Models.ViewModel
{
    public class ProductDropdownViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal GSTRate { get; set; }
    }
}
