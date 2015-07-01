using MvcFlash.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using TeachLogistics.Business;
using TeachLogistics.Models;
using TeachLogistics.ViewModels;

namespace TeachLogistics.Controllers
{
    public class StadisticsController : BaseController
    {
        // GET: Stadistics
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }


        [Authorize(Roles = "Estudiante")]
        [HttpGet]
        public ActionResult StudentStadistics()
        {
            Section section = CurrentUser.Section;
            if (section.CaseStudy == null)
            {
                Flash.Error("Error", "No existe un modelo de gestión asignado para esta sección");
                return RedirectToAction("Index", "Home");
            }
            Group group = CurrentUser.Group;
            if (group == null || group.IsInSimulation == false)
            {
                Flash.Error("Error", "No pertenece a ningún grupo para poder visualizar estadisticas");
                return RedirectToAction("Index", "Home");
            }
            if (section.Periods.Count() == 0)
            {
                Flash.Warning("Adventencia", "No hay datos para mostrar");
            }
            StadisticsBL stadistics = new StadisticsBL();
            ResultBL results = new ResultBL();
            ViewBag.PeriodNumber = section.CaseStudy.Periods;
            StadisticsViewModel stadisticsViewModel = new StadisticsViewModel();
            stadisticsViewModel.TotalCost = stadistics.GetTotalCost(group);
            stadisticsViewModel.DemandCost = stadistics.GetDemandCost(group);
            stadisticsViewModel.OrderCost = stadistics.GetOrderCost(group);
            stadisticsViewModel.StockCost = stadistics.GetStockCost(group);
            stadisticsViewModel.AverageTotalCost = stadistics.GetAverageTotalCost(section);
            stadisticsViewModel.AverageDemandCost = stadistics.GetAverageDemandCost(section);
            stadisticsViewModel.AverageOrderCost = stadistics.GetAverageOrderCost(section);
            stadisticsViewModel.AverageStockCost = stadistics.GetAverageStockCost(section);
            stadisticsViewModel.Groups = results.GetRanking(section);
            return View(stadisticsViewModel);
        }

        [Authorize(Roles = "Estudiante")]
        [ActionName("GroupsStudents")]
        [HttpGet]
        public async Task<ActionResult> Groups()
        {
            List<GroupStadistics> groups = await Db.Groups
                .Where(x => x.Section.Id == CurrentUser.Section.Id && x.IsInSimulation && x.Id != CurrentUser.GroupId)
                .Select(x => new GroupStadistics
                {
                    GroupId = x.Id,
                    GroupName = x.Name
                })
                .ToListAsync();
            return View("Groups", groups);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<ActionResult> Groups(Guid SectionId)
        {
            if (SectionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await Db.Sections.Where(x => x.Id == SectionId).FirstOrDefaultAsync();
            if (section == null)
            {
                return HttpNotFound();
            }
            List<GroupStadistics> groups = await Db.Groups
                .Where(x => x.Section.Id == section.Id && x.IsInSimulation)
                .Select(x => new GroupStadistics
                {
                    GroupId = x.Id,
                    GroupName = x.Name
                })
                .ToListAsync();
            return View(groups);
        }
             
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<ActionResult> Sections()
        {
            List<SectionStadistics> sections = await Db.Sections
                .Where(x => x.IsActivedSimulation || x.Periods.Select(t => t.IsLastPeriod).Contains(true))
                .Select(x => new SectionStadistics {
                    SectionId = x.Id,
                    Section = x.Number,
                    Semester = x.Semester.Description
                })
                .ToListAsync();
            return View(sections);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GroupStadistics(Guid? GroupId)
        {
            if (GroupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await Db.Groups.Where(x => x.Id == GroupId).FirstOrDefaultAsync();
            if (group == null)
            {
                return HttpNotFound();
            }
            Section section = group.Section;
            StadisticsBL stadistics = new StadisticsBL();
            ResultBL results = new ResultBL();
            ViewBag.PeriodNumber = section.CaseStudy.Periods;
            StadisticsViewModel stadisticsViewModel = new StadisticsViewModel();
            stadisticsViewModel.TotalCost = stadistics.GetTotalCost(group);
            stadisticsViewModel.DemandCost = stadistics.GetDemandCost(group);
            stadisticsViewModel.OrderCost = stadistics.GetOrderCost(group);
            stadisticsViewModel.StockCost = stadistics.GetStockCost(group);
            stadisticsViewModel.AverageTotalCost = stadistics.GetAverageTotalCost(section);
            stadisticsViewModel.AverageDemandCost = stadistics.GetAverageDemandCost(section);
            stadisticsViewModel.AverageOrderCost = stadistics.GetAverageOrderCost(section);
            stadisticsViewModel.AverageStockCost = stadistics.GetAverageStockCost(section);
            stadisticsViewModel.Groups = results.GetRanking(section);
            return View(stadisticsViewModel);
        }
    }
}