using FakeStoreApi.Models;
using FakeStoreApi.Repository;
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
    public class CategoryController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        public CategoryController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromForm]Category category)
        {
            if (category != null)
            {
                var cat = await _productRepo.AddCategory(category);
                return StatusCode(StatusCodes.Status200OK);
            }
            return NotFound();
        }
    }
}
