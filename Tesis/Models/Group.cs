namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DisplayName("Grupo")]
    public partial class Group
    {
        public Group()
        {
            Users = new HashSet<User>();
        }
        public Guid Id { get; set; }

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
        public virtual ICollection<User> Users { get; set; }
    }
}
