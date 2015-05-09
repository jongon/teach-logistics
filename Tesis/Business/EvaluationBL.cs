using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
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

        public bool UserCanBeEvaluated(Evaluation evaluation, string UserId)
        {
            EvaluationUser evaluationUser = evaluation.EvaluationUsers.Where(x => x.UserId == UserId).FirstOrDefault();
            if (evaluationUser == null)
            {
                User user = evaluation.Sections.Where(x => x.Users.Select(y => y.Id).Contains(UserId)).SelectMany(z => z.Users.Where(c => c.Id == UserId)).FirstOrDefault();
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetQuiz(Evaluation evaluation)
        {
            List<QuestionQuizViewModel> questionViewModel = new List<QuestionQuizViewModel>();
            foreach (var question in evaluation.Questions)
            {
                foreach (var option in question.Options)
                {
                    
                }
            }
            QuizViewModel quizViewModel = new QuizViewModel();
            return true;
        }
    }
}