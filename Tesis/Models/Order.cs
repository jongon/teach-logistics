using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        [DisplayName("Cantidad")]
        public int Quantity { get; set; }

        [DisplayName("Tipo de Orden")]
        public OrderType OrderType { get; set; }

        [DisplayName("Grupo")]
        public virtual Group Group { get; set; }

        public Guid GroupId { get; set; }

        [DisplayName("Período")]
        public virtual Period Period { get; set; }

        public Guid PeriodId { get; set; }

        [DisplayName("Producto")]
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}