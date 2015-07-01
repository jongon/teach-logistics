using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TeachLogistics.ViewModels
{
    public class StadisticsViewModel
    {
        [DisplayName("Costo Total")]
        public List<PeriodResultViewModel> TotalCost { get; set; }

        [DisplayName("Costo por Demanda insatisfecha")]
        public List<PeriodResultViewModel> DemandCost { get; set; }

        [DisplayName("Costo por inventario")]
        public List<PeriodResultViewModel> StockCost { get; set; }

        [DisplayName("Costo por ordenar")]
        public List<PeriodResultViewModel> OrderCost { get; set; }

        [DisplayName("Promedio Costo total")]
        public List<PeriodResultViewModel> AverageTotalCost { get; set; }

        [DisplayName("Promedio por demanda insatisfecha")]
        public List<PeriodResultViewModel> AverageDemandCost { get; set; }

        [DisplayName("Promerio por costo de inventario")]
        public List<PeriodResultViewModel> AverageStockCost { get; set; }

        [DisplayName("Promerio de costo por ordenar")]
        public List<PeriodResultViewModel> AverageOrderCost { get; set; }

        [DisplayName("Grupos")]
        public List<GroupRankingViewModel> Groups { get; set; }
    }

    public class GroupStadistics
    {
        [DisplayName("Grupo Id")]
        public Guid GroupId { get; set; }

        [DisplayName("Grupo")]
        public string GroupName { get; set; }
    }

    public class SectionStadistics
    {
        [DisplayName("Semestre")]
        public string Semester { get; set; }

        [DisplayName("Sección")]
        public string Section { get; set; }

        [DisplayName("Sección Id")]
        public Guid SectionId { get; set; }
    }

    public class PeriodResultViewModel
    {
        [DisplayName("Período")]
        public int PeriodNumber { get; set; }

        [DisplayName("Cantidad")]
        public double Quantity { get; set; }
    }
}