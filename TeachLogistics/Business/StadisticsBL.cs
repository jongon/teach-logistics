using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeachLogistics.Models;
using TeachLogistics.ViewModels;

namespace TeachLogistics.Business
{
    public class StadisticsBL
    {
        public List<PeriodResultViewModel> GetTotalCost(Group group)
        {
            var results = group.Balances
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new PeriodResultViewModel
                {
                    PeriodNumber = index++,
                    Quantity = t.Key.Balances.Where(x => x.Group == group).Sum(x => (x.OrderCost + x.DissatisfiedCostPast + x.FinalStockCostPast))
                })
                .ToList();
            if (results.Count() > 0)
            {
                results.RemoveAt(0);
            }
            return results;
        }

        public List<PeriodResultViewModel> GetAverageTotalCost(Section section)
        {
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation).ToList<Group>();
            var results = groups.SelectMany(x => x.Balances)
                .Where(x => x.Period != section.Periods.OrderBy(t => t.Created).ToList().FirstOrDefault())
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new PeriodResultViewModel
                {
                    PeriodNumber = index++,
                    Quantity = section.Groups.Where(x => x.IsInSimulation)
                        .Average(x => x.Balances.Where(w => w.Period == t.Key).Sum(z => z.DissatisfiedCostPast + z.OrderCost + z.FinalStockCostPast))         
                })
                .ToList();
            return results;
        }

        public List<PeriodResultViewModel> GetDemandCost(Group group)
        {
            var results = group.Balances
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new PeriodResultViewModel
                {
                    PeriodNumber = index++,
                    Quantity = t.Key.Balances.Where(x => x.Group == group).Sum(x => x.DissatisfiedCostPast)
                })
                .ToList();
            if (results.Count() > 0)
            {
                results.RemoveAt(0);
            }
            return results;
        }

        public List<PeriodResultViewModel> GetAverageDemandCost(Section section)
        {
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation).ToList<Group>();
            var results = groups.SelectMany(x => x.Balances)
                .Where(x => x.Period != section.Periods.OrderBy(t => t.Created).ToList().FirstOrDefault())
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new PeriodResultViewModel
                {
                    PeriodNumber = index++,
                    Quantity = section.Groups.Where(x => x.IsInSimulation)
                        .Average(x => x.Balances.Where(w => w.Period == t.Key).Sum(z => z.DissatisfiedCostPast)) 
                })
                .ToList();
            return results;
        }

        public List<PeriodResultViewModel> GetStockCost(Group group)
        {
            var results = group.Balances
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new PeriodResultViewModel
                {
                    PeriodNumber = index++,
                    Quantity = t.Key.Balances.Where(x => x.Group == group).Sum(x => x.FinalStockCostPast)
                })
                .ToList();
            if (results.Count() > 0)
            {
                results.RemoveAt(0);
            }
            return results;
        }

        public List<PeriodResultViewModel> GetAverageStockCost(Section section)
        {
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation).ToList<Group>();
            var results = groups.SelectMany(x => x.Balances)
                .Where(x => x.Period != section.Periods.OrderBy(t => t.Created).ToList().FirstOrDefault())
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new PeriodResultViewModel
                {
                    PeriodNumber = index++,
                    Quantity = section.Groups.Where(x => x.IsInSimulation)
                        .Average(x => x.Balances.Where(w => w.Period == t.Key).Sum(z => z.FinalStockCostPast))
                })
                .ToList();
            return results;
        }

        public List<PeriodResultViewModel> GetOrderCost(Group group)
        {
            var results = group.Balances
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new PeriodResultViewModel
                {
                    PeriodNumber = index++,
                    Quantity = t.Key.Balances.Where(x => x.Group == group).Sum(x => x.OrderCost)
                })
                .ToList();
            if (results.Count() > 0)
            {
                results.RemoveAt(0);
            }
            return results;
        }

        public List<PeriodResultViewModel> GetAverageOrderCost(Section section)
        {
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation).ToList<Group>();
            var results = groups.SelectMany(x => x.Balances)
                .Where(x => x.Period != section.Periods.OrderBy(t => t.Created).ToList().FirstOrDefault())
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new PeriodResultViewModel
                {
                    PeriodNumber = index++,
                    Quantity = section.Groups.Where(x => x.IsInSimulation)
                        .Average(x => x.Balances.Sum(z => z.OrderCost))
                })
                .ToList();
            return results;
        }
    }
}