﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcFlash.Core;
using MvcFlash.Core.Extensions;
using Tesis;
using Tesis.ViewModels;
using Tesis.Business;
using System.Collections;
using Tesis.Models;
using Tesis.DAL;
using System.IO;

namespace Tesis.Controllers
{
    public class CaseStudiesController : BaseController
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
            db.Sections.Where(x => x.CaseStudy == null).Select(x => new { x.Id, x.Number, x.Semester.Description });
            CaseStudyViewModel caseStudy = new CaseStudyViewModel();
            ViewBag.Products = caseStudy.Products;
            ViewBag.ChargeTypes = caseStudy.ChargeTypes;
            return View();
        }

        // POST: /InitialCharges/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include="Demand,Stddev,Price,PreparationCost,AnnualMaintenanceCost,PreparationTime,FillTime,DeliveryTime,SecurityStock,InitialStock,ProductId")] CaseStudyViewModel initialCharge)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByXml(CreateByXmlViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CaseStudyXmlBL caseStudyBL = new CaseStudyXmlBL();
                    CaseStudyXml caseStudyXml = caseStudyBL.Deserealize(model.XmlUpload.InputStream);
                    CaseStudy caseStudy = caseStudyBL.XmlToModel(caseStudyXml);
                    db.CaseStudies.Add(caseStudy);
                    db.SaveChanges();
                    Flash.Success("Ok", "El archivo Xml es correcto, se ha creado el caso de estudio");
                }
                catch (Exception)
                {
                    Flash.Error("Ok", "El Archivo Xml no ha podido ser cargado, revise que esté correctamente formado y tenga todos los campos llenos");
                }
            }
            return View();
        }

        public ActionResult CreateByXml()
        {
            return View();
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
