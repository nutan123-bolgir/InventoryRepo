namespace InventoryRepo.Models.ViewModel
{
    public class OrderedProductViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal GSTRate { get; set; }
        public string ProductName { get; set; }
        public decimal Total { get; set; }
    }
}
