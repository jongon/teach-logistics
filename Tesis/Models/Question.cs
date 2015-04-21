using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Question
    {
        public Question()
        {

        }

        public Guid Id { get; set; }

        public string Question { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
    }
}