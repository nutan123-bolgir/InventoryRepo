using InventoryRepo.Models;

namespace InventoryRepo.Repository
{
    public interface ISupplierRepo
    {
        Task<IEnumerable<Supplier>> GetAllAsync();

        Task<Supplier?> GetAsync(int id);

        Task<Supplier> AddAsync(Supplier supplier, IFormFile file);

        Task<Supplier?> UpdateAsync(Supplier supplier);

        Task<Supplier?> DeleteAsync(int id);
       
    }
}
