using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tesis.ViewModels
{
    public class AsignSectionViewModel
    {
        [DisplayName("Sección")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SectionId { get; set; }

        [DisplayName("Semestre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SemesterId { get; set; }

        [Required]
        public Guid CaseStudyId { get; set; }
    }
}