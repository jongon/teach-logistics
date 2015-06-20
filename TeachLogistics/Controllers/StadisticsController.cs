using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TeachLogisticsTest.Controllers
{
    public class StadisticsController : BaseController
    {
        // GET: Stadistics
        [Authorize(Roles="Administrador")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            throw new NotImplementedException();
        }


        [Authorize(Roles="Estudiante")]
        public async Task<ActionResult> StudentStadistics()
        {
            throw new NotImplementedException();
        }
    }
}