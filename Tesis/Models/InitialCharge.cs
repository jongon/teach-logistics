namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DisplayName("Carga Inicial")]
    public partial class InitialCharge
    {
        public Guid Id { get; set; }

        [DisplayName("Demanda")]
        public short Demand { get; set; }

        [DisplayName("Desviación estandar")]
        public short Stddev { get; set; }

        [DisplayName("Precio")]
        public int Price { get; set; }

        [DisplayName("Costo de preparación")]
        public int PreparationCost { get; set; }

        [DisplayName("Costo anual mantener")]
        public double AnnualMaintenanceCost { get; set; }

        [DisplayName("Costo semanal mantener")]
        public double WeeklyMaintenanceCost { get; set; }

        [DisplayName("Recargo por orden de compra")]
        public short PurchaseOrderRecharge { get; set; }

        [DisplayName("Cargos de Courier")]
        public double CourierCharges { get; set; }

        [DisplayName("Tiempo de preparación")]
        public byte PreparationTime { get; set; }

        [DisplayName("Tiempo de surtir")]
        public byte FillTime { get; set; }

        [DisplayName("Tiempo de entrega")]
        public byte DeliveryTime { get; set; }

        [DisplayName("Tiempo total")]
        public short TotalTime { get; set; }

        [DisplayName("Lote de reposición")]
        public int ReplacementBatch { get; set; }

        [DisplayName("Lote de reposición mínimo")]
        public double MinimunBatchReplacement { get; set; }

        [DisplayName("Costo trimestral de producto")]
        public int ProductQuaterlyCost { get; set; }

        [DisplayName("Costo ")]
        public double RequestQuaterlyCost { get; set; }

        [DisplayName("Coso trimestral mantener")]
        public double MaintenanceQuaterlyCost { get; set; }

        [DisplayName("Costo trimestral total")]
        public double TotalQuaterlyCost { get; set; }

        [DisplayName("Inventario de seguridad")]
        public short SecurityStock { get; set; }

        [DisplayName("Coeficiente de variación")]
        public double VariationCoefficient { get; set; }

        [DisplayName("Tiempo del ciclo")]
        public double CycleTime { get; set; }

        [DisplayName("Inventario promedio")]
        public double AverageStock { get; set; }

        [DisplayName("EOQ")]
        public double EOQ { get; set; }

        [DisplayName("Inventario Inicial")]
        public int InitialStock { get; set; }

        [DisplayName("Caso de Estudio")]
        public virtual CaseStudy CaseStudy { get; set; }

        [DisplayName("Producto")]
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }

        public Guid CaseStudyId { get; set; }

    }
}
