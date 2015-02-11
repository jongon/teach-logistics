namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group
    {
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Puntaje")]
        public string Score { get; set; }

        [DisplayName("Posici�n")]
        public string Position { get; set; }

        [DisplayName("Secci�n")]
        public Guid SectionId { get; set; }
        
        [DisplayName("Secci�n")]
        public virtual Section Section { get; set; }

        [DisplayName("Integrantes")]
        public ICollection<User> Users { get; set; }
    }
}
