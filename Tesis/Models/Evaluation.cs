using System;
using System.ComponentModel.DataAnnotations;

namespace Tesis.Models
{
    public class Evaluation
    {
        public Evaluation()
        {

        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}