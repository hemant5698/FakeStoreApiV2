using FakeStoreApi.ApiModels;
using FakeStoreApi.DataContext;
using FakeStoreApi.Helper;
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

        public async Task<ProductModel> GetProductById(int id)
        {
            var product = await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null)
                return null;
            var model = new ProductModel()
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Categories = product.CategoryId.HasValue ? GetCategoryById(product.CategoryId.Value).Name: string.Empty,
                ImageUrl = product.ImageUrl,
            };
            return model;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _dbContext.Products.ToListAsync();

            return products;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
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

        public Category GetCategoryById(int id)
        {
            if(id > 0)
            {
                var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
                return category;
            }
            return null;
        }
    }
}
