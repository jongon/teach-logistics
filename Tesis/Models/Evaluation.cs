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
   
        [DisplayName("Creación")]
        public DateTime Created { get; set; }

        [DisplayName("Límite")]
        [Required(ErrorMessage = "Fecha límite de examen es requerido")]
        public DateTime LimitDate { get; set; }

        [DisplayName("Duración")]
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