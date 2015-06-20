using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis.Models;
using Tesis.ViewModels;

namespace Tesis.Business
{
    public class ResultBL
    {
        public ICollection<GroupResultViewModel> GetGroupResult(Section section)
        {
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation == true).ToList();
            List<GroupResultViewModel> groupsResult = new List<GroupResultViewModel>();
            foreach (Group group in groups)
            {
                GroupResultViewModel groupResult = new GroupResultViewModel();
                var periodBalances = group.Balances.OrderBy(x => x.Period.Created).GroupBy(x => x.Period).ToList();
                foreach (var period in periodBalances)
                {
                    
                }
            }
            throw new NotImplementedException();
        }
    }
}