namespace InventoryRepo.Models.ViewModel
{
    public class BillViewModel
    {
        public int supplierId {  get; set; }
        public string supplierName {  get; set; }
        public List<Item> Items { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public decimal GSTRATE {  get; set; }
        public int Discount { get; set; }
        public int Grandtaotal {  get; set; }
        public int TotalPrice {  get; set; }

    }
}
