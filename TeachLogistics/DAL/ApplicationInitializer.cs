using System.Data.Entity;
using TeachLogisticsTest.Business;

namespace TeachLogisticsTest.DAL
{
    public class ApplicationInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            UserBL.CreateFirstUser(context);
        }
    }
}