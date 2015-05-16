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
                foreach (var demand in demands)
                {
                    var productBalances = balancesGroup.Where(x => x.ProductId == demand.ProductId).OrderBy(y => y.Created).ToList();
                    //Si aún el grupo no ha creado balance se crea nuevo balance
                    Balance balance;
                    if (productBalances.Count() == 0)
                    {
                        balance = CreateFirstBalance(caseStudy, demand, group);
                        group.Balances.Add(balance);
                        continue;
                    }
                    balance = CalculateNextBalance();
                    group.Balances.Add(balance);
                }
            }
        }

        public Balance CreateFirstBalance(CaseStudy caseStudy, Demand demand, Group group)
        {
            int initialStock = caseStudy.InitialCharges.Where(x => x.ProductId == demand.ProductId).Select(y => y.InitialStock).FirstOrDefault();
            int demandNumber = demand.Quantity;
            int receivedOrders = 0;
            int finalStock = (demandNumber >= initialStock) ? 0 : (initialStock - demandNumber);
            int sells = initialStock - finalStock;
            int productPrice = caseStudy.InitialCharges.Where(x => x.ProductId == demand.ProductId).Select(y => y.Price).FirstOrDefault();
            int dissastifiedDemand = (initialStock >= demandNumber) ? 0 : (demandNumber - initialStock);
            int orderCost = 0;
            Balance balance = new Balance
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Demand = demandNumber,
                ReceivedOrders = receivedOrders,
                InitialStock = initialStock,
                FinalStock = finalStock,
                FinalStockCost = finalStock * productPrice,
                DissatisfiedDemand = dissastifiedDemand,
                DissatisfiedCost = dissastifiedDemand * productPrice,
                OrderCost = orderCost,
                Sells = sells,
                Product = demand.Product,
                Group = group,
                Period = demand.Period,
            };
            return balance;
        }

        public Balance CalculateNextBalance()
        {
            throw new NotImplementedException();
        }
    }
}