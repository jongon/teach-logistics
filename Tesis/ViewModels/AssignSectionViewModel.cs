using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tesis.ViewModels
{
    public class AssignSectionViewModel
    {
        [DisplayName("Sección")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SectionId { get; set; }

        [DisplayName("Semestre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SemesterId { get; set; }

        [Required]
        public Guid CaseStudyId { get; set; }

        [DisplayName("Caso de Estudio")]
        public string CaseStudyName { get; set; }
    }
}