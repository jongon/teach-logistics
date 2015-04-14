using System;
using System.ComponentModel.DataAnnotations;

namespace Tesis.Models
{
    public class Evaluation
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}