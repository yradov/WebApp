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
    }
}
