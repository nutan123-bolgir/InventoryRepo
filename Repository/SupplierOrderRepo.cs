using InventoryRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Repository
{
    public class SupplierOrderRepo : ISupplierOrderRepo
    {   
        private readonly InventoryDbContext _context;
        public SupplierOrderRepo(InventoryDbContext _context) {
            this._context = _context;
        }
        public async Task<Supplierorder> AddAsync(Supplierorder supplierorder,List<Item>newItems)
        {

            await _context.Supplierorders.AddAsync(supplierorder);
            int generatedSupplierorderId = supplierorder.SupplierorderId;
            await _context.Items.AddRangeAsync(newItems);
            await _context.SaveChangesAsync();
            return supplierorder;

        }

        public async Task<Supplierorder?> DeleteAsync(int id)
        {
            var existingOrder = await _context.Supplierorders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(p => p.SupplierorderId == id);

            if (existingOrder != null)
            {
                // Delete related records in the Items table
                _context.Items.RemoveRange(existingOrder.Items);

                // Delete the Supplierorder
                _context.Supplierorders.Remove(existingOrder);

                await _context.SaveChangesAsync();
                return existingOrder;
            }

            return null;
        }


        public async Task<IEnumerable<Supplierorder>> GetAllAsync()
        {
			return await _context.Supplierorders.Include(so => so.Items).Include(s => s.Supplier).ToListAsync();
		}

        public async Task<Supplierorder?> GetAsync(int id)
        {
            var existingTag = await _context.Supplierorders.Include(so => so.Items).Include(s => s.Supplier).FirstOrDefaultAsync(p => p.SupplierorderId == id);
            return existingTag;
        }

        public Task<Supplierorder?> UpdateAsync(Supplierorder supplierorder)
        {
            throw new NotImplementedException();
        }
        public string GetProductNameById(int? productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            return product != null ? product.ProductName : string.Empty;
        }

    }
}
