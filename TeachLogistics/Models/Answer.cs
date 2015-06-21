using System;
using System.ComponentModel.DataAnnotations;

namespace TeachLogistics.Models
{
    public class Answer
    {
        public Answer()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public virtual QuestionOption QuestionOption { get; set; }

        [Required]
        public virtual Guid QuestionOptionId { get; set; }

        [Required]
        public virtual EvaluationUser EvaluationUser { get; set; }

        [Required]
        public virtual Guid EvaluationUserId { get; set; }
    }
}