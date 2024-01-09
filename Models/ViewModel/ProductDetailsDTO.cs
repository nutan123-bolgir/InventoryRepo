namespace InventoryRepo.Models.ViewModel
{
    public class ProductDetailsDTO
    {
         public decimal? UnitPrice { get; set; }
         public decimal? GSTRate { get; set; }

    public static ProductDetailsDTO FromProduct(Product product)
    {
        if (product != null)
        {
            return new ProductDetailsDTO
            {
                UnitPrice = product.Price,
                GSTRate = product.Gstrate
            };
        }
        else
        {
            return new ProductDetailsDTO
            {
                UnitPrice = 0.00m,
                GSTRate = 0.00m
            };
        }
    }
    }
}
