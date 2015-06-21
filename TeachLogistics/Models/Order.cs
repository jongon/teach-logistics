using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeachLogistics.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        [DisplayName("Cantidad")]
        [Required]
        public int Quantity { get; set; }

        [DisplayName("Fecha de Creación")]
        [Required]
        public DateTime Created { get; set; }

        [DisplayName("Tipo de Orden")]
        [Required]
        public OrderType OrderType { get; set; }

        [DisplayName("Grupo")]
        [Required]
        public virtual Group Group { get; set; }

        public Guid GroupId { get; set; }

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