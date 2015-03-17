namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

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
        [MaxLength(45)]
        public string Name { get; set; }

        [DisplayName("Fecha de creación")]
        public DateTime Created { get; set; }

        [DisplayName("Secciones")]
        public virtual ICollection<Section> Sections { get; set; }

        [DisplayName("Cargas Iniciales")]
        public virtual ICollection<InitialCharge> InitialCharges { get; set; }
    }
}
