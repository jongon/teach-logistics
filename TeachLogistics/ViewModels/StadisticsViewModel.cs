using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachLogistics.ViewModels
{
    public class StadisticsViewModel
    {
        public List<PeriodResultViewModel> TotalCost { get; set; }

        public List<PeriodResultViewModel> DemandCost { get; set; }

        public List<PeriodResultViewModel> StockCost { get; set; }

        public List<PeriodResultViewModel> OrderCost { get; set; }

        public List<PeriodResultViewModel> AverageTotalCost { get; set; }

        public List<PeriodResultViewModel> AverageDemandCost { get; set; }

        public List<PeriodResultViewModel> AverageStockCost { get; set; }

        public List<PeriodResultViewModel> AverageOrderCost { get; set; }

        public List<GroupRankingViewModel> Groups { get; set; }
    }

    public class PeriodResultViewModel
    {
        public int PeriodNumber { get; set; }

        public double Quantity { get; set; }
    }
}