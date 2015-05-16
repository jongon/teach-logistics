using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis.Models;

namespace Tesis.Business
{
    public class SimulationBL
    {
        public void Simulation(Section section)
        {
            CaseStudy caseStudy = section.CaseStudy;
            List<Group> groups = section.Groups.ToList();
            Period lastPeriod = section.Periods.OrderByDescending(x => x.Created).FirstOrDefault();
            List<Demand> demands = lastPeriod.Demands.ToList();
            //Simulación para cada uno de los grupos de la sección
            foreach (var group in groups)
            {
                //Las ordenes de un período del grupo
                List<Order> ordersInPeriod = group.Orders.Where(x => x.PeriodId == lastPeriod.Id).ToList();
                //Los Balances del grupo
                var balancesGroup = group.Balances;
                //Iteración de las ventas del último período
                foreach (var sale in demands)
                {
                    var productBalances = balancesGroup.Where(x => x.ProductId == sale.ProductId).OrderBy(y => y.Created).ToList();
                    //Si aún el grupo no ha creado balance se crea nuevo balance
                    if (productBalances.Count() == 0)
                    {

                    }

                    foreach (var balance in productBalances)
                    {
                        
                    }
                }
            }
        }

        public Balance CreateFirstBalance(CaseStudy caseStudy, Guid ProductId)
        {
            throw new NotImplementedException();
        }
    }
}