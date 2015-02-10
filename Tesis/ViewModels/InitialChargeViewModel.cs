using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tesis.DAL;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class InitialChargeViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [DisplayName("Demanda")]    
        [Required(ErrorMessage = "El campo Demanda es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short Demand { get; set; }

        [DisplayName("Desviación Estandar")]
        [Required(ErrorMessage = "El campo Desviación Estandar es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short Stddev { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "El campo Precio es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99999, ErrorMessage = "Solo entero positivo de máx. 5 dígitos")]
        public int Price { get; set; }

        [DisplayName("Costo De Preparación/Transmitir")]
        [Required(ErrorMessage = "El campo Costo Preparación es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public int PreparationCost { get; set; }

        [DisplayName("Costo Anual de Mantener el Inventario")]
        [Required(ErrorMessage = "El campo Costo anual mantener es requerido")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Solo número decirmal permitido, con Precisión de 2")]
        [Range(0, 10000, ErrorMessage = "Solo decimal positivo de máx. 4 dígitos")]
        public double AnnualMaintenanceCost { get; set; }
        
        [DisplayName("Tiempo de Preparación")]
        [Required(ErrorMessage = "El campo Tiempo de preparación es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte PreparationTime { get; set; }
        
        [DisplayName("Tiempo de Surtir Pedido")]
        [Required(ErrorMessage = "El campo Tiempo de surtir pedido es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte FillTime { get; set; }

        [DisplayName("Tiempo de Entrega")]
        [Required(ErrorMessage = "El campo Tiempo de entrega es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte DeliveryTime { get; set; }

        [DisplayName("Inventario de Seguridad")]
        [Required(ErrorMessage = "El campo Inventario de Seguridad es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short SecurityStock { get; set; }

        [DisplayName("Inventario Inicial")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Solo números enteros permitidos")]
        public int InitialStock { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public Guid ProductId { get; set; }

        public SelectList Products
        {
            get
            {
                return new SelectList(db.Products.Select(x => new { x.Id, x.Name }), "Id", "Name");
            }
            set { }
        }
    }
}