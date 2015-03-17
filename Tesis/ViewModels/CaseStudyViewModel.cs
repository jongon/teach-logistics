using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Tesis.DAL;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class CaseStudyViewModel
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

        [DisplayName("Costo De Preparación / Transmitir")]
        [Required(ErrorMessage = "El campo Costo Preparación es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public int PreparationCost { get; set; }

        [DisplayName("Costo Anual de Mantener el Inventario")]
        [Required(ErrorMessage = "El campo Costo anual mantener es requerido")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Solo número decirmal permitido, con Precisión de 2")]
        [Range(0, 10000, ErrorMessage = "Solo decimal positivo de máx. 4 dígitos")]
        public double AnnualMaintenanceCost { get; set; }

        [DisplayName("Costo semanal mantener")]
        [Required(ErrorMessage = "El campo Costo semanal mantener es requerido")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Solo número decirmal permitido, con Precisión de 2")]
        [Range(0, 10000, ErrorMessage = "Solo decimal positivo de máx. 4 dígitos")]
        public double WeeklyMaintenanceCost { get; set; }

        [DisplayName("Recargo por orden de compra")]
        [Required(ErrorMessage = "El campo recargo por orden de compra es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short PurchaseOrderRecharge { get; set; }

        [DisplayName("Cargos de Courier")]
        [Required(ErrorMessage = "El campo costos de courier es requerido")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Solo número decirmal permitido, con Precisión de 2")]
        [Range(0, 10000, ErrorMessage = "Solo decimal positivo de máx. 4 dígitos")]
        public double CourierCharges { get; set; }
        
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

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Nombre de Caso de Estudio")]
        [StringLength(45, ErrorMessage = "{0} no puede ser mayor a {1} caracteres")]
        public string Name { get; set; }

        [DisplayName("Sección")]
        public Guid SectionId { get; set; }

        [DisplayName("Semestre")]
        public Guid SemesterId { get; set; }

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

        [DisplayName("Tipo de carga")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de carga")]
        public string ChargeTypeName { get; set; }

        public SelectList ChargeTypes
        {
            get
            {
                return new SelectList(new Dictionary<string,string>() {
                    { "xml", "Archivo Xml"},
                    { "form", "Formulario"}
                }, "Key", "Value");
            }
            set { }
        }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "El Archivo Xml es requerido")]
        public HttpPostedFileBase XmlUpload { get; set; }
    }
}