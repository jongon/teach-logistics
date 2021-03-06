﻿using System;
using System.Collections.Generic;
using System.Linq;
using TeachLogistics.Models;

namespace TeachLogistics.Business
{
    public class SimulationBL
    {
        public void Simulation(Section section)
        {
            CaseStudy caseStudy = section.CaseStudy;
            List<Group> groups = section.Groups.Where(x => x.IsInSimulation == true).ToList();
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
                FinalStockCostPast = 0,
                DissatisfiedCostPast = 0,
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
            if (periods.Count() - 1 > orders.Count())
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
                //group.Orders.Add(lastOrder);
            }
            else
            {
                lastOrder = orders.Last();
            }
            Balance lastBalance = group.Balances.OrderByDescending(x => x.Created).Where(x => x.ProductId == demand.ProductId).FirstOrDefault();
            Balance newBalance = CloneBalance(lastBalance, demand);
            foreach (var order in orders)
            {
                newBalance = BalanceWithPendingOrders(newBalance, order, demand, caseStudy, periods);
            }
            newBalance = UpdateBalance(newBalance, demand, caseStudy, lastOrder);
            return newBalance;
        }

        private Balance CloneBalance(Balance lastBalance, Demand demand)
        {
            return new Balance
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Demand = lastBalance.Demand,
                DissatisfiedCost = lastBalance.DissatisfiedCost,
                DissatisfiedDemand = lastBalance.DissatisfiedDemand,
                FinalStock = lastBalance.FinalStock,
                FinalStockCost = lastBalance.FinalStockCost,
                Group = lastBalance.Group,
                GroupId = lastBalance.GroupId,
                InitialStock = lastBalance.InitialStock,
                OrderCost = lastBalance.OrderCost,
                Period = demand.Period,
                PeriodId = demand.PeriodId,
                Product = demand.Product,
                ProductId = demand.ProductId,
                ReceivedOrders = 0,
                Sells = lastBalance.Sells,
            };
        }

        private Balance BalanceWithPendingOrders(Balance balance, Order order, Demand demand, CaseStudy caseStudy, List<Period> periods)
        {
            int periodLength = periods.Count(); //Número de períodos en el caso de Estudio
            int indexOrderPeriod = periods.IndexOf(order.Period) + 1;
            Product product = demand.Product;
            OrderType orderType = order.OrderType;
            if (orderType != OrderType.None)
            {
                int periodDelivering = PeriodWillBeDelivered(caseStudy, product, orderType);
                if ((periodDelivering + indexOrderPeriod) == periodLength)
                {
                    balance.ReceivedOrders += order.Quantity;
                }
            }
            return balance;
        }

        private int PeriodWillBeDelivered(CaseStudy caseStudy, Product product, OrderType orderType)
        {
            InitialCharge initialCharge = caseStudy.InitialCharges.Where(x => x.Product == product).FirstOrDefault();
            if (orderType == OrderType.Normal)
            {
                return initialCharge.FillTime + initialCharge.PreparationTime + initialCharge.DeliveryTime + 1;
            }
            else if (orderType == OrderType.Fast)
            {
                return initialCharge.FillTime + initialCharge.DeliveryTime + 1;
            }
            else if (orderType == OrderType.Courier)
            {
                return initialCharge.PreparationTime + initialCharge.FillTime + 1;
            }
            else if (orderType == OrderType.FastCourier)
            {
                return initialCharge.FillTime + 1;
            }
            else
            {
                return -1;
            }
        }

        private Balance UpdateBalance(Balance balance, Demand demand, CaseStudy caseStudy, Order lastOrder)
        {
            InitialCharge initialChargeProduct = caseStudy.InitialCharges.Where(x => x.Product == demand.Product).FirstOrDefault();
            int initialStock = balance.FinalStock + balance.ReceivedOrders;
            int demandNumber = demand.Quantity;
            int sells = (initialStock - demandNumber) < 0 ? initialStock : demandNumber;
            int finalStock = initialStock - sells;
            int dissastifiedDemand = (finalStock  > 0) ? 0 : (demandNumber - initialStock);
            double orderCost = GetOrderCost(caseStudy, lastOrder);

            #region UpdateBalance
            balance.Demand = demand.Quantity;
            balance.DissatisfiedDemand = dissastifiedDemand;
            balance.DissatisfiedCostPast = balance.DissatisfiedCost;
            balance.FinalStockCostPast = balance.FinalStockCost;
            balance.DissatisfiedCost = dissastifiedDemand * initialChargeProduct.Price;
            balance.OrderCost = orderCost;
            balance.InitialStock = initialStock;
            balance.FinalStock = finalStock;
            balance.FinalStockCost = finalStock * initialChargeProduct.Price;
            balance.Sells = sells;
            #endregion
            return balance;
        }

        private double GetOrderCost(CaseStudy caseStudy, Order order)
        {
            OrderType orderType = order.OrderType;
            int productPrice = caseStudy.InitialCharges.Where(x => x.ProductId == order.ProductId).Select(x => x.Price).FirstOrDefault();
            switch (orderType)
            {
                case OrderType.Normal:
                    return productPrice * order.Quantity;
                case OrderType.Fast:
                    return caseStudy.PreparationCost + (productPrice * order.Quantity);
                case OrderType.Courier:
                    return ((double)(caseStudy.CourierCharges * productPrice) * order.Quantity) + (productPrice * order.Quantity);
                case OrderType.FastCourier:
                    return ((double)(caseStudy.CourierCharges * productPrice) * order.Quantity) + caseStudy.PreparationCost + (productPrice * order.Quantity);
                default:
                    return 0;
                
            }
        }

        public double GetOrderCost(InitialCharge initialCharge, OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.Fast:
                    return initialCharge.CaseStudy.PreparationCost;
                case OrderType.Courier:
                    return (initialCharge.CaseStudy.CourierCharges * initialCharge.Price);
                case OrderType.FastCourier:
                    return initialCharge.CaseStudy.PreparationCost;
                default:
                    return 0;

            }
        }

        public int GetTimeOrder(InitialCharge initialCharge, OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.Normal:
                    return initialCharge.PreparationTime + initialCharge.DeliveryTime + initialCharge.FillTime + 1;
                case OrderType.Fast:
                    return initialCharge.FillTime + initialCharge.DeliveryTime + 1;
                case OrderType.Courier:
                    return initialCharge.PreparationTime + initialCharge.FillTime + 1;
                case OrderType.FastCourier:
                    return initialCharge.FillTime + 1;
                default:
                    return 0;

            }
        }
    }
}