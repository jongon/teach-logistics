using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Period
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public bool IsLastPeriod { get; set; }

        public Section Section { get; set; }

        public Guid SectionId { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}