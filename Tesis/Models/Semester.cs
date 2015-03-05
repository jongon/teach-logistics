namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DisplayName("Semestre")]
    public partial class Semester
    {
        public Semester()
        {
            Sections = new HashSet<Section>();
        }

        public Guid Id { get; set; }

        [DisplayName("Descripci�n")]
        public string Description { get; set; }

        [DisplayName("Secciones")]
        public virtual ICollection<Section> Sections { get; set; }
    }
}
