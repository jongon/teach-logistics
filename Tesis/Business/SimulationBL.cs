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
                    balance = CalculateNextBalance(caseStudy, demand, group, periods);
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

        private Balance CalculateNextBalance(CaseStudy caseStudy, Demand demand, Group group, List<Period> periods)
        {
            List<Order> orders = group.Orders.Where(x => x.ProductId == demand.ProductId).OrderBy(y => y.Created).ToList();
            periods = periods.OrderBy(x => x.Created).ToList(); //List de períodos ordenados de menor a mayor en orden de creación
            //Si el estudiante no creo ninguna orden en este período se le crea
            //Pero se le crea vacia
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
            Balance newBalance = group.Balances.OrderByDescending(x => x.Created).FirstOrDefault();
            foreach (var order in orders)
            {
                newBalance = BalanceWithPendingOrders(newBalance, order, demand, caseStudy, periods);
            }
            return newBalance;
        }

        private Balance BalanceWithPendingOrders(Balance balance, Order order, Demand demand, CaseStudy caseStudy, List<Period> periods)
        {
            int periodLength = periods.Count(); //Número de períodos en el caso de Estudio
            int indexOrderPeriod = periods.IndexOf(order.Period);
            OrderType orderType = order.OrderType;
            Product product = demand.Product;
            int periodDelivering = PeridoWillBeDelivered(caseStudy, product, orderType);
            if ((periodDelivering + indexOrderPeriod) == periodLength)
            {
                return UpdateBalance(balance, caseStudy, product);
            }
            return balance;
        }

        private int PeridoWillBeDelivered(CaseStudy caseStudy, Product product, OrderType orderType)
        {
            throw new NotImplementedException();
        }

        private Balance UpdateBalance(Balance balance, CaseStudy caseStudy, Product product)
        {
            throw new NotImplementedException();
        }
    }
}