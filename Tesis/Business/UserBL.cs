using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using Tesis.DAL;
using Tesis.Models;

namespace Tesis.Business
{
    public class UserBL
    {
        public static void CreateFirstUser(ApplicationDbContext context) {
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
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                FirstName = "Administrador",
                LastName = "Administrador",
                IdCard = "111111",
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
        }
    }
}