namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DisplayName("Caso de Estudio")]
    public partial class CaseStudy
    {
        public CaseStudy()
        {
            InitialCharges = new HashSet<InitialCharge>();
            Sections = new HashSet<Section>();
        }

        public Guid Id { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public virtual ICollection<InitialCharge> InitialCharges { get; set; }
    }
}
