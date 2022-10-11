using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreApi.ApiModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Categories { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
