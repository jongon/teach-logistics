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

        [DisplayName("Recargo por orden de compra")]
        [Required(ErrorMessage = "El campo recargo por orden de compra es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short PurchaseOrderRecharge { get; set; }

        [DisplayName("Cargos de Courier")]
        [Required(ErrorMessage = "El campo costos de courier es requerido")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9][0-9]?)?", ErrorMessage = "Solo número decirmal permitido, con Precisión de 3")]
        [Range(0, 10000, ErrorMessage = "Solo decimal positivo de máx. 4 dígitos")]
        public double CourierCharges { get; set; }
        
        [DisplayName("Tiempo de Preparación")]
        [Required(ErrorMessage = "El campo Tiempo de preparación es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte PreparationTime { get; set; }

        [DisplayName("Tiempo de preparación acelerado")]
        [Required(ErrorMessage = "El campo Tiempo de preparación de acelerado es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte AcceleratedPreparationTime { get; set; }
        
        [DisplayName("Tiempo de Surtir Pedido")]
        [Required(ErrorMessage = "El campo Tiempo de surtir pedido es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte FillTime { get; set; }

        [DisplayName("Tiempo de surtir en existencia")]
        [Required(ErrorMessage = "El campo Tiempo de surtir en existencia pedido es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte ExistingFillTime { get; set; }

        [DisplayName("Tiempo de Entrega")]
        [Required(ErrorMessage = "El campo Tiempo de entrega es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte DeliveryTime { get; set; }

        [DisplayName("Tiempo de entrega con uso de courier")]
        [Required(ErrorMessage = "El campo Tiempo de entrega con uso de courier es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte CourierDeliveryTime { get; set; }

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

        [Required(ErrorMessage = "Número de Períodos es requerido")]
        [DisplayName("Número de Períodos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(1, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos mayor a 1")]
        public int Periods { get; set; }

        [DisplayName("Sección")]
        public Guid? SectionId { get; set; }

        [DisplayName("Semestre")]
        public Guid? SemesterId { get; set; }

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

        [Required(ErrorMessage = "El Tiempo de preparación es requerido")]
        [DisplayName("Tiempo de Preparación")]
        public byte PreparationTimeOption { get; set; }

        [Required(ErrorMessage = "El Tiempo de surtir es requerido")]
        [DisplayName("Tiempo de Surtir")]
        public byte FillTimeOption { get; set; }

        [Required(ErrorMessage = "El Tiempo de entrega es requerido")]
        [DisplayName("Tiempo de Entrega")]
        public byte DeliveryTimeOption { get; set; }

        public SelectList PreparationTimeRadio
        {
            get
            {
                return new SelectList(new Dictionary<string, string>() {
                    { "1", "Proceso Ordinario"},
                    { "0", "Proceso Acelerado"}
                }, "Key", "Value");
            }
            set { }
        }

        public SelectList FillTimeRadio
        {
            get
            {
                return new SelectList(new Dictionary<string, string>() {
                    { "1", "Con Elaboración"},
                    { "0", "En Existencia"}
                }, "Key", "Value");
            }
            set { }
        }

        public SelectList DeliveryTimeRadio
        {
            get
            {
                return new SelectList(new Dictionary<string, string>() {
                    { "1", "Entrega Ordinaria"},
                    { "0", "Con Uso de Courier"}
                }, "Key", "Value");
            }
            set { }
        }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "El Archivo Xml es requerido")]
        public HttpPostedFileBase XmlUpload { get; set; }

        [HiddenInput(DisplayValue=false)]
        public string InitialCharges { get; set; }
    }

    public class AssignSectionViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Nombre de Caso de estudio")]
        public string CaseStudyName { get; set; }

        public ICollection<Semester> Semesters { get; set; }

        public ICollection<Guid> Sections { get; set; }
    }
}