using FakeStoreApi.DataContext;
using FakeStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreApi.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var productCategory = await GetCategoryById(product.Id);
            product.Categories = productCategory.Name;
            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _dbContext.Products.ToListAsync();

            return products;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (result != null)
            {
                result.Title = product.Title;
                result.Description = product.Description;
                result.Price = product.Price;
                result.Categories = product.Categories;
                result.CategoryId = product.CategoryId;
                result.ImageUrl = product.ImageUrl;
                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Category> AddCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }
    }
}
