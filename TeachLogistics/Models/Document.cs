using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeachLogisticsTest.Models
{
    public class Document
    {
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Ruta")]
        public string Path { get; set; }
    }
}