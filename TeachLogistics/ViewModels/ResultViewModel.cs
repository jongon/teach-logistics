using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TeachLogisticsTest.ViewModels
{
    public class GroupResultViewModel
    {
        [DisplayName("Periodo")]
        public int PeriodNumber { get; set; }

        [DisplayName("Grupo Id")]
        public Guid GroupId { get; set; }

        [DisplayName("Grupo")]
        public string GroupName { get; set; }

        [DisplayName("Costo Inventario Final")]
        public double FinalStockCost { get; set; }

        [DisplayName("Costo Demanda Insatisfecha")]
        public double UnsatisfiedDemandCost { get; set; }

        [DisplayName("Costo Total Ordenar")]
        public double TotalOrderCost { get; set; }
    }

    public class GroupDetailedResultViewModel
    {

    }
}