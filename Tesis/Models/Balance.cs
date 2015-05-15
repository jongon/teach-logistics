using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Balance
    {
        public Guid Id { get; set; }

        [DisplayName("Costo Insatisfecho")]
        [Required]
        public double DissatisfiedCost { get; set; }

        [DisplayName("Costo Inventario Final")]
        [Required]
        public double FinalStockCost { get; set; }

        [DisplayName("Costo Por ordenar")]
        [Required]
        public double OrderCost { get; set; }

        [DisplayName("Periodo")]
        [Required]
        public virtual Period Period { get; set; }

        [DisplayName("Periodo")]
        [Required]
        public Guid PeriodId { get; set; }

        [DisplayName("Producto")]
        [Required]
        public virtual Product Product { get; set; }

        [DisplayName("Periodo")]
        [Required]
        public Guid ProductId { get; set; }

        [DisplayName("Grupo")]
        [Required]
        public virtual Group Group { get; set; }

        [DisplayName("Periodo")]
        [Required]
        public Guid GroupId { get; set; }
    }
}