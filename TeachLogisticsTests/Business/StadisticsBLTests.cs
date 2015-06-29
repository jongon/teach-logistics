using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeachLogistics.Business;
using TeachLogistics.DAL;
using TeachLogisticss;
using TeachLogistics.Models;
using TeachLogistics.ViewModels;

namespace TeachLogistics.Business.Tests
{
    [TestClass()]
    public class StadisticsBLTests : Test
    {
        [TestMethod()]
        public void GetTotalCostTest()
        {
            Group group = Db.Groups.FirstOrDefault();
            if (group == null) 
            {
                return;
            }
            StadisticsBL stadistics = new StadisticsBL();
            List<PeriodResultViewModel> result = stadistics.GetTotalCost(group);
            foreach (var item in result)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
                Console.WriteLine("Cantidad: " + item.Quantity);
            }
        }   

        [TestMethod()]
        public void GetAverageTotalCostTest()
        {
            Section section = Db.Sections.Where(x => x.Number == "1001").FirstOrDefault();
            if (section == null)
            {
                return;
            } 
            StadisticsBL stadistics = new StadisticsBL();
            List<PeriodResultViewModel> result = stadistics.GetAverageTotalCost(section);
            foreach (var item in result)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
                Console.WriteLine("Cantidad: " + item.Quantity);
            }
        }

        [TestMethod()]
        public void GetDemandCostTest()
        {
            Group group = Db.Groups.FirstOrDefault();
            if (group == null) 
            {
                return;
            }
            StadisticsBL stadistics = new StadisticsBL();
            List<PeriodResultViewModel> result = stadistics.GetDemandCost(group);
            foreach (var item in result)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
                Console.WriteLine("Cantidad: " + item.Quantity);
            }
        }

        [TestMethod()]
        public void GetAverageDemandCostTest()
        {
            Section section = Db.Sections.FirstOrDefault();
            if (section == null)
            {
                return;
            }
            StadisticsBL stadistics = new StadisticsBL();
            List<PeriodResultViewModel> result = stadistics.GetAverageDemandCost(section);
            foreach (var item in result)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
                Console.WriteLine("Cantidad: " + item.Quantity);
            }
        }

        [TestMethod()]
        public void GetStockCostTest()
        {
            Group group = Db.Groups.FirstOrDefault();
            if (group == null)
            {
                return;
            }
            StadisticsBL stadistics = new StadisticsBL();
            List<PeriodResultViewModel> result = stadistics.GetStockCost(group);
            foreach (var item in result)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
                Console.WriteLine("Cantidad: " + item.Quantity);
            }
        }

        [TestMethod()]
        public void GetAverageStockCostTest()
        {
            Section section = Db.Sections.FirstOrDefault();
            if (section == null)
            {
                return;
            }
            StadisticsBL stadistics = new StadisticsBL();
            List<PeriodResultViewModel> result = stadistics.GetAverageStockCost(section);
            foreach (var item in result)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
            }   
        }

        [TestMethod()]
        public void GetOrderCostTest()
        {
            Group group = Db.Groups.FirstOrDefault();
            if (group == null)
            {
                return;
            }
            StadisticsBL stadistics = new StadisticsBL();
            List<PeriodResultViewModel> result = stadistics.GetOrderCost(group);
            foreach (var item in result)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
                Console.WriteLine("Cantidad: " + item.Quantity);
            }
        }

        [TestMethod()]
        public void GetAverageOrderCostTest()
        {
            Section section = Db.Sections.FirstOrDefault();
            if (section == null)
            {
                return;
            }
            StadisticsBL stadistics = new StadisticsBL();
            List<PeriodResultViewModel> result = stadistics.GetAverageOrderCost(section);
            foreach (var item in result)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
            }   
        }
    }
}
