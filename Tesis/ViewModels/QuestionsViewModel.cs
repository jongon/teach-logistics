using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class QuestionViewModel
    {
        [DisplayName("Pregunta")]
        [Required(ErrorMessage = "La pregunta es requerida")]
        public string QuestionText { get; set; }

        [DisplayName("Respuesta Correcta")]
        [Required(ErrorMessage = "La opción correcta es requerida")]
        public string CorrectOption { get; set; }

        [DisplayName("Respuesta Incorrecta #1")]
        [Required(ErrorMessage = "La opción es requerida")]
        public string IncorrectOption1 { get; set; }

        [DisplayName("Respuesta Incorrecta #2")]
        [Required(ErrorMessage = "La opción es requerida")]
        public string IncorrectOption2 { get; set; }

        [DisplayName("Respuesta Incorrecta #3")]
        [Required(ErrorMessage = "La opción es requerida")]
        public string IncorrectOption3 { get; set; }

        [DisplayName("Puntaje")]
        [DataType(DataType.Text)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números enteros permitidos")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Solo números enteros permitidos")]
        [Required(ErrorMessage = "El puntaje de la pregunta es requerido")]
        public int Score { get; set; }

        [DisplayName("Imagen")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }
    }
}