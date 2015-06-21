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
            Group group = Db.Groups.FirstOrDefault();
            ResultBL resultBL = new ResultBL();
            var groups = resultBL.GetGroupResults(group);
        }
    }
}
