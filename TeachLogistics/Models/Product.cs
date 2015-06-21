using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TeachLogistics.Models
{

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

        [DisplayName("Demandas")]
        public virtual ICollection<Demand> Demands { get; set; }

        [DisplayName("Ordenes")]
        public virtual ICollection<Order> Orders { get; set; }

        [DisplayName("Balances")]
        public virtual ICollection<Balance> Balances { get; set; }
    }
}
