namespace TeachLogistics.Business
{
    public class InitialChargeBL
    {
        //public const byte WEEKS = 12;
        //public InitialCharge InitialCharge { get; set; }
        //public InitialChargeBL(InitialCharge initialCharge)
        //{
        //    this.InitialCharge = new InitialCharge();
        //    this.InitialCharge.ProductId = initialCharge.ProductId;
        //    this.InitialCharge.Id = Guid.NewGuid();
        //    this.InitialCharge.Demand = initialCharge.Demand;
        //    this.InitialCharge.Stddev = initialCharge.Stddev;
        //    this.InitialCharge.Price = initialCharge.Price;
        //    this.InitialCharge.PreparationCost = initialCharge.PreparationCost;
        //    this.InitialCharge.AnnualMaintenanceCost = initialCharge.AnnualMaintenanceCost;
        //    this.InitialCharge.PreparationTime = initialCharge.PreparationTime;
        //    this.InitialCharge.FillTime = initialCharge.FillTime;
        //    this.InitialCharge.DeliveryTime = initialCharge.DeliveryTime;           
        //    this.InitialCharge.SecurityStock = initialCharge.SecurityStock;
        //    this.InitialCharge.InitialStock = initialCharge.InitialStock;
        //    this.InitialCharge.PurchaseOrderRecharge = initialCharge.PurchaseOrderRecharge;
        //    this.InitialCharge.CourierCharges = initialCharge.CourierCharges;
        //    this.GetCalculatedInitialCharge();
        //}

        ////Método que realiza todos los calculos
        //public void GetCalculatedInitialCharge()
        //{
        //    this.InitialCharge.TotalTime = this.GetTotalTime();
        //    this.InitialCharge.ProductQuaterlyCost = this.GetProductQuaterlyCost();
        //    this.InitialCharge.VariationCoefficient = this.GetVariationCoefficient();
        //    this.InitialCharge.MaintenanceQuaterlyCost = this.GetMaintenanceQuaterlyCost();
        //    this.InitialCharge.EOQ = this.GetEOQ();
        //    this.InitialCharge.ReplacementBatch = this.GetReplacementBatch();
        //    this.InitialCharge.MinimunBatchReplacement = this.GetMinimumBatchReplacement();
        //    this.InitialCharge.RequestQuaterlyCost = this.GetRequestQuaterlyCost();
        //    this.InitialCharge.TotalQuaterlyCost = this.GetTotalQuaterlyCost();
        //    this.InitialCharge.CycleTime = this.GetCycleTime();
        //    this.InitialCharge.AverageStock = this.GetAvergeStock();
        //}

        //private short GetTotalTime()
        //{
        //    return (short)(this.InitialCharge.PreparationTime + this.InitialCharge.FillTime + this.InitialCharge.DeliveryTime + 1);
        //}

        //private double GetReplacementBatch()
        //{
        //    return Math.Max(this.GetMinimumBatchReplacement(), this.GetEOQ());
        //}

        //private int GetMinimumBatchReplacement()
        //{
        //    return this.InitialCharge.Demand * this.GetTotalTime();
        //}

        //private int GetProductQuaterlyCost()
        //{
        //    return this.InitialCharge.Price * this.InitialCharge.Demand * WEEKS;
        //}

        //private double GetRequestQuaterlyCost()
        //{
        //    return ((double)(this.InitialCharge.Demand * WEEKS) / this.GetReplacementBatch()) * 100;
        //}

        //private double GetMaintenanceQuaterlyCost()
        //{
        //    return (((double)(this.InitialCharge.Demand * WEEKS) / 2) + this.InitialCharge.SecurityStock) * (((double)this.InitialCharge.AnnualMaintenanceCost) * ((double)1 / 4)) * this.InitialCharge.Price;
        //}

        //private double GetTotalQuaterlyCost()
        //{
        //    return this.GetProductQuaterlyCost() + this.GetRequestQuaterlyCost() + this.GetMaintenanceQuaterlyCost();
        //}

        //private double GetVariationCoefficient()
        //{
        //    return ((double)this.InitialCharge.Stddev / this.InitialCharge.Demand);
        //}

        //private double GetCycleTime()
        //{
        //    return (double)this.GetReplacementBatch() / this.InitialCharge.Demand;
        //}

        //private double GetAvergeStock()
        //{
        //    return ((double)this.GetReplacementBatch() / 2) + this.InitialCharge.SecurityStock;
        //}

        //private double GetEOQ()
        //{
        //    return Math.Sqrt((double)(2 * this.InitialCharge.Demand * this.InitialCharge.PreparationCost) / (((double)this.InitialCharge.AnnualMaintenanceCost / 52) * this.InitialCharge.Price));
        //}
    }
}