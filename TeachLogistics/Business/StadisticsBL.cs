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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}