using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis.DAL;
using Tesis.Models;

namespace Tesis.Business
{
    public class UserBO : User
    {
        //Método que obtiene los nombre de los roles del usuario
        public List<string> getRolesNameFromUser() {
            ApplicationDbContext db = new ApplicationDbContext();
            var UserManager = new UserManager<User>(new UserStore<User>(db));
            var RoleNames = UserManager.GetRoles(this.Id).ToList();
            return RoleNames;
        }
    }
}