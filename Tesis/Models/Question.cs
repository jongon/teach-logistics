using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Question
    {
        public Guid Id { get; set; }

        [DisplayName("Pregunta")]
        [Required]
        public string QuestionText { get; set; }

        [DisplayName("Imagen")]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        [DisplayName("Puntaje")]
        [DataType(DataType.Text)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Solo números enteros permitidos")]
        [Required(ErrorMessage = "El puntaje de la pregunta es requerido")]
        public int Score { get; set; }

        [DisplayName("Evaluaciones")]
        public virtual ICollection<Evaluation> Evaluations { get; set; }

        [DisplayName("Opciones")]
        [Required]
        public virtual ICollection<QuestionOption> Options { get; set; }
    }
}