﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class DemandViewModel {

        [DisplayName("Sección")]
        public virtual Section Section { get; set; }

        [DisplayName("Sección")]
        [Required(ErrorMessage = "El Id de la sección es requerido")]
        public virtual Guid SectionId { get; set; }

        public ICollection<Product> Products { get; set; }

        [DisplayName("Productos")]
        [Required(ErrorMessage = "Los Productos son requeridos")]
        [UIHint("Products")]
        public ICollection<ProductDemand> ProductDemands { get; set; }
    }

    public class ProductDemand
    {
        [DisplayName("Producto")]
        public Product Product { get; set; }

        [DisplayName("Cantidad")]
        [Required(ErrorMessage = "La Cantidad de productos es requerida")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public int Quantity { get; set; }
    }

    public class PeriodViewModel
    {
        [DisplayName("Número de Semana")]
        public int WeekNumber { get; set; }

        [DisplayName("Ordenes")]
        public IList<OrderViewModel> Orders { get; set; }

        [DisplayName("Grupo")]
        public Guid GroupId { get; set; }

        [DisplayName("Grupo")]
        public virtual Group Group { get; set; }

        [DisplayName("Caso de Estudio")]
        public virtual CaseStudy CaseStudy { get; set; }
    }

    public class OrderViewModel
    {   
        [DisplayName("Nro de Producto")]
        public int ProductNumber { get; set; }

        [DisplayName("NombreProducto")]
        public string ProductName { get; set; }

        [DisplayName("Precio Producto")]
        public int ProductPrice { get; set; }

        [DisplayName("Ordenes recibidas")]
        public int ReceivedOrders { get; set; }

        [DisplayName("Inventario Inicial")]
        public int InitialStock { get; set; }

        [DisplayName("Inventario de seguridad")]
        public int SecurityStock { get; set; }

        [DisplayName("Demanda de la Semana")]
        public int Demand { get; set; }

        [DisplayName("Ventas de la Semana")]
        public int Sells { get; set; }

        [DisplayName("Inventario Final")]
        public int FinalStock { get; set; }

        [DisplayName("Costo Inventario Final")]
        public int FinalStockCost { get; set; }

        [DisplayName("Demanda Insatisfecha")]
        public int UnsatisfiedDemand { get; set; }

        [DisplayName("Costo Demanda Insatisfecha")]
        public int UnsatisfiedDemandCost { get; set; }

        #region Input Fields
        [DisplayName("Producto")]
        public Guid ProductId { get; set; }
        
        [DisplayName("Cantidad a Ordenar")]
        public int Quantity { get; set; }

        [DisplayName("Costo Ordenar")]
        public int OrderCost { get; set; }

        [DisplayName("Método de Orden")]
        public SelectList OrderMethod
        {
            get
            {
                return new SelectList(new Dictionary<OrderType, string>() {
                    { OrderType.Normal, "Pedido Ordinario" },
                    { OrderType.Fast, "Rápido" },
                    { OrderType.Courrier, "Courier" },
                    { OrderType.FastCourier, "Compra Rápida + Uso de Courier" },
                }, "Key", "Value");
            }
            set { }
        }

        [DisplayName("Tipo de Orden")]
        public OrderType OrderMethodOption { get; set; }

        [DisplayName("Semana de entrega")]
        public int WeekDelivery { get; set; }
        #endregion 
    }
}