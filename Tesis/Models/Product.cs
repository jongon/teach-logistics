namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public Product()
        {
            InitialCharges = new HashSet<InitialCharge>();
        }

        public Guid Id { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Number { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Name { get; set; }

        [DisplayName("Ciudad")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string City { get; set; }

        [DisplayName("Distancia")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Distance { get; set; }

        [DisplayName("Cargas Iniciales")]
        [Required(ErrorMessage = "Al menos una carga es necesaria")]
        public virtual ICollection<InitialCharge> InitialCharges { get; set; }
    }
}
