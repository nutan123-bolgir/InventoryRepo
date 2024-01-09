using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryRepo.Models;

namespace InventoryRepo.Repository
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer?> GetAsync(int id);

        Task<Customer> AddAsync(Customer customer);

        Task<Customer?> UpdateAsync(Customer customer);

        Task<Customer?> DeleteAsync(int id);
    }
}
