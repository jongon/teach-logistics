using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeachLogistics.Models;
using TeachLogistics.ViewModels;

namespace TeachLogistics.Business
{
    public class ResultBL
    {
        public ICollection<GroupResultViewModel> GetGroupsResult(Section section)
        {
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation == true).ToList();
            List<GroupResultViewModel> groupsResult = new List<GroupResultViewModel>();
            foreach (Group group in groups)
            {
                GroupResultViewModel groupResult = new GroupResultViewModel {
                    GroupId = group.Id,
                    GroupName = group.Name,
                    GroupDetailedResult = GetGroupResults(group),
                };
                groupsResult.Add(groupResult);
            }
            return groupsResult;
        }

        public List<GroupDetailedResultViewModel> GetGroupResults(Group group)
        {
            var results = group.Balances
                .OrderBy(x => x.Period.Created)
                .GroupBy(x => x.Period)
                .Select((t, index) => new GroupDetailedResultViewModel
                {
                    PeriodNumber = index++,
                    PeriodId = t.Key.Id,
                    FinalStockCost = t.Key.Balances.Where(x => x.Group == group).Sum(x => x.FinalStockCostPast),
                    UnsatisfiedDemandCost = t.Key.Balances.Where(x => x.Group == group).Sum(x => x.DissatisfiedCostPast),
                    TotalOrderCost = t.Key.Balances.Where(x => x.Group == group).Sum(x => x.OrderCost)
                })
                .ToList();
            if (results.Count() > 0)
            {
                results.RemoveAt(0);
            }
            return results;
        }

        public DetailedGroupResultViewModel GetDetailedGroupResult(Group group, Period period)
        {
            DetailedGroupResultViewModel result = new DetailedGroupResultViewModel();
            var resultList = group.Balances
                .Where(x => x.Period == period)
                .Select(x => new ResultViewModel
                {
                    ProductName = x.Product.Name,
                    ProductNumber = x.Product.Number.ToString(),
                    FinalStockCost = x.FinalStockCostPast,
                    UnsatisfiedDemandCost = x.DissatisfiedCostPast,
                    OrderCost = x.OrderCost
                })
                .ToList();
            result.PeriodNumber = group.Section.Periods.OrderBy(x => x.Created).ToList().IndexOf(period);
            result.Results = resultList;
            return result;
        }

        public List<GroupRankingViewModel> GetRanking(Section section)
        {
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation == true).ToList();
            List<GroupRankingViewModel> groupsRanking = groups
                .OrderByDescending(x => (x.Balances.Sum(c => c.DissatisfiedCostPast) + x.Balances.Sum(c => c.FinalStockCostPast) + x.Balances.Sum(c => c.OrderCost)))
                .Select(x => new GroupRankingViewModel {
                    GroupName = x.Name,
                    Score = x.Balances.Sum(c => c.DissatisfiedCostPast) + x.Balances.Sum(c => c.FinalStockCostPast) + x.Balances.Sum(c => c.OrderCost),
                })
                .ToList();
            return groupsRanking;
        }
    }
}