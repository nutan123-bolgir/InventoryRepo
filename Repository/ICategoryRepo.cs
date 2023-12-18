using InventoryRepo.Models;

namespace InventoryRepo.Repository
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetAsync(int id);

        Task<Category> AddAsync(Category Category);

        Task<Category?> UpdateAsync(Category Category);

        Task<Category?> DeleteAsync(int id);
    }
}
