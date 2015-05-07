using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Sale
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public Period Period { get; set; }

        public Guid PeriodId { get; set; }

        public Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}