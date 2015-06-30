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
            Group group = CurrentUser.Group;
            if (group == null || group.IsInSimulation == false)
            {
                Flash.Error("Error", "No pertenece a ningún grupo para poder visualizar estadisticas");
                return RedirectToAction("Index", "Home");
            }
            Section section = CurrentUser.Section;
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
        [HttpGet]
        public ActionResult Groups()
        {
            List<Group> groups = CurrentUser.Section.Groups.Where(x => x.IsInSimulation).OrderBy(x => x.Name).ToList();
            return View(groups);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GroupStadistics(Guid? GroupId)
        {
            throw new NotImplementedException();
        }
    }
}