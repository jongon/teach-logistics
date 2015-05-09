using System.Linq;
using System.Web.Mvc;
using MvcFlash.Core;
using System.Net;
using System.Web;
using Tesis.DAL;
using Tesis.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Tesis.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext Db { get; set; }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationUserManager _userManager;

        protected User CurrentUser { get; set; }

        protected string UserId { get; set; }

        protected virtual IFlashPusher Flash { get; private set; }

        protected BaseController()
        {
            Db = new ApplicationDbContext();
            Flash = MvcFlash.Core.Flash.Instance;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserId = User.Identity.GetUserId();
            CurrentUser = UserManager.FindById(User.Identity.GetUserId());
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.User = CurrentUser;
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