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

namespace Tesis.DAL
{
    public class ApplicationInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(Tesis.DAL.ApplicationDbContext context)
        {
            var UserManager = new UserManager<User>(new UserStore<User>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            List<string> roles = new List<string> { "Administrador", "Estudiante" };

            foreach (string role in roles)
            {
                if (!RoleManager.RoleExists(role))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(role));
                }
            }

            User user = new User
            {
                Id = "2bc72049-0730-4570-8e16-51e5bf750fdd",
                UserName = "jon.gon1@gmail.com",
                Email = "jon.gon1@gmail.com",
                FirstName = "Jonathan",
                LastName = "Gonzalez",
                IdCard = "18830219",
                EmailConfirmed = true,
            };

            if (UserManager.Users.Where(x => x.Id.Equals(user.Id)).FirstOrDefault() == null)
            {
                var result = UserManager.Create(user, "123456");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Administrador");
                }
            }
            base.Seed(context);
            context.SaveChanges();
        }
    }
}