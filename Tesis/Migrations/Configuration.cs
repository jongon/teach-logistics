using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Tesis.Business;
using Tesis.Controllers;
using Tesis.DAL;
using Tesis.Models;

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
