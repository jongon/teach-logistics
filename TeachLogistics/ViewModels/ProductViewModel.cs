using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TeachLogisticsTest.UtilityCode;

namespace TeachLogisticsTest.ViewModels
{
    public class ProductViewModel
    {
        [DisplayName("Número")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        [Remote("ExistsProductNumber", "Products", ErrorMessage = "Número de producto ya registrado")]
        [UniqueProductNumber]
        public int Number { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50, ErrorMessage = "No puede exceder los {1} caracteres.")]
        [Remote("ExistsProductName", "Products", ErrorMessage = "Nombre de producto ya registrado")]
        [UniqueProductName]
        public string Name { get; set; }

        [DisplayName("Ciudad")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50, ErrorMessage = "No puede exceder los {1} caracteres.")]
        public string City { get; set; }

        [DisplayName("Distancia (Km's)")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Solo números enteros permitidos")]
        public int Distance { get; set; }
    }

    public class ProductViewModelEdit
    {
        public Guid Id { get; set; }

        [DisplayName("Número")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, 9999, ErrorMessage = "Solo entero positivo de máx. 4 dígitos")]
        public int Number { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50, ErrorMessage = "No puede exceder los {1} caracteres.")]
        public string Name { get; set; }

        [DisplayName("Ciudad")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50, ErrorMessage = "No puede exceder los {1} caracteres.")]
        public string City { get; set; }

        [DisplayName("Distancia (Km's)")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Solo números enteros permitidos")]
        public int Distance { get; set; }
    }
}