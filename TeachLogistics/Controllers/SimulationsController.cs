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
    public class SimulationsController : BaseController
    {
        // GET: Simulations
        [Authorize(Roles="Administrador")]
        public async Task<ActionResult> Index()
        {
            return View(await Db.Sections.Where(x => x.CaseStudyId != null).ToListAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> EnableSimulation([Bind(Prefix = "EnableId")] Guid? Id)
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Section section = await Db.Sections.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (section == null)
                {
                    return HttpNotFound();
                }
                section.IsActivedSimulation = true;
                foreach (var group in section.Groups)
                {
                    group.IsInSimulation = true;
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> RegisterDemands(Guid? Id)
        {
            try
            {
                Section section = await Db.Sections.Where(x => x.Id == Id).FirstOrDefaultAsync<Section>();
                if (section == null)
                {
                    throw new Exception();
                }

                if (section.IsActivedSimulation == false)
                {
                    Flash.Error("Error", "No ha sido activada la simulación");
                    return RedirectToAction("Index");
                }

                var caseStudyQuery = Db.CaseStudies.Where(x => x.Id == section.CaseStudyId);
                CaseStudy caseStudy = await caseStudyQuery.FirstOrDefaultAsync();
                DemandViewModel sellViewModel = new DemandViewModel
                {
                    ProductDemands = caseStudy.InitialCharges.Select(y => new ProductDemand { Product = y.Product }).OrderBy(t => t.Product.Number).ToList<ProductDemand>(),
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
        [Authorize(Roles="Administrador")]
        public async Task<ActionResult> RegisterDemands(DemandViewModel model)
        {
            if (ModelState.IsValid)
            {
                Section section = await Db.Sections.Where(x => x.Id == model.SectionId).FirstOrDefaultAsync();
                if (section == null)
                {
                    return HttpNotFound();
                }
                Period newPeriod = new Period
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsLastPeriod = section.CaseStudy.Periods == section.Periods.Count() + 1 ? true : false,
                };
                section.Periods.Add(newPeriod);
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
        [Authorize(Roles = "Estudiante")]
        public async Task<ActionResult> Orders()
        {
            Group group = await Db.Groups.Where(x => x.Users.Select(c => c.Id).Contains(CurrentUser.Id)).FirstOrDefaultAsync();
            if (group == null)
            {
                Flash.Error("Error", "Lo Siento pero usted no pertenece a ningún grupo para empezar el modelo de gestión");
                return RedirectToAction("Index", "Home");
            }
            if (!group.Section.IsActivedSimulation)
            {
                //Tira un error de que el model de gestión aún no ha empezado
                Flash.Error("Error", "El Modelo de gestión aún no ha empezado");
                return RedirectToAction("Index", "Home");
            }
            Period lastPeriod = group.Section.Periods.Where(t => t.Demands.Count() > 0).OrderByDescending(x => x.Created).FirstOrDefault();
            if (lastPeriod == null)
            {
                //Tira un error de que no puede llenar ordenes hasta que el profesor ingrese demanda.
                Flash.Error("Error", "El Profesor aún no suministra nuevas demandas");
                return RedirectToAction("Index", "Home");
            }
            else if (lastPeriod.Orders.Where(x => x.Group == group).Count() != 0)
            {
                //Tira un error de que no puede llenar ordenes hasta que el profesor ingrese demanda.
                Flash.Error("Error", "El Profesor aún no sumistra nuevas demandas");
                return RedirectToAction("Index", "Home");
            }
            SimulationBL simulation = new SimulationBL();
            if (group.IsInSimulation == false)
            {
                Flash.Error("Error", "Este Modelo de gestión ya ha iniciado no puede incluirse");
                return RedirectToAction("Index", "Home");
            }
            List<Balance> balances = group.Balances.Where(x => x.PeriodId == lastPeriod.Id).ToList<Balance>();
            List<OrderViewModel> orders = balances.Select(x => new OrderViewModel {
                Demand = x.Demand,
                FinalStock = x.FinalStock,
                FinalStockCost = x.FinalStockCost,
                InitialStock = x.InitialStock,
                ReceivedOrders = x.ReceivedOrders,
                ProductName = x.Product.Name,
                ProductNumber = x.Product.Number,
                ProductId = x.ProductId,
                ProductPrice = group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault().Price,
                OrderCostTime = new OrderCostTime {
                    OrdinaryOrderCost = simulation.GetOrderCost(group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault(), OrderType.Normal),
                    CourierOrderCost = simulation.GetOrderCost(group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault(), OrderType.Courier),
                    FastCourierCost = simulation.GetOrderCost(group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault(), OrderType.FastCourier),
                    FastOrderCost = simulation.GetOrderCost(group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault(), OrderType.Fast),
                    OrdinaryOrderTime = simulation.GetTimeOrder(group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault(), OrderType.Normal),
                    CourierTime = simulation.GetTimeOrder(group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault(), OrderType.Courier),
                    FastOrderTime = simulation.GetTimeOrder(group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault(), OrderType.Fast),
                    FastCourierTime = simulation.GetTimeOrder(group.Section.CaseStudy.InitialCharges.Where(t => t.ProductId == x.ProductId).FirstOrDefault(), OrderType.FastCourier),
                },
                Sells = x.Sells,
                UnsatisfiedDemand = x.DissatisfiedDemand,
                UnsatisfiedDemandCost = x.DissatisfiedCost
            })
            .OrderBy(x => x.ProductNumber)
            .ToList();
            //Lista de integrantes
            List<User> members = group.Users.Select(x => new User { Id = x.Id, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName }).ToList();
            PeriodViewModel periodViewModel = new PeriodViewModel {
                CaseStudy = group.Section.CaseStudy,
                Members = members,
                Group = group,
                WeekNumber = group.Section.Periods.Count(),
                GroupId = group.Id,
                Orders = orders,
                IsLastPeriod = lastPeriod.IsLastPeriod,
            };
            return View(periodViewModel);
        }

        [HttpPost]
        [ActionName("MakeOrders")]
        [Authorize(Roles = "Estudiante")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Orders([Bind(Include="Orders,GroupId,IsLastPeriod")] PeriodViewModel model)
        {
            Group group = await Db.Groups.Where(x => x.Users.Select(c => c.Id).Contains(CurrentUser.Id)).FirstOrDefaultAsync();
            if (group == null)
            {
                Flash.Error("Error", "Lo Siento pero usted no pertenece a ningún grupo para empezar el modelo de gestión");
                return RedirectToAction("Index", "Home");
            }
            if (!group.Section.IsActivedSimulation)
            {
                //Tira un error de que el model de gestión aún no ha empezado
                Flash.Error("Error", "El Modelo de gestión aún no ha empezado");
                return RedirectToAction("Index", "Home");
            }
            Period lastPeriod = group.Section.Periods.Where(t => t.Demands.Count() > 0).OrderByDescending(x => x.Created).FirstOrDefault();
            if (lastPeriod == null)
            {
                //Tira un error de que no puede llenar ordenes hasta que el profesor ingrese demanda.
                Flash.Error("Error", "El Profesor aún no suministra nuevas demandas");
                return RedirectToAction("Index", "Home");
            }
            else if (lastPeriod.Orders.Where(x => x.Group == group).Count() != 0)
            {
                //Tira un error de que no puede llenar ordenes hasta que el profesor ingrese demanda.
                Flash.Error("Error", "El Profesor aún no sumistra nuevas demandas");
                return RedirectToAction("Index", "Home");
            }
            
            if (group.IsInSimulation == false)
            {
                Flash.Error("Error", "Este Modelo de gestión ya ha iniciado no puede incluirse");
                return RedirectToAction("Index", "Home");
            }
            lastPeriod = group.Section.Periods.OrderByDescending(x => x.Created).FirstOrDefault();
            if (ModelState.IsValid)
            {
                foreach (var order in model.Orders)
                {
                    if (!TryValidateModel(order))
                    {
                        Flash.Error("Error", "Ha ocurrido un error con las ordenes");
                        return RedirectToAction("Orders");
                    }
                    Product product = Db.Products.Where(x => x.Id == order.ProductId).FirstOrDefault();
                    Order newOrder = new Order
                    {
                        Id = Guid.NewGuid(),
                        Created = DateTime.Now,
                        Group = group,
                        OrderType = (order.OrderMethodOption == null  || order.Quantity == null) ? OrderType.None : (OrderType)order.OrderMethodOption,
                        Product = product,
                        Quantity = order.Quantity == null ? 0 : (int)order.Quantity, 
                    };
                    lastPeriod.Orders.Add(newOrder);
                }
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "Las Ordenes han sido realizadas exitosamente");
                return View("Index");
            }
            else
            {
                Flash.Error("Error", "Ha Ocurrido un error creando las ordenes");
                return View("Index");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Demands(Guid? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await Db.Sections.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.Section = section;
            List<Period> periods = await Db.Periods.Where(x => x.SectionId == Id && x.Section.IsActivedSimulation == true).OrderByDescending(t => t.Created).ToListAsync<Period>();
            if (periods.Count() > 0)
            {
                return View(periods);
            }
            Flash.Error("Error", "No hay demandas suministradas aún");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Groups(Guid? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await Db.Sections.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.Section = section;
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation == true).ToList<Group>();
            return View(groups);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Rankings(Guid? Id)
        {
            throw new NotImplementedException();
        }
    }
}