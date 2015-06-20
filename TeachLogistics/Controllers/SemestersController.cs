using MvcFlash.Core.Extensions;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TeachLogisticsTest.Models;
using TeachLogisticsTest.ViewModels;

namespace TeachLogisticsTest.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class SemestersController : BaseController
    {
        // GET: /Semesters/
        public async Task<ActionResult> Index()
        {
            var semesters = await Db.Semesters.Select(x => new SemesterViewModel { Description = x.Description, Id = x.Id }).ToListAsync();
            return View(semesters);
        }

        public async Task<JsonResult> IndexJson()
        {
            var semesters = await Db.Semesters.Select(c => new { Id = c.Id, Description = c.Description }).ToListAsync();
            return Json(semesters, JsonRequestBehavior.AllowGet);
        }

        // GET: /Semesters/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterViewModel semester = await Db.Semesters.Select(x => new SemesterViewModel { Id = x.Id, Description = x.Description }).SingleAsync(l => l.Id == id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        // GET: /Semesters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Semesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Description")] SemesterViewModel semesterViewModel)
        {
            if (ModelState.IsValid)
            {
                Semester semester = new Semester();
                semester.Id = Guid.NewGuid();
                semester.Description = semesterViewModel.Description;
                Db.Semesters.Add(semester);
                await Db.SaveChangesAsync();
                Flash.Success("OK", "Semestre Creado Exitosamente");
                return RedirectToAction("Index");
            }
            Flash.Error("Error", "Ha Ocurrido un error al agregar el semestre, revise los campos");
            return View(semesterViewModel);
        }

        // GET: /Semesters/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterViewModel semester = await Db.Semesters.Select(x => new SemesterViewModel { Id = x.Id, Description = x.Description }).SingleAsync(l => l.Id == id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        // POST: /Semesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id, Description")] SemesterViewModel semester)
        {
            if (ModelState.IsValid)
            {
                Semester semesterDomain = new Semester { Id = semester.Id, Description = semester.Description };
                Db.Entry(semesterDomain).State = EntityState.Modified;
                await Db.SaveChangesAsync();
                Flash.Success("OK", "Semestre editado exitosamente");
                return RedirectToAction("Index");
            }
            Flash.Error("Error", "El semestre no ha podido ser editado");
            return View(semester);
        }

        // GET: /Semesters/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterViewModel semester = await Db.Semesters.Select(x => new SemesterViewModel { Id = x.Id, Description = x.Description }).SingleAsync(l => l.Id == id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        // POST: /Semesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                Semester semester = await Db.Semesters.FindAsync(id);
                if (semester != null)
                {
                    Db.Semesters.Remove(semester);
                    await Db.SaveChangesAsync();
                    Flash.Success("OK", "El semestre ha sido eliminado exitosamente");
                    return RedirectToAction("Index");
                }
                Flash.Error("Error", "Ha ocurrido un error eliminando el semestre");
                return RedirectToAction("Delete", id);
            }
            catch (Exception)
            {
                Flash.Error("Error", "Ha ocurrido un error eliminando el semestre, puede tener relaciones");
                return RedirectToAction("Index");
            }
        }
    }
}
