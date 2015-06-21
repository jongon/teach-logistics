using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeachLogistics.Models
{
    public class Balance
    {
        public Guid Id { get; set; }

        [DisplayName("Fecha de creación")]
        [Required]
        public DateTime Created { get; set; }

        [DisplayName("Costo Demanda Insatisfecho")]
        [Required]
        public int DissatisfiedCost { get; set; }

        [DisplayName("Demanda Insatisfecha")]
        public int DissatisfiedDemand { get; set; }

        [DisplayName("Inventario Final")]
        [Required]
        public int FinalStock { get; set; }

        [DisplayName("Costo de Inventario Final")]
        public int FinalStockCost { get; set; }

        [DisplayName("Costo Por ordenar")]
        [Required]
        public double OrderCost { get; set; }

        [DisplayName("Inventario Inicial")]
        [Required]
        public int InitialStock { get; set; }

        [DisplayName("Ordenes Recibidas")]
        [Required]
        public int ReceivedOrders { get; set; }

        [DisplayName("Demanda de la Semana")]
        [Required]
        public int Demand { get; set; }

        [DisplayName("Ventas de la Semana")]
        [Required]
        public int Sells { get; set; }

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

        [DisplayName("Grupo")]
        [Required]
        public Guid GroupId { get; set; }
    }
}