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
                        balancesGroup.Add(balance);
                        continue;
                    }
                    List<Period> periods = section.Periods.ToList();
                    List<Order> orders = section.Periods.SelectMany(x => x.Orders.Where(y => y.ProductId == demand.ProductId && y.GroupId == group.Id)).ToList();
                    balance = CalculateNextBalance(caseStudy, demand, group, orders, periods);
                    balancesGroup.Add(balance);
                }
            }
        }

        private Balance CreateFirstBalance(CaseStudy caseStudy, Demand demand, Group group)
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

        private Balance CalculateNextBalance(CaseStudy caseStudy, Demand demand, Group group, List<Order> orders, List<Period> periods)
        {
            //Si el estudiante no creo ninguna orden en este período se le crea
            //Pero se le crea vacia
            int periodLength = periods.Count();
            Order lastOrder;
            if (periods.Count() > orders.Count())
            {
                lastOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderType = OrderType.None,
                    Quantity = 0,
                    Group = group,
                    Product = demand.Product,
                    Period = demand.Period,
                    Created = DateTime.Now,
                };
                group.Orders.Add(lastOrder);
            }
            else
            {
                lastOrder = orders.OrderByDescending(c => c.Created).FirstOrDefault();
            }
            throw new NotImplementedException();
            //DeliveryTimes deliveryTimes = new DeliveryTimes{
            //    OrdinaryOrderTime =  caseStudy.PreparationTime,
            //    FastOrderTIme = caseStudy.AcceleratedPreparationTime,
            //    CourierDeliveryTime = caseStudy.FillTime + caseStudy.CourierDeliveryTime,
            //    CourierDeliveryFastOrderTime = caseStudy.FillTime
            //};

            ////Costs costs = new Costs
            ////{
            ////    OrdinaryOrderCost = caseStudy.PreparationCost,
            ////    FastOrderCost = caseStudy.
            ////    CourierChargeCost = caseStudy.CourierCharges,
                    
            ////};

            //Balance newBalance = group.Balances.OrderByDescending(x => x.Created).FirstOrDefault();
            //foreach (var order in orders)
            //{
            //    newBalance = BalanceWithPendingOrders(newBalance, deliveryTimes, costs, periodLength);
            //}
            //return newBalance;
        }

        public Balance BalanceWithPendingOrders(Balance balance, DeliveryTimes deliveryTimes, Costs costs, int periodLength)
        {
            throw new NotImplementedException();
        }

        private struct DeliveryTimes
        {
            public int OrdinaryOrderTime { get; set; }

            public int FastOrderTIme { get; set; }

            public int CourierDeliveryTime { get; set; }

            public int CourierDeliveryFastOrderTime { get; set; }
        }

        private struct Costs
        {
            public int OrdinaryOrderCost { get; set; }

            public double CourierChargeCost { get; set; }

            public int AnnualMaintenanceCost { get; set; }
        }
    }

}