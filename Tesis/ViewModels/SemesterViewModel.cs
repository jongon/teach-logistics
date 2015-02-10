using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tesis.ViewModels
{
    public class SemesterViewModel
    {

        public Guid Id { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(45, ErrorMessage = "Nombre no puede tener mas de {0} caracteres.")]
        public string Description { get; set; }
    }
}