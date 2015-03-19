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
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo n�meros enteros permitidos")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de m�x. 4 d�gitos")]
        public short Demand { get; set; }

        [DisplayName("Desviaci�n estandar")]
        [Required(ErrorMessage = "El campo Desviaci�n Estandar es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo n�mero entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de m�x. 4 d�gitos")]
        public short Stddev { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "El campo Precio es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo n�mero entero positivo permitido")]
        [Range(0, 99999, ErrorMessage = "Solo entero positivo de m�x. 5 d�gitos")]
        public int Price { get; set; }

        [DisplayName("Lote de reposici�n")]
        public double ReplacementBatch { get; set; }

        [DisplayName("Lote de reposici�n m�nimo")]
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
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo n�mero entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de m�x. 4 d�gitos")]
        public short SecurityStock { get; set; }

        [DisplayName("Coeficiente de variaci�n")]
        public double VariationCoefficient { get; set; }

        [DisplayName("Tiempo del ciclo")]
        public double CycleTime { get; set; }

        [DisplayName("Inventario promedio")]
        public double AverageStock { get; set; }

        [DisplayName("EOQ")]
        public double EOQ { get; set; }

        [DisplayName("Inventario Inicial")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo n�meros enteros permitidos")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Solo n�meros enteros permitidos")]
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
