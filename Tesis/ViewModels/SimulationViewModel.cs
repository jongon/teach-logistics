using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class SellViewModel {

        public Guid CaseStudyId { get; set; }

        public virtual Section Section { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductSell> ProductSells { get; set; }
    }

    public class ProductSell
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}