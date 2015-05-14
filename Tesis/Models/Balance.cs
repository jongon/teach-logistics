using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Balance
    {
        public Guid Id { get; set; }

        [DisplayName("Periodo")]
        public virtual Period Period { get; set; }

        public Guid PeriodId { get; set; }

        [DisplayName("Producto")]
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }

        [DisplayName("Grupo")]
        public virtual Group Group { get; set; }

        public Guid GroupId { get; set; }
    }
}