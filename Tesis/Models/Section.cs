namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Section
    {
        public Section()
        {
            Groups = new HashSet<Group>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        
        [DisplayName("Número")]
        public string Number { get; set; }

        public Guid SemesterId { get; set; }

        public Guid? CaseStudyId { get; set; }

        public CaseStudy CaseStudy { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
