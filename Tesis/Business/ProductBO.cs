using System;
using Tesis.Models;
using Tesis.ViewModels;

namespace Tesis.Business
{
    public class ProductBO
    {
        public Product Product { get; set; }

        public ProductBO (ProductViewModel product) 
        {
            this.Product = new Product();
            this.Product.Id = Guid.NewGuid();
            this.Product.Number = product.Number;
            this.Product.Name = product.Name;
            this.Product.City = product.City;
            this.Product.Distance = product.Distance;
        }
    }
}