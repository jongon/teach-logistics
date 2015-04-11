using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Tesis.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using Tesis.Models;
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