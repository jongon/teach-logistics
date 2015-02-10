using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tesis;
using MvcFlash.Core;
using MvcFlash.Core.Extensions;
using Tesis.ViewModels;
using Tesis.Models;
using Tesis.DAL;

namespace Tesis.Controllers
{
    public class SectionsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Sections/
        public async Task<ActionResult> Index()
        {
            return View(await db.Semesters.Where(x => x.Sections.Count > 0).ToListAsync());
        }

        public async Task<JsonResult> GetSectionsBySemester(Guid? SemesterId)
        {
            if (SemesterId == null)
            {
                return Json(new { hasError = true, data = "id not found" }, JsonRequestBehavior.AllowGet); 
            }
            else
            {
                var sections = await db.Sections.Where(x => x.Semester.Id == SemesterId).Select(c => new { Id = c.Id, Number = c.Number }).ToListAsync();
                return Json(sections, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: /Sections/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: /Sections/Create
        public ActionResult Create()
        {
            SectionViewModel section = new SectionViewModel();
            ViewBag.Semesters = section.Semesters;
            return View();
        }

        // POST: /Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="SemesterId, Number")] SectionViewModel section)
        {
            if (ModelState.IsValid)
            {
                Semester semester = await db.Semesters.Where(x => x.Id == section.SemesterId).FirstOrDefaultAsync();
                if (semester != null) {
                    Section sectionDomain = new Section { Id = Guid.NewGuid(), Number = section.Number, Semester = semester };
                    db.Sections.Add(sectionDomain);
                    await db.SaveChangesAsync();
                    Flash.Success("Ok", "Sección creada exitosamente");
                    return RedirectToAction("Index");
                } 
            }
            Flash.Error("Error", "Ha habido un error creando la sección");
            return View(section);
        }

        // GET: /Sections/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SectionViewModel section = await db.Sections.Where(c => c.Id == id).Select(x => new SectionViewModel { Id = x.Id, Number = x.Number, SemesterId = x.Semester.Id }).FirstAsync();
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.Semesters = section.Semesters;
            return View(section);
        }

        // POST: /Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id, Number, SemesterId")] SectionViewModel section)
        {
            if (ModelState.IsValid)
            {
                Semester semester = db.Semesters.Where(x => x.Id == section.SemesterId).First();
                if (semester != null)
                {
                    Section sectionDomain = await db.Sections.Where(x => x.Id == section.Id).FirstAsync();
                    sectionDomain.Number = section.Number;
                    sectionDomain.Semester = semester;
                    db.Entry(sectionDomain).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    Flash.Success("Ok!", "La sección ha sido editada exitosamente");
                    return RedirectToAction("Index");
                }
            }
            Flash.Error("Error", "Ha habido un problema editando la sección");
            return View(section);
        }

        // GET: /Sections/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: /Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Section section = await db.Sections.FindAsync(id);
            if (section != null)
            {
                db.Sections.Remove(section);
                await db.SaveChangesAsync();
                Flash.Success("Ok!", "Sección eliminada exitosamente");
                return RedirectToAction("Index");
            }
            Flash.Error("Error", "No se ha podido eliminar la sección");
            return View(section);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
