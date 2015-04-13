using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using MvcFlash.Core.Extensions;
using Tesis.ViewModels;
using Tesis.Models;

namespace Tesis.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class SectionsController : BaseController
    {
        // GET: /Sections/
        public async Task<ActionResult> Index()
        {
            return View(await Db.Semesters.Where(x => x.Sections.Count > 0).ToListAsync());
        }

        public async Task<JsonResult> GetSectionsBySemester(Guid? SemesterId)
        {
            if (SemesterId == null)
            {
                return Json(new { hasError = true, data = "id not found" }, JsonRequestBehavior.AllowGet); 
            }
            else
            {
                var sections = await Db.Sections.Where(x => x.Semester.Id == SemesterId).Select(c => new { Id = c.Id, Number = c.Number }).ToListAsync();
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
            Section section = await Db.Sections.FindAsync(id);
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
                Semester semester = await Db.Semesters.Where(x => x.Id == section.SemesterId).FirstOrDefaultAsync();
                if (semester != null) {
                    Section sectionDomain = new Section { Id = Guid.NewGuid(), Number = section.Number, Semester = semester };
                    Db.Sections.Add(sectionDomain);
                    await Db.SaveChangesAsync();
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

            SectionViewModel section = await Db.Sections.Where(c => c.Id == id).Select(x => new SectionViewModel { Id = x.Id, Number = x.Number, SemesterId = x.Semester.Id }).FirstAsync();
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
                Semester semester = Db.Semesters.Where(x => x.Id == section.SemesterId).First();
                if (semester != null)
                {
                    Section sectionDomain = await Db.Sections.Where(x => x.Id == section.Id).FirstAsync();
                    sectionDomain.Number = section.Number;
                    sectionDomain.Semester = semester;
                    Db.Entry(sectionDomain).State = EntityState.Modified;
                    await Db.SaveChangesAsync();
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
            Section section = await Db.Sections.FindAsync(id);
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
            try
            {
                Section section = await Db.Sections.FindAsync(id);
                if (section != null)
                {
                    Db.Sections.Remove(section);
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok!", "Sección eliminada exitosamente");
                    return RedirectToAction("Index");
                }
                Flash.Error("Error", "No se ha podido eliminar la sección");
                return View(section);
            } catch(Exception) {
                Flash.Error("Error", "No se ha podido eliminar la sección, puede tener relaciones");
                return RedirectToAction("Index");
            }
        }
    }
}
