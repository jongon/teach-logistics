using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis.Models;
using Tesis.ViewModels;

namespace Tesis.Business
{
    public class EvaluationBL
    {
        public ICollection<EvaluationStudentViewModel> GetEvaluationStudent(ICollection<Evaluation> evaluations, string userId) {
            List<EvaluationStudentViewModel> evaluationStudentList = new List<EvaluationStudentViewModel>();
            foreach (var evaluation in evaluations)
            {
                EvaluationStudentViewModel evaluationStudentViewModel = new EvaluationStudentViewModel
                {
                    Id = evaluation.Id,
                    QuestionNumbers = evaluation.Questions.Count(),
                    QuizName = evaluation.Name,
                    TotalScore = evaluation.Questions.Sum(c => c.Score)
                };
                EvaluationUser evaluationUser = evaluation.EvaluationUsers.Where(x => x.UserId == userId).FirstOrDefault();
                if (evaluationUser != null)
                {
                    evaluationStudentViewModel.GotScore = evaluationUser.Calification;
                    evaluationStudentViewModel.IsTaken = evaluationUser.Active;
                    evaluationStudentViewModel.TakenDate = evaluationUser.TakenDate;
                }
                evaluationStudentList.Add(evaluationStudentViewModel);
            }
            return evaluationStudentList;
        }
    }
}