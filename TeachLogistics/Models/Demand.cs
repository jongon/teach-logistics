using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeachLogisticsTest.Models
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
        [Required]
        public virtual Period Period { get; set; }

        public Guid PeriodId { get; set; }

        [DisplayName("Producto")]
        [Required]
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}