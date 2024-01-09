using InventoryRepo.Models;
using InventoryRepo.Models.ViewModel;

namespace InventoryRepo.Repository
{
    public interface ISupplierOrderRepo
    {
        Task<IEnumerable<Supplierorder>> GetAllAsync();

        Task<Supplierorder?> GetAsync(int id);

        Task<Supplierorder> AddAsync(Supplierorder supplierorder,List<Item> newItem);

        Task<Supplierorder?> UpdateAsync(Supplierorder supplierorder);

        Task<Supplierorder?> DeleteAsync(int id);

        public string GetProductNameById(int? productId);
    }
}
