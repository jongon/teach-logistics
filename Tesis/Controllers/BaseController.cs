using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using MvcFlash.Core;
using Tesis.DAL;

namespace Tesis.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext Db { get; set; }

        protected virtual IFlashPusher Flash { get; private set; }

        protected BaseController()
        {
            Db = new ApplicationDbContext();
            Flash = MvcFlash.Core.Flash.Instance;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.User = Db.Users.Where(x => x.Id == userId).FirstOrDefault();
            base.OnActionExecuted(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    
}