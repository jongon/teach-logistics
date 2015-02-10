using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFlash.Core;
using MvcFlash.Core.Extensions;
using System.Threading.Tasks;
using Tesis.Models;
using Tesis.DAL;

namespace Tesis.Controllers
{
    [AllowAnonymous]
    public class AdminController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult AddMenu()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddMenu([Bind(Include = "Name")] Menu menu, string option = "null")
        //{
        //    if (ModelState.IsValid)
        //    {
        //        menu.Id = Guid.NewGuid();
        //        db.Menus.Add(menu);
        //        await db.SaveChangesAsync();
        //        ViewBag.MenuName = menu.Name;
        //        Flash.Success("El Menú ha sido creado exitosamente");
        //        if (option.Equals("element"))
        //        {
        //            return RedirectToAction("AddMenuElements");
        //        } else if (option == null) {
        //            return View();
        //        }
        //    } else {
        //        Flash.Error("Ha habido un error salvando el menú");
        //        return View(menu);
        //    }
        //    return View(menu);
        //}

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
