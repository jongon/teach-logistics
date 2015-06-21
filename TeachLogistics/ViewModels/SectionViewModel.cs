using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using TeachLogistics.DAL;

namespace TeachLogistics.ViewModels
{
    public class SectionViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Número")]
        public string Number { get; set; }

        [DisplayName("Semestre")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public Guid SemesterId { get; set; }

        public SelectList Semesters
        {
            get
            {
                return new SelectList(db.Semesters.Select(x => new { x.Id, x.Description }), "Id", "Description");
            }
            set { }
        }
    }
}