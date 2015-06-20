using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

using TeachLogisticsTest.DAL;

namespace TeachLogisticsTests
{
    public class Test
    {
        public ApplicationDbContext Db { get; set; }

        public Test()
        {
            this.Db = new ApplicationDbContext();
        }
    }
}
