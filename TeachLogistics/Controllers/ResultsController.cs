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
    [Authorize(Roles = "Administrador")]
    public class ResultsController : BaseController
    {
        public ResultBL ResultBL { get; set; }

        public ResultsController()
        {
            this.ResultBL = new ResultBL();
        }

        [HttpGet]
        public async Task<ActionResult> Results(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await Db.Sections.Where(x => x.Id == Id).FirstOrDefaultAsync();

            List<GroupResultViewModel> groups = (List<GroupResultViewModel>)ResultBL.GetGroupsResult(section);  
            return View("Index", groups);
        }

    }
}