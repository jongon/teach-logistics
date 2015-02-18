namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InitialCharge
    {
        public Guid Id { get; set; }

        public short Demand { get; set; }

        public short Stddev { get; set; }

        public int Price { get; set; }

        public int PreparationCost { get; set; }

        public double AnnualMaintenanceCost { get; set; }

        public double WeeklyMaintenanceCost { get; set; }

        public short PurchaseOrderRecharge { get; set; }

        public double CourierCharges { get; set; }

        public byte PreparationTime { get; set; }

        public byte FillTime { get; set; }

        public byte DeliveryTime { get; set; }

        public short TotalTime { get; set; }

        public int ReplacementBatch { get; set; }

        public double MinimunBatchReplacement { get; set; }

        public int ProductQuaterlyCost { get; set; }

        public double RequestQuaterlyCost { get; set; }

        public double MaintenanceQuaterlyCost { get; set; }

        public double TotalQuaterlyCost { get; set; }

        public short SecurityStock { get; set; }

        public double VariationCoefficient { get; set; }

        public double CycleTime { get; set; }

        public double AverageStock { get; set; }

        public double EOQ { get; set; }

        public int InitialStock { get; set; }

        public Guid ProductId { get; set; }

        public Guid CaseStudyId { get; set; }

        public virtual CaseStudy CaseStudy { get; set; }

        public virtual Product Product { get; set; }
    }
}
