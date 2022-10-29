using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controler]")]
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
            return await context.Suppliers.FindAsync(id);
        }
    }
}
