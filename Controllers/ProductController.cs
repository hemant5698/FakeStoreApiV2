using FakeStoreApi.Helper;
using FakeStoreApi.Models;
using FakeStoreApi.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepo productRepo,
            IWebHostEnvironment webHostEnvironment)
        {
            _productRepo = productRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var model = await _productRepo.GetAllProducts();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployees([FromForm] Product product)
        {
            if (product != null)
            {
                try
                {
                    if (product.ImageFile != null)
                    {
                        var filePath = await FileUploader.FileUpload(product.ImageFile, _webHostEnvironment);

                        product.ImageUrl = filePath;
                    }
                }
                catch (Exception ex)
                {
                    return Content(ex.Message.ToString());
                }
                await _productRepo.AddProduct(product);
                return StatusCode(StatusCodes.Status200OK);
            }
            return NotFound();
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<ActionResult> GetProductsById([FromRoute]int id)
        {
            if(id > 0)
            {
                var model = await _productRepo.GetProductById(id);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct([FromForm]Product product,[FromRoute]int id)
        {
            await _productRepo.UpdateProduct(id, product);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            if(id > 0)
            {
                await _productRepo.DeleteProduct(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
