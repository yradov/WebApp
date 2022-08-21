using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WebApp.Models;


namespace Microsoft.AspNetCore.Builder
{
    public static class WebServiceEndpoint
    {
        private static string BASEURL = "api/products";

        public static void MapWebService(this IEndpointRouteBuilder app)
        {
            app.MapGet($"{BASEURL}/{{id}}", async context =>
            {
                long key = long.Parse(context.Request.RouteValues["id"] as string);

                DataContext data = context.RequestServices.GetService<DataContext>();

                Product p = data.Products.Find(key);

                if (p == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize<Product>(p));
                }

            });
        }
    }// WebServiceEndpoint
}// Microsoft.AspNetCore.Builder
