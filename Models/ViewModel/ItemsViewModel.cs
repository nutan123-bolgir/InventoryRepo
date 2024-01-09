namespace InventoryRepo.Models.ViewModel
{
    public class ItemsViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal GstRate { get; set; }
        public decimal Total { get; set; }
    }
}
