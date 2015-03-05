namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DisplayName("Producto")]
    public partial class Product
    {
        public Product()
        {
            InitialCharges = new HashSet<InitialCharge>();
        }

        public Guid Id { get; set; }

        [DisplayName("Número")]
        public int Number { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Ciudad")]
        public string City { get; set; }

        [DisplayName("Distancia")]
        public int Distance { get; set; }

        [DisplayName("Cargas Iniciales")]
        public virtual ICollection<InitialCharge> InitialCharges { get; set; }
    }
}
