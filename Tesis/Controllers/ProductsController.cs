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
using Tesis.ViewModels;
using Tesis.Business;
using MvcFlash.Core;
using MvcFlash.Core.Extensions;
using Tesis.Models;
using Tesis.DAL;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Tesis.Controllers
{
    [AllowAnonymous]
    public class ProductsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string products)
        {
            try {
                List<ProductViewModel> productsList = (List<ProductViewModel>)JsonConvert.DeserializeObject(products, typeof(List<ProductViewModel>));
                foreach (var productViewModel in productsList)
                {
                    if (TryValidateModel(productViewModel)) {
                        Product product = new Product { 
                            Id = Guid.NewGuid(),
                            Name = productViewModel.Name,
                            City = productViewModel.City,
                            Distance = productViewModel.Distance,
                            Number = productViewModel.Number
                        };
                        db.Products.Add(product);
                    } else {
                        throw new Exception();
                    }
                }
                await db.SaveChangesAsync();
                Flash.Success("Los Productos se han agregado correctamente");
                return RedirectToAction("Index");
            } catch (Exception) {
                Flash.Error("Ha Ocurrido un error al agregar el producto, revise los campos");
                return View();
            }
        }

        // GET: /Products/
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: /Products/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // GET: /Products/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Number,Name,City,Distance")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Products/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
