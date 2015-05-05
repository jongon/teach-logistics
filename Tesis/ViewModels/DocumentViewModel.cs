using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tesis.ViewModels
{
    public class DocumentViewModel
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Una descripción para el documento es requerido")]
        public string Name { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "El documento es requerido")]
        public HttpPostedFileBase Document { get; set; }

        public string DocumentPath { get; set; }
    }
}