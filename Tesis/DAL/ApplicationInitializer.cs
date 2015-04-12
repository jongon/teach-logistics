using System.Data.Entity;
using Tesis.Business;

namespace Tesis.DAL
{
    public class ApplicationInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            UserBL.CreateFirstUser(context);
        }
    }
}