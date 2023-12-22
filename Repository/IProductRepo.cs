using InventoryRepo.Models;

namespace InventoryRepo.Repository
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetAsync(int id);

        Task<Product> AddAsync(Product product , IFormFile file);

        Task<Product?> UpdateAsync(Product product);

        Task<Product?> DeleteAsync(int id);
        Task<string> SaveImageAsync(IFormFile file);

    }
}
