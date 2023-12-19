using Azure;
using InventoryRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Repository
{
    public class CategoryRepo : ICategoryRepo
    {  
        private readonly InventoryDbContext _dbContext;
        public CategoryRepo(InventoryDbContext _dbContext) { 
           this._dbContext = _dbContext;
        }
        public async Task<Category> AddAsync(Category Category)
        {
            await _dbContext.Categories.AddAsync(Category);
            await _dbContext.SaveChangesAsync();
            return Category;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var existingTag = await _dbContext.Categories.FindAsync(id) ;

            if (existingTag != null)
            {
               _dbContext.Categories.Remove(existingTag);
                await _dbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetAsync(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task<Category?> UpdateAsync(Category Category)
        {
            var existingTag = await _dbContext.Categories.FindAsync(Category.CategoryId) ;

            if (existingTag != null)
            {
                existingTag.CategoryName = Category.CategoryName;
                existingTag.Description = Category.Description;

                await _dbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
