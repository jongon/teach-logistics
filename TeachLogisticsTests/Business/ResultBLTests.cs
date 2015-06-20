using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeachLogisticsTest.Business;
using TeachLogisticsTest.DAL;
using TeachLogisticsTests;
using TeachLogisticsTest.Models;

namespace TeachLogisticsTest.Business.Tests
{
    [TestClass()]
    public class ResultBLTests : Test
    {
        [TestMethod()]
        public void GetGroupResultTest()
        {
            Section section = Db.Sections.Where(x => x.Number == "1001").FirstOrDefault();
            ResultBL resultBL = new ResultBL();
            var groups = resultBL.GetGroupResult(section);
            foreach (var group in groups)
            {
                
            }
        }
    }
}
