using FakeStoreApi.ApiModels;
using FakeStoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreApi.Repository
{
    public interface IProductRepo
    {
        Task<List<Product>> GetAllProducts();
        Task<ProductModel> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);

        //add Category
        Task<Category> AddCategory(Category category);
    }
}
