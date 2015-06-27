using System.Web.Mvc;
using TeachLogistics.Business;

namespace TeachLogistics.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Administrador"))
            {
                return View();
            }
            else if (User.IsInRole("Estudiante"))
            {
                return View("IndexStudent");
            }
            return HttpNotFound();
        }
    }
}