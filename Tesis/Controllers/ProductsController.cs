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
            int errors = 0;
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
                        await db.SaveChangesAsync();
                    } else {
                        errors++;
                    }
                }
                if (errors <= 0) {
                    Flash.Success("Los Productos se han agregado correctamente");
                    return RedirectToAction("Index");
                } else {
                    Flash.Error("Error", errors + " productos no puedieron ser agregados");
                    return RedirectToAction("Index");
                }
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

        public JsonResult ExistsProductNumber(string number)
        {
            bool result = false;
            int numberProduct = Convert.ToInt32(number);
            try
            {
                Product product = db.Products.Where(x => x.Number == numberProduct).FirstOrDefault();
                if (product != null)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExistsProductName(string name)
        {
            bool result = false;
            try
            {
                Product product = db.Products.Where(x => x.Name == name).FirstOrDefault();
                if (product != null)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
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
            ProductViewModelEdit productViewModel = new ProductViewModelEdit
            {
                Id = product.Id,
                Name = product.Name,
                Number = product.Number,
                City = product.City,
                Distance = product.Distance
            };
            return View(productViewModel);
        }

        // POST: /Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Number,Name,City,Distance")] ProductViewModelEdit productViewModel)
        {
            Product product = db.Products.Where(x => x.Number == productViewModel.Number && x.Name == productViewModel.Name).FirstOrDefault();
            if (product == null)
            {
                product = db.Products.Where(x => x.Number == productViewModel.Number || x.Name == productViewModel.Name).FirstOrDefault();
                if (product != null)
                {
                    Flash.Error("Error", "El usuario no puede ser editado con esos valores");
                    return View(productViewModel);
                }
            }
            if (ModelState.IsValid)
            {
                product = new Product
                {
                    Id = productViewModel.Id,
                    Number = productViewModel.Number,
                    Name = productViewModel.Name,
                    City = productViewModel.City,
                    Distance = productViewModel.Distance
                };
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Flash.Success("Ok", "El producto ha sido editado exitosamente");
                return RedirectToAction("Index");
            }
            return View(productViewModel);
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
