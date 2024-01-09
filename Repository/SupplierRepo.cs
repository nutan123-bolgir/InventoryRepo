using InventoryRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Repository
{
    public class SupplierRepo : ISupplierRepo
    {
        private readonly InventoryDbContext _dbContext;
        private readonly IProductRepo _productRepo;
        public SupplierRepo(InventoryDbContext _dbContext,IProductRepo _productRepo)
        {
            this._dbContext = _dbContext;
            this._productRepo = _productRepo;
        }
        public async Task<Supplier> AddAsync(Supplier supplier, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
               supplier.SupplierPhoto = await _productRepo.SaveImageAsync(file);
            }

            await _dbContext.Suppliers.AddAsync(supplier);
            await _dbContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier?> DeleteAsync(int id)
        {
            var existingTag = await _dbContext.Suppliers.Include(p => p.Product).FirstOrDefaultAsync(p => p.SupplierId == id);

            if (existingTag != null)
            {
                _dbContext.Suppliers.Remove(existingTag);
                await _dbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _dbContext.Suppliers.Include(s => s.Product).ToListAsync();

        }

        public async Task<Supplier?> GetAsync(int id)
        {
            return await _dbContext.Suppliers.Include(p => p.Product).FirstOrDefaultAsync(p => p.SupplierId == id);
        }

        public async Task<Supplier?> UpdateAsync(Supplier supplier)
        {
            var existingProduct = await _dbContext.Suppliers.Include(p => p.Product).FirstOrDefaultAsync(p => p.SupplierId == supplier.SupplierId);

            if (existingProduct != null)
            {
                existingProduct.SupplierId=supplier.SupplierId;
                existingProduct.SupplierName=supplier.SupplierName;
                existingProduct.ContactNumber=supplier.ContactNumber;
                existingProduct.ContactPerson=supplier.ContactPerson;
                existingProduct.Email=supplier.Email;
                existingProduct.IsActive=supplier.IsActive;
                existingProduct.ProductId=supplier.ProductId;



                await _dbContext.SaveChangesAsync();

                return existingProduct;
            }

            return null;
        }
    }
}
