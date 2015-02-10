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
using System.Collections;
using Tesis.Models;
using Tesis.DAL;

namespace Tesis.Controllers
{
    public class InitialChargesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /InitialCharges/
        public async Task<ActionResult> Index()
        {
            return View(await db.InitialCharges.ToListAsync());
        }

        // GET: /InitialCharges/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InitialCharge initialCharge = await db.InitialCharges.FindAsync(id);
            if (initialCharge == null)
            {
                return HttpNotFound();
            }
            return View(initialCharge);
        }

        // GET: /InitialCharges/Create
        public ActionResult Create()
        {
            //Traerme las secciones y semestres
            //db.Sections.Where(x => x.InitialCharges == null).Select(x => new { x.Id, x.Number, x.Semester.Description });
            //InitialChargeViewModel initialCharge = new InitialChargeViewModel();
            //ViewBag.Products = initialCharge.Products;
            return View();
        }

        // POST: /InitialCharges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Demand,Stddev,Price,PreparationCost,AnnualMaintenanceCost,PreparationTime,FillTime,DeliveryTime,SecurityStock,InitialStock,ProductId")] InitialChargeViewModel initialCharge)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products.Where(x => x.Id == initialCharge.ProductId).First();
                InitialChargeBO initialChargeBO = new InitialChargeBO(initialCharge);
                initialChargeBO.InitialCharge.Product = product;
                db.InitialCharges.Add(initialChargeBO.InitialCharge);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");              
            }

            return View(initialCharge);
        }

        // GET: /InitialCharges/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InitialCharge initialCharge = await db.InitialCharges.FindAsync(id);
            if (initialCharge == null)
            {
                return HttpNotFound();
            }
            return View(initialCharge);
        }

        // POST: /InitialCharges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Demand,Stddev,Price,PreparationCost,AnnualMaintenanceCost,WeeklyMaintenanceCost,PurchaseOrderRecharge,CourierCharges,PreparationTime,FillTime,DeliveryTime,TotalTime,ReplacementBatch,MinimunBatchReplacement,ProductQuaterlyCost,RequestQuaterlyCost,MaintenanceQuaterlyCost,TotalQuaterlyCost,SecurityStock,VariationCoefficient,CycleTime,AverageStock,EOQ,InitialStock")] InitialCharge initialCharge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(initialCharge).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(initialCharge);
        }

        // GET: /InitialCharges/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InitialCharge initialCharge = await db.InitialCharges.FindAsync(id);
            if (initialCharge == null)
            {
                return HttpNotFound();
            }
            return View(initialCharge);
        }

        // POST: /InitialCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            InitialCharge initialCharge = await db.InitialCharges.FindAsync(id);
            db.InitialCharges.Remove(initialCharge);
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
