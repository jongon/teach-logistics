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
        protected virtual IFlashPusher Flash { get; private set; }

        protected BaseController()
        {
            Flash = MvcFlash.Core.Flash.Instance;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            ViewBag.User = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            base.OnActionExecuted(filterContext);
        }
    }
    
}