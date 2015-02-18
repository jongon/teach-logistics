using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis;
using Tesis.Models;
using Tesis.ViewModels;

namespace Tesis.Business
{
    public class InitialChargeBO
    {
        public const byte WEEKS = 12;
        public InitialCharge InitialCharge { get; set; }
        public InitialChargeBO(CaseStudyViewModel initialCharge)
        {
            this.InitialCharge = new InitialCharge();
            this.InitialCharge.Id = Guid.NewGuid();
            this.InitialCharge.Demand = initialCharge.Demand;
            this.InitialCharge.Stddev = initialCharge.Stddev;
            this.InitialCharge.Price = initialCharge.Price;
            this.InitialCharge.PreparationCost = initialCharge.PreparationCost;
            this.InitialCharge.AnnualMaintenanceCost = initialCharge.AnnualMaintenanceCost;
            this.InitialCharge.PreparationTime = initialCharge.PreparationTime;
            this.InitialCharge.FillTime = initialCharge.FillTime;
            this.InitialCharge.DeliveryTime = initialCharge.DeliveryTime;           
            this.InitialCharge.SecurityStock = initialCharge.SecurityStock;
            this.InitialCharge.InitialStock = initialCharge.InitialStock;
            this.GetCalculatedInitialCharge();
        }

        //Método que realiza todos los calculos
        public void GetCalculatedInitialCharge()
        {
            this.InitialCharge.TotalTime = this.GetTotalTime();
            this.InitialCharge.ProductQuaterlyCost = this.GetProductQuaterlyCost();
            this.InitialCharge.VariationCoefficient = this.GetVariationCoefficient();
            this.InitialCharge.MaintenanceQuaterlyCost = this.GetMaintenanceQuaterlyCost();
            this.InitialCharge.EOQ = this.GetEOQ();
            this.InitialCharge.ReplacementBatch = this.GetReplacementBatch();
            this.InitialCharge.MinimunBatchReplacement = this.GetMinimumBatchReplacement();
            this.InitialCharge.RequestQuaterlyCost = this.GetRequestQuaterlyCost();
            this.InitialCharge.TotalQuaterlyCost = this.GetTotalQuaterlyCost();
            this.InitialCharge.CycleTime = this.GetCycleTime();
            this.InitialCharge.AverageStock = this.GetAvergeStock();
            this.InitialCharge.WeeklyMaintenanceCost = this.GetWeeklyMaintenanceCost();
            this.InitialCharge.PurchaseOrderRecharge = this.GetPurchaseOrderRecharge();
            this.InitialCharge.CourierCharges = this.GetCourierCharges(); 
        }
        //Aqui va un método por cada campo a calcular
        private double GetWeeklyMaintenanceCost()
        {
            return 1;
        }

        private short GetPurchaseOrderRecharge()
        {
            return 1;
        }

        private double GetCourierCharges()
        {
            return 1;
        }

        private short GetTotalTime()
        {
            return (short)(this.InitialCharge.PreparationTime + this.InitialCharge.FillTime + this.InitialCharge.DeliveryTime);
        }

        private int GetReplacementBatch()
        {
            return this.InitialCharge.Demand * this.GetTotalTime();
        }

        private double GetMinimumBatchReplacement()
        {
            return Math.Max(this.GetReplacementBatch(), this.GetEOQ());
        }

        private int GetProductQuaterlyCost()
        {
            return this.InitialCharge.Price * this.InitialCharge.Demand * WEEKS;
        }

        private double GetRequestQuaterlyCost()
        {
            return (this.InitialCharge.Demand * WEEKS) / (this.GetMinimumBatchReplacement() * this.InitialCharge.PreparationCost);
        }

        private double GetMaintenanceQuaterlyCost()
        {
            return (((this.InitialCharge.Demand * WEEKS) / 2) + this.GetVariationCoefficient()) * ((this.InitialCharge.AnnualMaintenanceCost / 100) * (1 / 4)) * this.InitialCharge.Price;
        }

        private double GetTotalQuaterlyCost()
        {
            return this.GetProductQuaterlyCost() + this.GetRequestQuaterlyCost() + this.GetMaintenanceQuaterlyCost();
        }

        private double GetVariationCoefficient()
        {
            return this.InitialCharge.Stddev / this.InitialCharge.Demand;
        }

        private double GetCycleTime()
        {
            return this.GetMinimumBatchReplacement() / this.InitialCharge.Demand;
        }

        private double GetAvergeStock()
        {
            return (this.GetMinimumBatchReplacement() / 2) + this.InitialCharge.SecurityStock;
        }

        private double GetEOQ()
        {
            return Math.Sqrt((2 * this.InitialCharge.Demand * this.InitialCharge.PreparationCost) / (this.GetWeeklyMaintenanceCost() * this.InitialCharge.Price));
        }
    }
}