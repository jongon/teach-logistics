using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tesis.Models
{
    [DisplayName("Evaluación")]
    public class Evaluation
    {
        public Evaluation()
        {
            this.Id = Guid.NewGuid();
            Questions = new HashSet<Question>();
        }

        public Guid Id { get; set; }

        [DisplayName("Evaluación")]
        [Required(ErrorMessage = "El nombre de la evaluación es requerido")]
        public string Name { get; set; }
   
        [DisplayName("Fecha de Creación")]
        public DateTime Created { get; set; }

        [DisplayName("Fecha Límite")]
        [Required(ErrorMessage = "Fecha límite de examen es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LimitDate { get; set; }

        [DisplayName("Minutos de Duración")]
        [Required(ErrorMessage = "Duración del examen es requerido")]
        public int MinutesDuration { get; set; }

        [DisplayName("Preguntas")]
        [UIHint("Questions")]
        public virtual ICollection<Question> Questions { get; set; }

        [DisplayName("Secciones")]
        public virtual ICollection<Section> Sections { get; set; }

        [DisplayName("Usuarios")]
        public virtual ICollection<EvaluationUser> EvaluationUsers { get; set; }
    }
}