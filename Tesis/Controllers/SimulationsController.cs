using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tesis.ViewModels;
using MvcFlash.Core.Extensions;
using Tesis.Models;
using System.Net;
using Tesis.Business;

namespace Tesis.Controllers
{
    public class SimulationsController : BaseController
    {
        // GET: Simulations
        public async Task<ActionResult> Index()
        {
            return View(await Db.Sections.Where(x => x.CaseStudyId != null).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> EnableSimulation([Bind(Prefix = "EnableId")] Guid? Id)
        {
            try
            {
                Section section = await Db.Sections.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (section == null)
                {
                    throw new Exception();
                }
                section.IsActivedSimulation = true;
                if (section.Periods.Count() == 0)
                {
                    Period period = new Period
                    {
                        Created = DateTime.Now,
                        Id = Guid.NewGuid(),
                        IsLastPeriod = section.CaseStudy.Periods == 1 ? true : false,
                    };
                    section.Periods.Add(period);
                }
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "La Simulación ha sido activada con exito");
                return RedirectToAction("Index");
            }
            catch
            {
                Flash.Error("Error", "Ha Ocurrido un error habilitando la sección");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DisableSimulation([Bind(Prefix = "DisableId")] Guid? Id)
        {
            Section section = await Db.Sections.Where(x => x.Id == Id).FirstOrDefaultAsync<Section>();
            try 
            {
                if (section == null)
                {
                    throw new Exception();
                }
                section.IsActivedSimulation = false;
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "La Simulación ha sido finalizada");
                return RedirectToAction("Index");
            }
            catch
            {
                Flash.Error("Error", "Ha ocurrido un error finalizando la simulación");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> RegisterDemands(Guid? Id)
        {
            try
            {
                Section section = await Db.Sections.Where(x => x.Id == Id).FirstOrDefaultAsync<Section>();
                if (section == null)
                {
                    throw new Exception();
                }

                if (section.Periods.Count() == 0 || section.IsActivedSimulation == false)
                {
                    Flash.Error("Error", "No ha sido activada la simulación");
                    return RedirectToAction("Index");
                }

                var caseStudyQuery = Db.CaseStudies.Where(x => x.Id == section.CaseStudyId);
                CaseStudy caseStudy = await caseStudyQuery.FirstOrDefaultAsync();
                DemandViewModel sellViewModel = new DemandViewModel
                {
                    ProductDemands = caseStudy.InitialCharges.Select(y => new ProductDemand { Product = y.Product }).ToList<ProductDemand>(),
                    Section = section,
                    SectionId = section.Id,
                };
                return View(sellViewModel);
            }
            catch
            {
                Flash.Error("Error", "Ha ocurrido un error inesperado");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDemands(DemandViewModel model)
        {
            if (ModelState.IsValid)
            {
                Section section = await Db.Sections.Where(x => x.Id == model.SectionId).FirstOrDefaultAsync();
                if (section == null)
                {
                    return HttpNotFound();
                }
                Period period = section.Periods.OrderByDescending(x => x.Created).FirstOrDefault();
                period.Demands = new List<Demand>();
                foreach (var productSell in model.ProductDemands)
                {
                    if (TryValidateModel(productSell))
                    {
                        Demand sale = new Demand
                        {
                            Product = await Db.Products.Where(x => x.Id == productSell.Product.Id).FirstOrDefaultAsync(),
                            Quantity = productSell.Quantity
                        };
                        period.Demands.Add(sale);
                    }
                    else
                    {
                        Flash.Error("Error", "Ha Ocurrido un error inesperado");
                        return RedirectToAction("Index");
                    }
                }
                await Db.SaveChangesAsync();
                //llamada a la simulación
                SimulationBL simulation = new SimulationBL();
                simulation.Simulation(await Db.Sections.Where(x => x.Id == section.Id).FirstOrDefaultAsync());
                //Fin de llamada a la simulación

                if (!period.IsLastPeriod)
                {
                    Period newPeriod = new Period
                    {
                        Created = DateTime.Now,
                        Id = Guid.NewGuid(),
                        IsLastPeriod = section.CaseStudy.Periods == section.Periods.Count() + 1 ? true : false,
                    };
                    section.Periods.Add(newPeriod);
                }
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "Las ventas del período han sido registradas exitosamente");
                return RedirectToAction("Index");
            }
            else
            {
                Flash.Error("Error", "Ha Ocurrido un error registrando las ventas");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Groups(Guid? Id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult> Rankings(Guid? Id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult> Orders(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Group group = await Db.Groups.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (group == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Period lastPeriod = await Db.Periods.Where(y => y.Section == group.Section).OrderByDescending(x => x.Created).FirstOrDefaultAsync();
            if (lastPeriod == null)
            {
                //Tira un error de que el model de gestión aún no ha empezado
                Flash.Error("Error", "El Modelo de gestión aún no ha empezado");
                return RedirectToAction("Index", "Home");
            }
            List<Order> ordersInLastPeriod = lastPeriod.Orders.Where(x => x.Group == group).ToList();
            if (ordersInLastPeriod.Count() > 0)
            {
                //Tira un error de que no puede llenar ordenes hasta que el profesor ingrese demanda.
                Flash.Error("Error", "El Profesor aún no sumistrado nuevas demandas");
                return RedirectToAction("Index", "Home");
            }
            List<Balance> balances = await Db.Balances.Where(x => x.GroupId == id && x.Period == lastPeriod).ToListAsync<Balance>();     
            List<OrderViewModel> orders = balances.Select(x => new OrderViewModel {
                Demand = x.Demand,
                FinalStock = x.FinalStock,
                FinalStockCost = x.FinalStockCost,
                InitialStock = x.InitialStock,
                ReceivedOrders = x.ReceivedOrders,
                ProductName = x.Product.Name,
                ProductNumber = x.Product.Number,
                Sells = x.Sells,
                UnsatisfiedDemand = x.DissatisfiedDemand,
                UnsatisfiedDemandCost = x.DissatisfiedCost
            }).ToList();
            PeriodViewModel periodViewModel = new PeriodViewModel {
                CaseStudyName = group.Section.CaseStudy.Name,
                WeekNumber = group.Section.Periods.Count(),
                Orders = orders,
            };
            return View(periodViewModel);
        }
    }
}