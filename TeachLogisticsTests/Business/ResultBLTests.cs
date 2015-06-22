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
    public class ResultBLTests : Test
    {
        [TestMethod()]
        public void GetGroupsResultTest()
        {
            Section section = Db.Sections.Where(x => x.Number == "1001").FirstOrDefault();
            ResultBL resultBL = new ResultBL();
            List<GroupResultViewModel> groupsResult = (List<GroupResultViewModel>)resultBL.GetGroupsResult(section);
            PrintGroupsDetails(groupsResult);
        }

        [TestMethod()]
        public void GetGroupResultsTest()
        {
            Group group = Db.Groups.FirstOrDefault();
            ResultBL resultBL = new ResultBL();
            List<GroupDetailedResultViewModel> result = resultBL.GetGroupResults(group);
            PrintGroupDetail(result);
        }

        private void PrintGroupsDetails(List<GroupResultViewModel> groups) {
            foreach (var group in groups)
            {
                Console.WriteLine("--------------------------=========================---------------");
                Console.WriteLine("Id: " + group.GroupId);
                Console.WriteLine("Grupo: " + group.GroupName);
                PrintGroupDetail(group.GroupDetailedResult);
            }
        }

        private void PrintGroupDetail(List<GroupDetailedResultViewModel> details)
        {
            foreach (var item in details)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("Periodo: " + item.PeriodNumber);
                Console.WriteLine("Costo Final de Inventario: " + item.FinalStockCost);
                Console.WriteLine("Costo Total de las ordenes: " + item.TotalOrderCost);
                Console.WriteLine("Costo Total Demanda Insatisfecha: " + item.UnsatisfiedDemandCost);
            }
        }
    }
}
