using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Tesis.Models;
using Tesis.ViewModels;
using Tesis.Business;

namespace Tesis.Controllers
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

            List<GroupResultViewModel> groups = (List<GroupResultViewModel>)ResultBL.GetGroupResult(section);  
            return View("Index", groups);
        }

    }
}