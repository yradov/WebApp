using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApp.Models;
using System.Threading.Channels;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private DataContext context;

        public ProductsController(DataContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            {
                /**
                 * only for testing
                 */
                //return new Product[]
                //{
                //    new Product() {Name = "Product #1"},
                //    new Product() {Name = "Product #2"},
                //};
            }

            return context.Products;
        }

        [HttpGet("{id}")]
        //public Product GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        public async Task<Product> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            {
                /**
                 * only for testing
                 */
                //return new Product(){ProductId = 1, Name = "Test Product"};
            }

            //logger.LogDebug("GetProduct Action Invoked");
            //return context.Products.FirstOrDefault(); without model binding
            //return context.Products.Find(id);
            return await context.Products.FindAsync(id);
        }
        // Invoke-WebRequest http://localhost:5000/api/products/1000 | Select-Object StatusCode

        [HttpPost]
        public async Task<Product> SaveProduct([FromBody] ProductBindingTarget target)
        {
            await context.Products.AddAsync(target.ToProduct);
            await context.SaveChangesAsync();
            return target.ToProduct;
        }
        // Invoke-RestMethod http://localhost:5000/api/products -Method POST -Body (@{ Name="Soccer Boots"; Price=89.99; CategoryId=2; SupplierId=2 } | ConvertTo-Json) -ContentType "application/json"


        [HttpPut]
        public async Task UpdateProduct([FromBody] Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
        // Invoke-RestMethod http://localhost:5000/api/products -Method PUT -Body (@{ ProductId=1; Name="Green Kayak"; Price=275; CategoryId=21; SupplierId=1 } | ConvertTo-Json) -ContentType "application/json"


        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id)
        {
            context.Products.Remove(new Product() { ProductId = id });
            await context.SaveChangesAsync();
        }
        // Invoke-RestMethod http://localhost:5000/api/products/2 -Method DELETE
    }// ProductsController
}
