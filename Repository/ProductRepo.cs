using InventoryRepo.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InventoryRepo.Repository
{
    public class ProductRepo : IProductRepo
    {

        private readonly InventoryDbContext _dbContext;
        public ProductRepo(InventoryDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<Product> AddAsync(Product product, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
             product.ProductImage = await SaveImageAsync(file);
            }

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            _dbContext.Database.ExecuteSqlRaw("EXEC UpdateStock @ProductID,@ProductName,@StockQuantity, @OperationType",
                new SqlParameter("@ProductID", product.ProductId),
                new SqlParameter("@StockQuantity", product.StockQuantity),
                new SqlParameter("@ProductName", product.@ProductName),
                new SqlParameter("@OperationType", "Insert"));
            return product;

        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var existingTag = await _dbContext.Products.FindAsync(id);
            

            if (existingTag != null)
            {
                _dbContext.Products.Remove(existingTag);
                await _dbContext.SaveChangesAsync();

                _dbContext.Database.ExecuteSqlRaw("EXEC UpdateStock @ProductID,@ProductName, @StockQuantity, @OperationType",
          new SqlParameter("@ProductID", existingTag.ProductId),
          new SqlParameter("@StockQuantity", existingTag.StockQuantity),
          new SqlParameter("@ProductName", existingTag.ProductName),
          new SqlParameter("@OperationType", "Delete"));
                return existingTag;
            }
          
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
           
                return await _dbContext.Products.ToListAsync();
            
        }

        public async Task<Product?> GetAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var existingProduct = await _dbContext.Products.FindAsync(product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.ProductId = product.ProductId;
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Category = product.Category;
                existingProduct.StockQuantity = product.StockQuantity;

                await _dbContext.SaveChangesAsync();
                _dbContext.Database.ExecuteSqlRaw("EXEC UpdateStock @ProductID, @ProductName, @StockQuantity, @OperationType",
    new SqlParameter("@ProductID", product.ProductId),
    new SqlParameter("@ProductName", product.ProductName), 
    new SqlParameter("@StockQuantity", product.StockQuantity),
    new SqlParameter("@OperationType", "Update"));

                return existingProduct;
            }
            
        
            return null;
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            string FileName = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                FileName = DateTime.Now.Ticks.ToString() + extension;
                var rootPath = "C:\\Users\\Nutan.Bolgir\\Desktop\\Inventory\\InventoryRepo\\wwwroot";
                var relativePath = "Upload/files";
                var filepath = Path.Combine(rootPath, relativePath);
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                var exactpath = Path.Combine(filepath, FileName);
                using (var strem = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(strem);
                }

            }
            catch (Exception ex)
            {
            }

            return FileName;


        }        
                
    }


       
}

