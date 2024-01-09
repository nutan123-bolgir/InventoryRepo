using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryRepo.Models;

namespace InventoryRepo.Repository
{
    public interface ICustomerOrderRepo
    {
        Task<IEnumerable<CustomerOrder>> GetAllAsync();
        Task<CustomerOrder?> GetAsync(int id);
        Task<CustomerOrder> AddAsync(CustomerOrder customerOrder);
        Task<CustomerOrder?> UpdateAsync(CustomerOrder customerOrder);
        Task<CustomerOrder?> DeleteAsync(int id);
        Task<CustomerOrder?> AddOrderWithDetailsAsync(CustomerOrder customerOrder, List<OrderedProduct> orderedProducts);


    }
}
