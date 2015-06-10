using MvcFlash.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tesis.Models;
using Tesis.ViewModels;

namespace Tesis.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class GroupsController : BaseController
    {
        // GET: /Groups/
        public async Task<ActionResult> Index()
        {
            return View(await Db.Groups.ToListAsync());
        }

        // GET: /Groups/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await Db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: /Groups/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Name,SectionId,SemesterId,Users")] GroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                if (!(group.Users.Count() == Db.Users.Where(x => (x.SectionId == group.SectionId && x.GroupId == null) && (group.Users.Contains(x.Id))).Count()))
                {
                    Flash.Error("Error", "Ha ocurrido un error creando el grupo, revise que el usuario no tenga un grupo asignado");
                    return View(group);
                }

                try
                {
                    List<User> users = await Db.Users.Where(x => group.Users.Contains(x.Id)).ToListAsync();
                    group.Id = Guid.NewGuid();
                    Db.Groups.Add(new Group { Id = group.Id, Name = group.Name, Score = "0", Users = users, SectionId = group.SectionId });
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok", "Grupo creado exitosamente");
                    ViewBag.SemesterId = group.SemesterId.ToString();
                    ViewBag.SectionId = group.SectionId.ToString();
                    ModelState.Clear();
                    return View();
                }
                catch (Exception e)
                {
                    Flash.Error("Error", e.Message.ToString());
                    return View(group);
                } 
            }
            return View(group);
        }

        // GET: /Groups/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await Db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            GroupViewModel groupViewModel = new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
                SectionId = group.SectionId,
                SemesterId = group.Section.SemesterId,
            };
            ViewBag.Users = new MultiSelectList(group.Users.Select(x => new { Value = x.Id,  Text = x.FirstName + " " + x.LastName }), "Value", "Text");
            return View(groupViewModel);
        }

        // POST: /Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,SectionId,SemesterId,Users")] GroupViewModel group)
        {
            try
            {
                ViewBag.Users = new MultiSelectList(new List<User>());  
                if (ModelState.IsValid)
                {
                    Group currentGroup = Db.Groups.Where(x => x.Id == group.Id).FirstOrDefault();
                    foreach (var user in currentGroup.Users)
                    {
                        user.Group = null;
                    }
                    await Db.SaveChangesAsync();
                    if (!(group.Users.Count() == Db.Users.Where(x => (x.SectionId == group.SectionId && x.GroupId == null) && (group.Users.Contains(x.Id))).Count()))
                    {
                        Flash.Error("Error", "Ha ocurrido un error creando el grupo, revise que el usuario no tenga un grupo asignado");
                        return View(group);
                    }
                    currentGroup.Users = await Db.Users.Where(x => group.Users.Contains(x.Id)).ToListAsync();
                    Db.Entry(currentGroup).State = EntityState.Modified;
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok", "Grupo ha sido editado satisfactoriamente");
                    return RedirectToAction("Index");
                }
                Flash.Error("Error", "Ha ocurrido un error intentando editar el usuario");
                return View(group);
            }
            catch (Exception)
            {
                Flash.Error("Error", "Ha ocurrido un error intentando editar el usuario");
                return View(group);
            }
        }

        // GET: /Groups/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await Db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: /Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Group group = await Db.Groups.FindAsync(id);
            foreach (var user in group.Users)
            {
                user.Group = null;
            }
            try
            {
                Db.Groups.Remove(group);
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "Grupo ha sido eliminado correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Flash.Error("Error", "Ha ocurrido un error eliminando el grupo");
                return View(group);
            }
        }
    }
}
