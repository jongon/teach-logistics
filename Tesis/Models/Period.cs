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

        public byte PeriodNumber { get; set; }

        public bool isLastPeriod { get; set; }

        public Section Section { get; set; }

        public Guid SectiondId { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}