using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeachLogisticsTest.ViewModels
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50, ErrorMessage = "No puede exceder los {1} caracteres.")]
        public string Name { get; set; }

        [DisplayName("Sección")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SectionId { get; set; }
        
        [DisplayName("Semestre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SemesterId { get; set; }

        [Required(ErrorMessage = "{0} es campo requerido")]
        [DisplayName("Integrantes")]
        public IEnumerable<string> Users { get; set; }

        [DisplayName("Buscar Integrantes")]
        public string UserAutoComplete { get; set; }
    }
}