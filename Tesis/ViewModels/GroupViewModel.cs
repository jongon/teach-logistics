using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class GroupViewModel
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50, ErrorMessage = "No puede exceder los {1} caracteres.")]
        public string Name { get; set; }

        [DisplayName("Sección")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SectionId { get; set; }

        [DisplayName("Sección")]
        public virtual Section Section { get; set; }

        [Required(ErrorMessage = "{0} es campo requerido")]
        [DisplayName("Integrantes")]
        public ICollection<User> Users { get; set; }
    }
}