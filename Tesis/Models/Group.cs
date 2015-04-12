using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tesis.Models
{
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

        [DisplayName("Posición")]
        public string Position { get; set; }

        [DisplayName("Sección")]
        public Guid SectionId { get; set; }
        
        [DisplayName("Sección")]
        public virtual Section Section { get; set; }

        [DisplayName("Integrantes")]
        public virtual ICollection<User> Users { get; set; }
    }
}
