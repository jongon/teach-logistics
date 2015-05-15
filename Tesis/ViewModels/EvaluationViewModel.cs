using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public Guid? Id { get; set; }

        [DisplayName("Evaluación")]
        [Required(ErrorMessage = "El nombre de la evaluación es requerido")]
        public string Name { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime Created { get; set; }

        [DisplayName("Fecha Límite")]
        [Required(ErrorMessage = "Fecha límite de examen es requerido")]
        public DateTime LimitDate { get; set; }

        [DisplayName("Duración")]
        [Required(ErrorMessage = "Duración del examen es requerido")]
        [Range(0, 999, ErrorMessage = "Solo entero positivo de máx. 3 dígitos")]
        public int MinutesDuration { get; set; }

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

    public class EvaluationStudentViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Nombre de la Evaluación")]
        public string QuizName { get; set; }

        [DisplayName("Puntaje Obtenido")]
        public int GotScore { get; set; }

        [DisplayName("Puntaje Total")]
        public int TotalScore { get; set; }

        [DisplayName("N° de Preguntas")]
        public int QuestionNumbers { get; set; }

        [DisplayName("Fecha Límite")]
        public DateTime LimitDate { get; set; }

        public bool IsTaken { get; set; }

        [DisplayName("Presentado")]
        public DateTime TakenDate { get; set; }

        [DisplayName("Duración")]
        public int MinutesDuration { get; set; }
    }

    public class QuizViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Usuario")]
        public User User { get; set; }

        [DisplayName("Nombre del Quiz")]
        public string QuizName { get; set; }

        [UIHint("QuestionsQuiz")]
        public ICollection<QuestionQuizViewModel> Questions { get; set; }

        [DisplayName("Puntaje Total")]
        public int Score { get; set; }

        [DisplayName("Puntaje Obtenido")]
        public int GotScore { get; set; }

        [DisplayName("Duración")]
        public DateTime EndTime { get; set; }

        [DisplayName("Sin Tiempo")]
        public bool RunoutTime { get; set; }
    }

    public class QuestionQuizViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Imagen")]
        public string ImagePath { get; set; }

        [DisplayName("Pregunta")]
        public string QuestionText { get; set; }

        [DisplayName("Puntaje")]
        public int QuestionScore { get; set; }

        [DisplayName("Opciones")]
        [UIHint("OptionQuiz")]
        public OptionQuizViewModel Options { get; set; }
    }

    public class OptionQuizViewModel
    {
        [DisplayName("Respuesta Seleccionada")]
        [Required(ErrorMessage = "Debe seleccionar una respuesta")]
        public Guid SelectedAnswer { get; set; }

        [DisplayName("Respuesta Correcta")]
        public Guid CorrectAnswer { get; set; }

        [DisplayName("Opciones")]
        public SelectList Options { get; set; }
    }

    public class EvaluationCalificationViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Puntaje Total")]
        public int TotalScore { get; set; }

        [UIHint("Califications")]
        public virtual ICollection<CalificationVieWModel> Califications { get; set; }
    }

    public class CalificationVieWModel
    {
        [DisplayName("EvaluationId")]
        public Guid EvaluationId { get; set; }

        [DisplayName("UserId")]
        public string UserId { get; set; }

        [DisplayName("Nombre")]
        public string FirstName { get; set; }

        [DisplayName("Apellido")]
        public string LastName { get; set; }

        [DisplayName("Semestre")]
        public string SemesterDescription { get; set; }

        [DisplayName("Sección")]
        public string SectionNumber { get; set; }

        [DisplayName("Puntaje")]
        public int GotScore { get; set; }

        [DisplayName("Puntaje Total")]
        public int TotalScore { get; set; }
    }
}