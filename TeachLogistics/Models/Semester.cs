using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TeachLogistics.Models
{
    [DisplayName("Semestre")]
    public partial class Semester
    {
        public Semester()
        {
            Sections = new HashSet<Section>();
        }

        public Guid Id { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Secciones")]
        public virtual ICollection<Section> Sections { get; set; }
    }
}
