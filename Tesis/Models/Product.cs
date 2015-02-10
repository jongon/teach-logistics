namespace Tesis.Models
{
    using System;
    using System.Collections.Generic;
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

        public int Number { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public int Distance { get; set; }

        [StringLength(128)]

        public virtual ICollection<InitialCharge> InitialCharges { get; set; }
    }
}
