using MvcFlash.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TeachLogistics.Business;
using TeachLogistics.Models;
using TeachLogistics.ViewModels;

namespace TeachLogistics.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CaseStudiesController : BaseController
    {
        // GET: /InitialCharges/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await Db.CaseStudies.ToListAsync());
        }

        // GET: /InitialCharges/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseStudy caseStudy = await Db.CaseStudies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (caseStudy == null)
            {
                return HttpNotFound();
            }
            return View(caseStudy);
        }

        // GET: /InitialCharges/Create
        [HttpGet]
        public ActionResult Create()
        {
            Db.Sections.Where(x => x.CaseStudy == null).Select(x => new { x.Id, x.Number, x.Semester.Description });
            CaseStudyViewModel caseStudy = new CaseStudyViewModel();
            ViewBag.Products = caseStudy.Products;
            ViewBag.ChargeTypes = caseStudy.ChargeTypes;
            ViewBag.FillTime = caseStudy.FillTimeRadio;
            ViewBag.DeliveryTime = caseStudy.DeliveryTimeRadio;
            ViewBag.PreparationTime = caseStudy.PreparationTimeRadio;
            return View();
        }

        // POST: /InitialCharges/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name,Periods,PreparationTime,AcceleratedPreparationTime,FillTime,ExistingFillTime,DeliveryTime,CourierDeliveryTime,PurchaseOrderRecharge,CourierCharges,PreparationCost,AnnualMaintenanceCost,SemesterId,SectionId,ChargeTypeName,XmlUpload,InitialCharges,PreparationTimeOption,FillTimeOption,DeliveryTimeOption")] CaseStudyViewModel caseStudyViewModel)
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
                        caseStudy.Periods = caseStudyViewModel.Periods;
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
                        if (caseStudyViewModel.SectionId != null)
                        {
                            Section section = Db.Sections.Where(x => x.Id == caseStudyViewModel.SectionId && x.CaseStudy == null).FirstOrDefault();
                            if (section != null)
                                caseStudy.Sections.Add(section);
                            else
                                Flash.Warning("Advertencia", "No se ha podido asignar la sección seleccionada, ya está asignada a un caso de estudio");
                        }
                        Db.CaseStudies.Add(caseStudy);
                        await Db.SaveChangesAsync();
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
                            InitialCharge auxInitialCharge = caseStudyBL.ChangeTimes(caseStudyViewModel, initialCharge);
                            if (TryValidateModel(auxInitialCharge))
                            {
                                auxInitialCharge.CaseStudyId = caseStudyId;  
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
                            Periods = caseStudyViewModel.Periods,
                            Created = DateTime.Now,
                            CourierCharges = caseStudyViewModel.CourierCharges,
                            PurchaseOrderRecharge = caseStudyViewModel.PurchaseOrderRecharge,
                            PreparationCost = caseStudyViewModel.PreparationCost,
                            AnnualMaintenanceCost = caseStudyViewModel.AnnualMaintenanceCost,
                            InitialCharges = initialCharges
                        };
                        if (caseStudyViewModel.SectionId != null)
                        {
                            Section section = Db.Sections.Where(x => x.Id == caseStudyViewModel.SectionId && x.CaseStudy == null).FirstOrDefault();
                            if (section != null)
                                caseStudy.Sections.Add(section);
                            else
                                Flash.Warning("Advertencia", "No se ha podido asignar la sección");
                        }
                        Db.CaseStudies.Add(caseStudy);
                        await Db.SaveChangesAsync();
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
                Db.Sections.Where(x => x.CaseStudy == null).Select(x => new { x.Id, x.Number, x.Semester.Description });
                CaseStudyViewModel caseStudy = new CaseStudyViewModel();
                ViewBag.Products = caseStudy.Products;
                ViewBag.ChargeTypes = caseStudy.ChargeTypes;
                ViewBag.FillTime = caseStudy.FillTimeRadio;
                ViewBag.DeliveryTime = caseStudy.DeliveryTimeRadio;
                ViewBag.PreparationTime = caseStudy.PreparationTimeRadio;
            }
            return View();
        }

        // GET: /InitialCharges/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseStudy caseStudy = await Db.CaseStudies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (caseStudy == null)
            {
                return HttpNotFound();
            }
            return View(caseStudy);
        }

        //// POST: /InitialCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                CaseStudy caseStudy = await Db.CaseStudies.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (caseStudy == null)
                {
                    return HttpNotFound();
                }
                //Revisar que no tenga simulaciones activas
                List<Section> sections = await Db.Sections.Where(x => x.CaseStudyId == id).ToListAsync();
                foreach (var section in sections)
                {
                    section.CaseStudyId = null;
                    if (section.IsActivedSimulation)
                    {
                        throw new Exception();
                    }
                }
                Db.CaseStudies.Remove(caseStudy);
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "El caso de estudio ha sido eliminado exitosamente");
            }
            catch (Exception)
            {
                Flash.Error("Error", "No se puede eliminar el caso de estudio, revise el caso no se esté llevando a cabo por los estudiantes");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> AssignSection(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseStudy caseStudy = await Db.CaseStudies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (caseStudy == null)
            {
                return HttpNotFound();
            }
            AssignSectionViewModel asignSection = new AssignSectionViewModel { 
                Id = caseStudy.Id,
                CaseStudyName = caseStudy.Name,
                Semesters = Db.Semesters.ToList(),
            };
            ViewBag.SelectedSections = caseStudy.Sections.Select(x => x.Id).ToList();
            return View(asignSection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignSection([Bind(Exclude = "CaseStudyName, Semesters")] AssignSectionViewModel caseStudyModel)
        {
            if (ModelState.IsValid)
            {
                CaseStudy caseStudy = await Db.CaseStudies.Where(x => x.Id == caseStudyModel.Id).FirstOrDefaultAsync();
                if (caseStudy == null)
                {
                    Flash.Error("Error", "No existe el caso de estudio");
                    return RedirectToAction("Index");
                }
                List<Section> activatedSections = caseStudy.Sections.Where(x => x.IsActivedSimulation == true).ToList();
                List<Section> finalizedSections = caseStudy.Sections.Where(x => x.IsActivedSimulation == false && x.Periods.Select(c => c.IsLastPeriod).Contains(true)).ToList();
                caseStudy.Sections.Clear();
                if (caseStudyModel.Sections != null)
                {
                    List<Section> sections = Db.Sections.Where(x => caseStudyModel.Sections.Contains(x.Id)).ToList();
                    caseStudy.Sections = sections;
                }
                if (activatedSections.Count() > 0)
                {
                    var displayWarning = false;
                    foreach (var activatedSection in activatedSections)
                    {
                        caseStudy.Sections.Add(activatedSection);
                        if (caseStudyModel.Sections != null)
                        {
                            if (!caseStudyModel.Sections.Contains(activatedSection.Id))
                            {
                                displayWarning = true;
                            }
                        }
                        else
                        {
                            displayWarning = true;
                        }
                    }
                    if (displayWarning)
                    {
                        Flash.Warning("Advertencia", "No pueden ser desasignados los casos estudios que tengan simulaciones activas");
                    }
                }

                if (finalizedSections.Count() > 0)
                {
                    finalizedSections.ForEach(x => x.Groups.ToList().ForEach(t => Db.Groups.Where(z => z.Id == t.Id).FirstOrDefault().IsInSimulation = false));
                    finalizedSections.SelectMany(x => x.Periods).ToList().ForEach(x => Db.Periods.Remove(x));
                    Flash.Warning("Adventencia", "Simulaciones finalizadas han sido eliminadas");
                }
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "El caso de Estudio ha sido asignado a las sección(es) satisfactoriamente");
                return RedirectToAction("Index");
            }
            ViewBag.SelectedSections = Db.Sections.Where(y => y.CaseStudyId == caseStudyModel.Id).Select(x => x.Id).ToList();
            Flash.Error("Error", "Ha Ocurrido un error");
            return View(caseStudyModel);
        }
    }
}
