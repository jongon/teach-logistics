using System.Data.Entity.Migrations;
using Tesis.Business;
using Tesis.DAL;

namespace Tesis.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Tesis.DAL.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            UserBL.CreateFirstUser(context);
        }
    }
}
