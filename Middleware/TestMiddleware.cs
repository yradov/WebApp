using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Http;

namespace WebApp.Middleware
{
    public class TestMiddleware
    {
        private RequestDelegate nextDelegate;
        public TestMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            if (context.Request.Path == "/test")
            {
                await context.Response.WriteAsync($"There {dataContext.Products.Count()} products\n");
                await context.Response.WriteAsync($"There {dataContext.Categories.Count()} categories\n");
                await context.Response.WriteAsync($"There {dataContext.Suppliers.Count()} suppliers\n");
            }
            else
            {
                await nextDelegate(context);
            }
        }
    }
}
