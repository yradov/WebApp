using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApp.Models;

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
        public IEnumerable<Product> GetProducts()
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
        public Product GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            {
                /**
                 * only for testing
                 */
                //return new Product(){ProductId = 1, Name = "Test Product"};
            }

            logger.LogDebug("GetProduct Action Invoked");
            //return context.Products.FirstOrDefault(); without model binding
            return context.Products.Find(id);
        }

        public Product SaveProduct([FromBody] Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }
    }// ProductsController
}
