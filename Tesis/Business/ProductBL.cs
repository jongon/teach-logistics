using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis.Models;

namespace Tesis.Business
{
    public class ProductBL
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product> {
                new Product { Id = Guid.NewGuid(), City = "Caracas", Distance = 300, Name = "Margarina" },
                new Product { Id = Guid.NewGuid(), City = "Maracay", Distance = 400, Name = "Leche" },
                new Product { Id = Guid.NewGuid(), City = "Margarita", Distance = 500, Name = "Carne" }
            };
            return products;
        }
    }
}