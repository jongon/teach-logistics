namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DisplayName("Carga Inicial")]
    public partial class InitialCharge
    {
        public InitialCharge()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [DisplayName("Demanda")]
        [Required(ErrorMessage = "El campo Demanda es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short Demand { get; set; }

        [DisplayName("Desviación estandar")]
        [Required(ErrorMessage = "El campo Desviación Estandar es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short Stddev { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "El campo Precio es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99999, ErrorMessage = "Solo entero positivo de máx. 5 dígitos")]
        public int Price { get; set; }

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

        [DisplayName("Tiempo de preparación")]
        [Required(ErrorMessage = "El campo Tiempo de preparación es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte PreparationTime { get; set; }

        [DisplayName("Tiempo de surtir")]
        [Required(ErrorMessage = "El campo Tiempo de surtir pedido es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte FillTime { get; set; }

        [DisplayName("Tiempo de entrega")]
        [Required(ErrorMessage = "El campo Tiempo de entrega es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 99, ErrorMessage = "Solo entero positivo de máx. 2 dígitos")]
        public byte DeliveryTime { get; set; }

        [DisplayName("Tiempo total")]
        [Required(ErrorMessage = "El campo Inventario de Seguridad es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short TotalTime { get; set; }

        [DisplayName("Lote de reposición")]
        public double ReplacementBatch { get; set; }

        [DisplayName("Lote de reposición mínimo")]
        public double MinimunBatchReplacement { get; set; }

        [DisplayName("Costo trimestral de producto")]
        public int ProductQuaterlyCost { get; set; }

        [DisplayName("Costo ")]
        public double RequestQuaterlyCost { get; set; }

        [DisplayName("Coso trimestral mantener")]
        public double MaintenanceQuaterlyCost { get; set; }

        [DisplayName("Costo trimestral total")]
        public double TotalQuaterlyCost { get; set; }

        [DisplayName("Inventario de seguridad")]
        [Required(ErrorMessage = "El campo Inventario de Seguridad es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo número entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public short SecurityStock { get; set; }

        [DisplayName("Coeficiente de variación")]
        public double VariationCoefficient { get; set; }

        [DisplayName("Tiempo del ciclo")]
        public double CycleTime { get; set; }

        [DisplayName("Inventario promedio")]
        public double AverageStock { get; set; }

        [DisplayName("EOQ")]
        public double EOQ { get; set; }

        [DisplayName("Inventario Inicial")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Solo números enteros permitidos")]
        public int InitialStock { get; set; }

        [DisplayName("Caso de Estudio")]
        public virtual CaseStudy CaseStudy { get; set; }

        [DisplayName("Producto")]
        public virtual Product Product { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public Guid ProductId { get; set; }

        [DisplayName("Caso de Estudio")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public Guid CaseStudyId { get; set; }

    }
}
