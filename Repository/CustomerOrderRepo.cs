using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Repository
{
    public class CustomerOrderRepo : ICustomerOrderRepo
    {
        private readonly InventoryDbContext _dbContext;

        public CustomerOrderRepo(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CustomerOrder>> GetAllAsync()
        {
            var customerOrders = await _dbContext.CustomerOrders
                .Include(co => co.Customer)
                .Include(co => co.Product)
                .Include(co => co.OrderedProducts)
                .ToListAsync();

            // Check for null values in GrandTotal property
            foreach (var order in customerOrders)
            {
                if (order.GrandTotal == null)
                {
                    // Handle the case when GrandTotal is null
                }
            }

            return customerOrders;
        }


        public async Task<CustomerOrder?> GetAsync(int id)
        {
            return await _dbContext.CustomerOrders
                .Include(co => co.Customer)
                .Include(co => co.Product)
                .FirstOrDefaultAsync(co => co.CustomerOrderId == id);
        }

        public async Task<CustomerOrder> AddAsync(CustomerOrder customerOrder)
        {
            _dbContext.CustomerOrders.Add(customerOrder);
            await _dbContext.SaveChangesAsync();
            return customerOrder;
        }

        public async Task<CustomerOrder?> UpdateAsync(CustomerOrder customerOrder)
        {
            var existingOrder = await _dbContext.CustomerOrders.FindAsync(customerOrder.CustomerOrderId);

            if (existingOrder != null)
            {
                _dbContext.Entry(existingOrder).CurrentValues.SetValues(customerOrder);
                await _dbContext.SaveChangesAsync();
                return existingOrder;
            }

            return null;
        }

        public async Task<CustomerOrder?> DeleteAsync(int id)
        {
            var existingOrder = await _dbContext.CustomerOrders.FindAsync(id);

            if (existingOrder != null)
            {
                _dbContext.CustomerOrders.Remove(existingOrder);
                await _dbContext.SaveChangesAsync();
                return existingOrder;
            }

            return null;
        }

        public async Task<CustomerOrder> AddOrderWithDetailsAsync(CustomerOrder customerOrder, List<OrderedProduct> orderedProducts)
        {
            // Add customer order to the context
            _dbContext.CustomerOrders.Add(customerOrder);

            // Associate ordered products with the customer order
            foreach (var originalOrderedProduct in orderedProducts)
            {
                // Check if the orderedProduct is already being tracked
                var existingOrderedProduct = _dbContext.ChangeTracker.Entries<OrderedProduct>()
                    .FirstOrDefault(e => e.Entity.OrderedProductId == originalOrderedProduct.OrderedProductId);

                OrderedProduct updatedOrderedProduct;

                if (existingOrderedProduct != null)
                {
                    // Use the existing tracked entity
                    updatedOrderedProduct = existingOrderedProduct.Entity;
                }
                else
                {
                    // Attach the detachedOrderedProduct to the context
                    updatedOrderedProduct = _dbContext.Entry(originalOrderedProduct).Entity;
                    _dbContext.Entry(updatedOrderedProduct).State = EntityState.Added;
                }

                // Set the navigation property
                updatedOrderedProduct.CustomerOrder = customerOrder;

                // Add the orderedProduct if not already tracked
                if (existingOrderedProduct == null)
                {
                    _dbContext.OrderedProducts.Add(updatedOrderedProduct);
                }
            }

            await _dbContext.SaveChangesAsync();

            return customerOrder;
        }






    }
}
