using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tesis.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Número")]
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
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Solo números enteros permitidos")]
        public int Distance { get; set; }
    }
}