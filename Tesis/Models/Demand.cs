using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Demand
    {
        public Demand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int Quantity { get; set; }

        [DisplayName("Período")]
        public virtual Period Period { get; set; }

        public Guid PeriodId { get; set; }

        [DisplayName("Producto")]
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}