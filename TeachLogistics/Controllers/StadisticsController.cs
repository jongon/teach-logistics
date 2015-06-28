using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TeachLogistics.Controllers
{
    public class StadisticsController : BaseController
    {
        // GET: Stadistics
        [Authorize(Roles="Administrador")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }


        [Authorize(Roles="Estudiante")]
        [HttpGet]
        public async Task<ActionResult> StudentStadistics()
        {
            return View("Index");
        }
    }
}