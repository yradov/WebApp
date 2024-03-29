﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApp.Models;
using System.Threading.Channels;

namespace WebApp.Controllers
{
    [ApiController]
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
        //public async Task<Product> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        public async Task<IActionResult> GetProduct(long id)
        {
            {
                /**
                 * only for testing
                 */
                //return new Product(){ProductId = 1, Name = "Test Product"};
            }
            {
                //logger.LogDebug("GetProduct Action Invoked");
                //return context.Products.FirstOrDefault(); without model binding
                //return context.Products.Find(id);
                //return await context.Products.FindAsync(id);

            }
            Product p = await context.Products.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            //return Ok(p);
            // remove fields with null
            return Ok(new { 
                ProductId = p.ProductId, 
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId
            });
        }
        // Invoke-WebRequest http://localhost:5000/api/products/1000 | Select-Object StatusCode

        [HttpPost]
        //public async Task<Product> SaveProduct([FromBody] ProductBindingTarget target)
        //public async Task<IActionResult> SaveProduct([FromBody] ProductBindingTarget target)
        public async Task<IActionResult> SaveProduct(ProductBindingTarget target)
        {
            {
                // when return Task<Product>:
                //await context.Products.AddAsync(target.ToProduct());
                //await context.SaveChangesAsync();
                //return target.ToProduct();
            }
            //if (ModelState.IsValid)
            //{
                Product p = target.ToProduct();
                await context.Products.AddAsync(p);
                await context.SaveChangesAsync();
                return Ok(p);
            //}
            //return BadRequest(ModelState);
        }
        // Invoke-RestMethod http://localhost:5000/api/products -Method POST -Body (@{ Name="Soccer Boots"; Price=89.99; CategoryId=2; SupplierId=2 } | ConvertTo-Json) -ContentType "application/json"
        // Invoke-RestMethod http://localhost:5000/api/products -Method POST -Body (@{ Name="Boot Laces"; Price=19.99; CategoryId=2; SupplierId=2 } | ConvertTo-Json) -ContentType "application/json"
        // check ModelState.IsValid
        // Invoke-WebRequest http://localhost:5000/api/products -Method POST -Body (@{ Name="Boot Laces" } | ConvertTo-Json) -ContentType "application/json"


        [HttpPut]
        //public async Task UpdateProduct([FromBody] Product product)
        public async Task UpdateProduct(Product product)
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
    
        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            return Redirect("/api/products/1");
        }
        // Invoke-RestMethod http://localhost:5000/api/products/redirect

        [HttpGet("redirect2")]
        public IActionResult Redirect2()
        {
            return RedirectToRoute(new { controller = "Products", action = "GetProduct", Id = 1});
        }
        // Invoke-RestMethod http://localhost:5000/api/products/redirect2
    }// ProductsController
}
