using MvcFlash.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using TeachLogistics.Models;
using TeachLogistics.ViewModels;
using TeachLogistics.Business;

namespace TeachLogistics.Controllers
{
    [Authorize]
    public class ResultsController : BaseController
    {
        public ResultBL ResultBL { get; set; }

        public ResultsController()
        {
            this.ResultBL = new ResultBL();
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Results(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await Db.Sections.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (section == null)
            {
                return HttpNotFound();
            }
            List<GroupResultViewModel> groups = (List<GroupResultViewModel>)ResultBL.GetGroupsResult(section);
            if (groups.Count() > 0)
            {
                ViewBag.Section = section;
                return View("Index", groups);
            }
            Flash.Error("Error", "No existe grupos en la simulación");
            return RedirectToAction("Index", "Simulations");
        }

        [HttpGet]
        [ActionName("GroupResults")]
        [Authorize(Roles = "Estudiante")]
        public async Task<ActionResult> Results()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [ActionName("GroupDetails")]
        [Authorize(Roles = "Estudiante")]
        public async Task<ActionResult> Details()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Details(Guid? GroupId, Guid? PeriodId)
        {
            if (GroupId == null || PeriodId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await Db.Groups.Where(x => x.Id == GroupId && x.Balances.Select(t => t.PeriodId).Contains((Guid)PeriodId)).FirstOrDefaultAsync();
            if (group == null)
            {
                return HttpNotFound();
            }
            ResultBL resultBL = new ResultBL();

            throw new NotImplementedException();
        }
    }
}