using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class EvaluationUser
    {
        public Guid Id { get; set; }

        [DisplayName("Activo")]
        public virtual bool Active { get; set; }

        public virtual Guid EvaluationId { get; set; }

        [DisplayName("Evaluación")]
        public virtual Evaluation Evaluation { get; set; }

        public virtual string UserId { get; set; }

        [DisplayName("Estudiante")]
        public virtual User User { get; set; }
    }
}