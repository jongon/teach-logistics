using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeachLogisticsTest.Models;
using TeachLogisticsTest.ViewModels;

namespace TeachLogisticsTest.Business
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
                groupResult = GetGroupResult(group);
                groupsResult.Add(groupResult);
            }
            return groupsResult;
        }

        public GroupResultViewModel GetGroupResult(Group group)
        {
            GroupResultViewModel groupResult;

            throw new NotImplementedException();
        }
    }
}