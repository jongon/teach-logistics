using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tesis.DAL;
using Tesis.Models;

namespace Tesis.ViewModels
{
    public class EvaluationViewModel
    {
        private ApplicationDbContext db; 

        public EvaluationViewModel()
        {
            this.db = new ApplicationDbContext();
        }

        [DisplayName("Evaluación")]
        [Required(ErrorMessage = "El nombre de la evaluación es requerido")]
        public string Name { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime Created { get; set; }

        [DisplayName("Preguntas")]
        [UIHint("Questions")]
        public virtual ICollection<Question> Questions
        {
            get
            {
                return db.Questions.ToList();
            }
            set { }
        }

        [DisplayName("Identificadores de Preguntas")]
        public virtual ICollection<Guid> QuestionIds { get; set; }
    }

    public class AssignEvaluationViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Nombre de Evaluación")]
        public string EvaluationName { get; set; }

        public ICollection<Semester> Semesters { get; set; }

        public ICollection<Guid> Sections { get; set; }
    }
}