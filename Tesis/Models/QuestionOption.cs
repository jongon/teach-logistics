using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tesis.Models
{
    public class QuestionOption
    {
        public QuestionOption()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public bool IsCorrectOption { get; set; }

        public string Option { get; set; }

        [Required]
        public virtual Question Question { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}