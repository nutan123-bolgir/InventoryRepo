using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryRepo.Models;

public interface IItemRepo
{
    Task<IEnumerable<Item>> GetAllAsync();
    Task<Item?> GetAsync(int id);
    Task<Item> AddAsync(Item item);
    Task<Item?> UpdateAsync(Item item);
    Task<Item?> DeleteAsync(int id);
}