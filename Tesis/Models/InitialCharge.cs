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
