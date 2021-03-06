using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeachLogistics.Models
{
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

        [DisplayName("Fecha de creaci�n")]
        public DateTime Created { get; set; }

        [DisplayName("N�mero de Per�odos")]
        public int Periods { get; set; }

        [DisplayName("Recargo por orden de compra")]
        [Required(ErrorMessage = "El campo recargo por orden de compra es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo n�mero entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de m�x. 4 d�gitos")]
        public short PurchaseOrderRecharge { get; set; }

        [DisplayName("Cargos de Courier")]
        [Required(ErrorMessage = "El campo costos de courier es requerido")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9][0-9]?)?", ErrorMessage = "Solo n�mero decirmal permitido, con Precisi�n de 2")]
        [Range(0, 10000, ErrorMessage = "Solo decimal positivo de m�x. 4 d�gitos")]
        public double CourierCharges { get; set; }

        [DisplayName("Costo de preparaci�n")]
        [Required(ErrorMessage = "El campo Costo Preparaci�n es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo n�mero entero positivo permitido")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de m�x. 4 d�gitos")]
        public int PreparationCost { get; set; }

        [DisplayName("Costo anual mantener")]
        [Required(ErrorMessage = "El campo Costo anual mantener es requerido")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Solo n�mero decirmal permitido, con Precisi�n de 2")]
        [Range(0, 10000, ErrorMessage = "Solo decimal positivo de m�x. 4 d�gitos")]
        public double AnnualMaintenanceCost { get; set; }

        [DisplayName("Secciones")]
        public virtual ICollection<Section> Sections { get; set; }

        [DisplayName("Cargas Iniciales")]
        [Required]
        public virtual ICollection<InitialCharge> InitialCharges { get; set; }
    }
}
