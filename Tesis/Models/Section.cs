
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tesis.Models
{
    [DisplayName("Sección")]
    public partial class Section
    {
        public Section()
        {
            Groups = new HashSet<Group>();
            Users = new HashSet<User>();
            IsActivedSimulation = false;
        }

        public Guid Id { get; set; }
        
        [DisplayName("Número")]
        public string Number { get; set; }

        public bool IsActivedSimulation { get; set; }

        public Guid SemesterId { get; set; }

        public Guid? CaseStudyId { get; set; }
        
        [DisplayName("Caso de Estudio")]
        public virtual CaseStudy CaseStudy { get; set; }

        [DisplayName("Grupos")]
        public virtual ICollection<Group> Groups { get; set; }

        [DisplayName("Semestre")]
        public virtual Semester Semester { get; set; }

        [DisplayName("Integrantes")]
        public virtual ICollection<User> Users { get; set; }

        [DisplayName("Evaluaciones")]
        public virtual ICollection<Evaluation> Evaluations { get; set; }

        [DisplayName("Periodos")]
        public virtual ICollection<Period> Periods { get; set; }
    }
}
