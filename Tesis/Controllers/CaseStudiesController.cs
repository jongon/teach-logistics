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
            return View(await db.CaseStudies.ToListAsync());
        }

        // GET: /InitialCharges/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseStudy caseStudy = await db.CaseStudies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (caseStudy == null)
            {
                return HttpNotFound();
            }
            return View(caseStudy);
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
        public async Task<ActionResult> Create([Bind(Include = "Name,PreparationTime,AcceleratedPreparationTime,FillTime,ExistingFillTime,DeliveryTime,CourierDeliveryTime,PurchaseOrderRecharge,CourierCharges,PreparationCost,AnnualMaintenanceCost,SemesterId,SectionId,ChargeTypeName,XmlUpload,InitialCharges")] CaseStudyViewModel caseStudyViewModel)
        {
            try {
                CaseStudyBL caseStudyBL = new CaseStudyBL();
                if (caseStudyViewModel.ChargeTypeName == "xml")
                {
                    caseStudyBL.ModelStateForXML(ModelState);
                    if (ModelState.IsValid)
                    {
                        CaseStudyXmlBL caseStudyXmlBL = new CaseStudyXmlBL();
                        CaseStudyXml caseStudyXml = caseStudyXmlBL.Deserealize(caseStudyViewModel.XmlUpload.InputStream);
                        CaseStudy caseStudy = caseStudyXmlBL.XmlToModel(caseStudyXml);
                        caseStudy.Name = caseStudyViewModel.Name;
                        foreach (var initialCharge in caseStudy.InitialCharges)
                        {
                            if (!TryValidateModel(initialCharge))
                            {
                                throw new Exception("Ha ocurrido un error agregando los datos de un producto");
                            }
                        }
                        if (!TryValidateModel(caseStudy))
                        {
                            throw new Exception("Ha ocurrido un error agregando los datos de un producto");
                        }
                        db.CaseStudies.Add(caseStudy);
                        await db.SaveChangesAsync();
                        Flash.Success("Ok", "El archivo Xml es correcto, se ha creado el caso de estudio");
                        return RedirectToAction("Index");
                    }
                } else if (caseStudyViewModel.ChargeTypeName == "form") {
                    caseStudyBL.ModelStateInForm(ModelState);
                    if (ModelState.IsValid)
                    {
                        Guid caseStudyId = Guid.NewGuid();
                        List<InitialCharge> initialCharges = caseStudyBL.JsonToInitialChargeList(caseStudyViewModel.InitialCharges);
                        foreach (var initialCharge in initialCharges)
                        {
                            if (TryUpdateModel(initialCharge))
                            {
                                initialCharge.CaseStudyId = caseStudyId;  
                            }
                            else
                            {
                                Flash.Error("Error", "Ha ocurrido un error agregando los datos de un producto");
                                throw new Exception();
                            }
                        }
                        CaseStudy caseStudy = new CaseStudy
                        {
                            Id = caseStudyId,
                            Name = caseStudyViewModel.Name,
                            Created = DateTime.Now,
                            PreparationTime = caseStudyViewModel.PreparationTime,
                            AcceleratedPreparationTime = caseStudyViewModel.AcceleratedPreparationTime,
                            FillTime = caseStudyViewModel.FillTime,
                            ExistingFillTime = caseStudyViewModel.ExistingFillTime,
                            DeliveryTime = caseStudyViewModel.DeliveryTime,
                            CourierDeliveryTime = caseStudyViewModel.CourierDeliveryTime,
                            CourierCharges = caseStudyViewModel.CourierCharges,
                            PurchaseOrderRecharge = caseStudyViewModel.PurchaseOrderRecharge,
                            PreparationCost = caseStudyViewModel.PreparationCost,
                            AnnualMaintenanceCost = caseStudyViewModel.AnnualMaintenanceCost,
                            InitialCharges = initialCharges
                        };
                        if (caseStudyViewModel.SectionId != null)
                        {
                            Section section = db.Sections.Where(x => x.Id == caseStudyViewModel.SectionId).FirstOrDefault();
                            caseStudy.Sections.Add(section);
                        }
                        db.CaseStudies.Add(caseStudy);
                        await db.SaveChangesAsync();
                        Flash.Success("Ok", "El caso de estudio ha sido agregado exitosamente");
                        return RedirectToAction("Index");
                    } else {
                        throw new Exception("El caso de estudio no ha podido ser almacenado correctamente");
                    }
                }
                else
                {
                    throw new Exception("Error Inesperado");
                }
            } catch(Exception e) {
                Flash.Error("Error", e.Message);
                db.Sections.Where(x => x.CaseStudy == null).Select(x => new { x.Id, x.Number, x.Semester.Description });
                CaseStudyViewModel caseStudy = new CaseStudyViewModel();
                ViewBag.Products = caseStudy.Products;
                ViewBag.ChargeTypes = caseStudy.ChargeTypes;
            }
            return View();
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
            CaseStudy caseStudy = await db.CaseStudies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (caseStudy == null)
            {
                return HttpNotFound();
            }
            return View(caseStudy);
        }

        // POST: /InitialCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                CaseStudy caseStudy = await db.CaseStudies.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (caseStudy == null)
                {
                    return HttpNotFound();
                }
                db.CaseStudies.Remove(caseStudy);
                await db.SaveChangesAsync();
                Flash.Success("Ok", "El caso de estudio ha sido eliminado exitosamente");
            }
            catch (Exception)
            {
                Flash.Error("Error", "No se puede eliminar el caso de estudio, revise el caso no se esté llevando a cabo por los estudiantes");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> AsignSection(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseStudy caseStudy = await db.CaseStudies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (caseStudy ==  null) {
                return HttpNotFound();
            }
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> AsignSection(Guid id)
        //{

        //}

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
