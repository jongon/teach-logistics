using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tesis.Models
{
    public class Answer
    {
        public Answer()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public virtual QuestionOption QuestionOption { get; set; }

        public virtual Guid QuestionOptionId { get; set; }

        public virtual EvaluationUser EvaluationUser { get; set; }

        public virtual Guid EvaluationUserId { get; set; }
    }
}