using InventoryRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Repository
{
    public class ItemRepo : IItemRepo
    {
        private readonly InventoryDbContext _dbContext;

        public ItemRepo(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<Item?> GetAsync(int id)
        {
            return await _dbContext.Items.FindAsync(id);
        }

        public async Task<Item> AddAsync(Item item)
        {
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item?> UpdateAsync(Item item)
        {
            var existingItem = await _dbContext.Items.FindAsync(item.Id);

            if (existingItem != null)
            {
                _dbContext.Entry(existingItem).CurrentValues.SetValues(item);
                await _dbContext.SaveChangesAsync();
                return existingItem;
            }

            return null;
        }

        public async Task<Item?> DeleteAsync(int id)
        {
            var existingItem = await _dbContext.Items.FindAsync(id);

            if (existingItem != null)
            {
                _dbContext.Items.Remove(existingItem);
                await _dbContext.SaveChangesAsync();
                return existingItem;
            }

            return null;
        }

    }
}
