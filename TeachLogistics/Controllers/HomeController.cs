using System.Web.Mvc;

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
                return View();
            }
            return HttpNotFound();
        }
    }
}