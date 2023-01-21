using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : Controller
    {
        private DataContext context;

        public SuppliersController(DataContext ctx)
        {
            context = ctx;
        }

        [HttpGet("{id}")]
        public async Task<Supplier> GetSupplier(long id)
        {
            //return await context.Suppliers.FindAsync(id);

            /** JsonException: циклическая ссылка */
            //return await context.Suppliers
            //    .Include(s => s.Products)
            //    .FirstAsync(s => s.SupplierId == id);

            Supplier supplier = await context.Suppliers
                .Include(s => s.Products)
                .FirstAsync(s => s.SupplierId == id);

            foreach (Product product in supplier.Products)
            {
                product.Supplier = null;
            }

            return supplier;
        }

        [HttpPatch("{id}")]
        public async Task<Supplier> PatchSupplier(long id, JsonPatchDocument<Supplier> patchDoc)
        {
            Supplier s = await context.Suppliers.FindAsync(id);
            if (s != null)
            {
                patchDoc.ApplyTo(s);
                await context.SaveChangesAsync();
            }
            return s;
        }
        // Invoke-RestMethod http://localhost:5000/api/suppliers/1
        // -Method PATCH -ContentType "application/json"
        // -Body '[{"op":"replace","path":"City","value":"Los Angeles"}]'
    }
}
