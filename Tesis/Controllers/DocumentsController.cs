using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using MvcFlash.Core.Extensions;
using Tesis.ViewModels;
using System.IO;
using Tesis.Models;
using System.Net;

namespace Tesis.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DocumentsController : BaseController
    {
        // GET: Documents
        public async Task<ActionResult> Index()
        {
            return View(await Db.Documents.ToListAsync());
        }

        // GET: Documents/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude="DocumentPath")]DocumentViewModel documentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var uploadDir = "~/Content/app_files/";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), documentViewModel.Document.FileName);
                    var imageUrl = Path.Combine(uploadDir, documentViewModel.Document.FileName);
                    documentViewModel.Document.SaveAs(imagePath);
                    Db.Documents.Add(new Document
                        {
                            Id = Guid.NewGuid(),
                            Name = documentViewModel.Name,
                            Path = imageUrl,
                        }
                    );
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok", "El documento ha sido agregado satisfactoriamente");
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception();
                }
                
            }
            catch
            {
                Flash.Error("Error", "ha ocurrido un error cargando el archivo");
                return View();
            }
        }

        // GET: Documents/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await Db.Documents.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Document document = await Db.Documents.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (document == null)
                {
                    return HttpNotFound();
                }

                if (!String.IsNullOrEmpty(document.Path))
                {
                    string fullPath = Request.MapPath(document.Path);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                Db.Documents.Remove(document);
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "Documento eliminado satisfactoriamente");
                return RedirectToAction("Index");
            }
            catch
            {
                Flash.Error("Error", "Ha ocurrido un error eliminado el documento");
                return RedirectToAction("Index");
            }

        }
    }
}
