using InventoryRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly InventoryDbContext _dbContext;

        public CustomerRepo(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetAsync(int customerId)
        {
            return await _dbContext.Customers.FindAsync(customerId);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            var existingCustomer = await _dbContext.Customers.FindAsync(customer.CustomerId);

            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;

                await _dbContext.SaveChangesAsync();

                return existingCustomer;
            }

            return null;
        }

        public async Task<Customer?> DeleteAsync(int customerId)
        {
            var existingCustomer = await _dbContext.Customers.FindAsync(customerId);

            if (existingCustomer != null)
            {
                _dbContext.Customers.Remove(existingCustomer);
                await _dbContext.SaveChangesAsync();
            }

            return existingCustomer;
        }
    }
}
