using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [DisplayName("Posici�n")]
        public string Position { get; set; }

        [DisplayName("Secci�n")]
        [Required]
        public Guid SectionId { get; set; }
        
        [DisplayName("Secci�n")]
        public virtual Section Section { get; set; }

        [DisplayName("Activo en Simulaci�n")]
        public virtual bool IsInSimulation { get; set; }

        [DisplayName("Integrantes")]
        [Required]
        public virtual ICollection<User> Users { get; set; }

        [DisplayName("Ordenes")]
        public virtual ICollection<Order> Orders { get; set; } 

        [DisplayName("Balances")]
        public virtual ICollection<Balance> Balances { get; set; }
    }
}
