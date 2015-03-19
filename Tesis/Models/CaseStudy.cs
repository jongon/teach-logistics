namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    [DisplayName("Caso de Estudio")]
    public partial class CaseStudy
    {
        public CaseStudy()
        {
            InitialCharges = new HashSet<InitialCharge>();
            Sections = new HashSet<Section>();
        }

        public Guid Id { get; set; }

        [DisplayName("Nombre")]
        [MaxLength(45)]
        public string Name { get; set; }

        [DisplayName("Fecha de creación")]
        public DateTime Created { get; set; }

        [DisplayName("Tiempo de preparación")]
        [Required(ErrorMessage = "El campo Tiempo de preparación es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte PreparationTime { get; set; }

        [DisplayName("Tiempo de preparación acelerado")]
        [Required(ErrorMessage = "El campo Tiempo de preparación de acelerado es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte AcceleratedPreparationTime { get; set; }

        [DisplayName("Tiempo de surtir")]
        [Required(ErrorMessage = "El campo Tiempo de surtir pedido es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte FillTime { get; set; }

        [DisplayName("Tiempo de surtir en existencia")]
        [Required(ErrorMessage = "El campo Tiempo de surtir en existencia pedido es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte ExistingFillTime { get; set; }

        [DisplayName("Tiempo de entrega")]
        [Required(ErrorMessage = "El campo Tiempo de entrega es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte DeliveryTime { get; set; }

        [DisplayName("Tiempo de entrega con uso de courier")]
        [Required(ErrorMessage = "El campo Tiempo de entrega con uso de courier es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte CourierDeliveryTime { get; set; }

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

        [DisplayName("Costo de preparación")]
        [Required(ErrorMessage = "El campo Costo Preparación es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public int PreparationCost { get; set; }

        [DisplayName("Costo anual mantener")]
        [Required(ErrorMessage = "El campo Costo anual mantener es requerido")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Solo número decirmal permitido, con Precisión de 2")]
        [Range(0, 10000, ErrorMessage = "Solo decimal positivo de máx. 4 dígitos")]
        public double AnnualMaintenanceCost { get; set; }

        [DisplayName("Secciones")]
        public virtual ICollection<Section> Sections { get; set; }

        [DisplayName("Cargas Iniciales")]
        [Required]
        public virtual ICollection<InitialCharge> InitialCharges { get; set; }
    }
}
