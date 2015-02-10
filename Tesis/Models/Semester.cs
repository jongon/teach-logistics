namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Semester
    {
        public Semester()
        {
            Sections = new HashSet<Section>();
        }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}
