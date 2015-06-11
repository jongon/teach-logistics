using MvcFlash.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tesis.Business;
using Tesis.Models;
using Tesis.ViewModels;

namespace Tesis.Controllers
{
    [Authorize]
    public class EvaluationsController : BaseController
    {
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> AssignSection(Guid? id)
        {
            Object validatedEvaluation = await this.ValidateEvaluation(id);
            try
            {
                Evaluation evaluation = (Evaluation)validatedEvaluation;
                AssignEvaluationViewModel evaluationViewModel = new AssignEvaluationViewModel
                {
                    Id = evaluation.Id,
                    EvaluationName = evaluation.Name,
                    Semesters = Db.Semesters.ToList(),
                };
                var selectedSections = evaluation.Sections.Select(x => x.Id.ToString()).ToList();
                ViewBag.SelectedSections = selectedSections;
                return View(evaluationViewModel);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (ActionResult)validatedEvaluation;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> AssignSection([Bind(Exclude="EvaluationName,Semesters")]AssignEvaluationViewModel evaluationModel)
        {
            try
            {
                //Evaluación a Asignarle sección
                var evaluation = await Db.Evaluations.Where(x => x.Id == evaluationModel.Id).FirstOrDefaultAsync();
                List<Section> sections;
                if (evaluationModel.Sections != null)
                {
                    //Secciones por ser asignadas
                    sections = await Db.Sections.Where(x => evaluationModel.Sections.Contains(x.Id)).ToListAsync();
                }
                else
                {
                    sections = null;
                }

                if (evaluation != null)
                {
                    evaluation.Sections.Clear();
                    evaluation.Sections = sections;
                }
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "Las Asignaciones han sido exitosas");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Flash.Error("Error", "La Asignación no ha sido posible");
                return View(evaluationModel);
            }
        }

        // GET: Evaluations
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Index()
        {
            List<Evaluation> evaluations = await Db.Evaluations.ToListAsync();
            return View(evaluations);
        }

        // GET: Evaluations/Details/5
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Details(Guid? id)
        {
            Object validatedEvaluation = await this.ValidateEvaluation(id);
            Evaluation evaluation;
            try
            {
                evaluation = (Evaluation)validatedEvaluation;
            }
            catch (Exception)
            {
                return (ActionResult)validatedEvaluation;
            }
            return View(evaluation);
        }

        // GET: Evaluations/Create
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View(new EvaluationViewModel());
        }

        // POST: Evaluations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Create([Bind(Exclude="Questions")]EvaluationViewModel evaluationViewModel)
        {
            try
            {
                if (ModelState.IsValid && evaluationViewModel.QuestionIds.Count() > 0)
                {
                    Evaluation evaluation = new Evaluation
                    {
                        MinutesDuration = evaluationViewModel.MinutesDuration,
                        LimitDate = evaluationViewModel.LimitDate,
                        Name = evaluationViewModel.Name,
                        Created = DateTime.Now,
                        Questions = Db.Questions.Where(x => evaluationViewModel.QuestionIds.ToList().Contains(x.Id)).ToList(),
                    };
                    Db.Evaluations.Add(evaluation);
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok", "La Evaluación ha sido creada exitosamente");
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {

            } 
            Flash.Error("Error", "No se Ha Podido guardar la Evaluación");
            return View(evaluationViewModel);
        }

        // GET: Evaluations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            Object validatedEvaluation = await this.ValidateEvaluation(id);
            try
            {
                Evaluation evaluation = (Evaluation)validatedEvaluation;
                if (evaluation.Sections.Count() <= 0)
                {
                    EvaluationViewModel evaluationViewModel = new EvaluationViewModel
                    {
                        Name = evaluation.Name,
                        MinutesDuration = evaluation.MinutesDuration,
                        QuestionIds = evaluation.Questions.Select(x => x.Id).ToList(),
                        LimitDate = evaluation.LimitDate
                    };
                    return View(evaluationViewModel);
                }
                else
                {
                    Flash.Error("Error", "Esta evaluación no puede ser modificada porque se encuentra asignada");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (ActionResult)validatedEvaluation;
            }
        }

        // POST: Evaluations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit([Bind(Exclude = "Questions")]EvaluationViewModel evaluationViewModel)
        {
            Object validatedEvaluation = await this.ValidateEvaluation(evaluationViewModel.Id);
            try
            {
                Evaluation evaluation = (Evaluation)validatedEvaluation;
                if (ModelState.IsValid)
                {
                    if (evaluation.Sections.Count() <= 0)
                    {
                        evaluation.Name = evaluationViewModel.Name;
                        evaluation.LimitDate = evaluationViewModel.LimitDate;
                        evaluation.Questions.Clear();
                        evaluation.Questions = await Db.Questions.Where(x => evaluationViewModel.QuestionIds.Contains(x.Id)).ToListAsync();
                        await Db.SaveChangesAsync();
                        Flash.Success("Ok", "La evaluación ha sido editada con exito");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Flash.Error("Error", "Esta evaluación no puede ser modificada porque se encuentra asignada");
                    }
                }
                else
                {
                    Flash.Error("Error", "Ha ocurrido un error editando la evaluación");
                }
                return View(evaluationViewModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (ActionResult)validatedEvaluation;
            }
        }

        // GET: Evaluations/Delete/5
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            Object validatedEvaluation = await this.ValidateEvaluation(id);
            Evaluation evaluation;
            try
            {
                evaluation = (Evaluation)validatedEvaluation;
            }
            catch (Exception)
            {
                return (ActionResult)validatedEvaluation;
            }
            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteConfirmed(Guid? id)
        {
            Object validatedEvaluation = await this.ValidateEvaluation(id);
            try
            {
                Evaluation evaluation = (Evaluation)validatedEvaluation;
                Db.Evaluations.Remove(evaluation);
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "La evaluación ha sido eliminada con exito");
                return RedirectToAction("Index");
            }
            catch
            {
                return (ActionResult)validatedEvaluation;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Estudiante")]
        public async Task<ActionResult> Evaluations()
        {
            var evaluationQuery = Db.Users.Where(z => z.Id == CurrentUser.Id || z.EvaluationUsers.Select(c => c.UserId).Contains(CurrentUser.Id)).Select(x => x.Section).SelectMany(y => y.Evaluations);
            var evaluations = await evaluationQuery.Where(x => x.EvaluationUsers.Select(t => t.Active).Contains(true) || x.LimitDate >= DateTime.Today).ToListAsync();
            EvaluationBL evaluationBL = new EvaluationBL();
            return View(evaluationBL.GetEvaluationStudent(evaluations, CurrentUser.Id));
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Califications(Guid? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            Evaluation evaluation = await Db.Evaluations.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            List<CalificationVieWModel> califications = evaluation.EvaluationUsers.Select(x => new CalificationVieWModel { 
                UserId = x.UserId,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                GotScore = x.Calification,
                TotalScore = evaluation.Questions.Sum(c => c.Score),
                SemesterDescription = x.User.Section.Semester.Description,
                SectionNumber = x.User.Section.Number,
                EvaluationId = evaluation.Id
            }).ToList();
            EvaluationCalificationViewModel evaluationViewModel = new EvaluationCalificationViewModel
            {
                Id = evaluation.Id,
                Name = evaluation.Name,
                TotalScore = evaluation.Questions.Sum(x => x.Score),
                Califications = califications
            };
            return View(evaluationViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> ReviewQuiz(string userId, Guid evaluationId)
        {
            if (evaluationId == null || String.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = await Db.Evaluations.Where(x => x.Id == evaluationId).FirstOrDefaultAsync<Evaluation>();
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            EvaluationBL evaluationBL = new EvaluationBL();
            QuizViewModel reviewedQuiz = evaluationBL.ReviewQuiz(evaluation, userId, true);
            return View(reviewedQuiz);
        }

        [HttpGet]
        [Authorize(Roles = "Estudiante")]
        [ActionName("ReviewQuizStudent")]
        public async Task<ActionResult> ReviewQuiz(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = await Db.Evaluations.Where(x => x.Id == Id).FirstOrDefaultAsync<Evaluation>();
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            EvaluationBL evaluationBL = new EvaluationBL();
            QuizViewModel reviewedQuiz = evaluationBL.ReviewQuiz(evaluation, CurrentUser.Id);
            return View("ReviewQuiz", reviewedQuiz);
        }

        [HttpPost]
        [ActionName("TakeQuizFirst")]
        [Authorize(Roles = "Estudiante")]
        public async Task<ActionResult> TakeQuiz(Guid? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = await Db.Evaluations.Where(x => x.Id == Id).FirstOrDefaultAsync<Evaluation>();
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            EvaluationBL evaluationBL = new EvaluationBL();
            if (evaluationBL.UserCanBeEvaluated(evaluation, CurrentUser.Id))
            {
                DateTime dateTime = evaluation.EvaluationUsers.Where(x => x.UserId == CurrentUser.Id).FirstOrDefault().TakenDate;
                await Db.SaveChangesAsync();
                return View("TakeQuiz", evaluationBL.GetQuiz(evaluation, dateTime));
            }
            await Db.SaveChangesAsync();
            Flash.Error("Error", "Esta evaluación ya ha sido presentada o Ha ocurrido un error al intentar presentar la evaluación");
            return RedirectToAction("Evaluations");
        }

        [HttpPost]
        [Authorize(Roles = "Estudiante")]
        public async Task<ActionResult> TakeQuiz(QuizViewModel quiz)
        {
            Evaluation evaluation = await Db.Evaluations.Where(x => x.Id == quiz.Id).FirstOrDefaultAsync();
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            EvaluationBL evaluationBL = new EvaluationBL();
            try
            {
                List<Answer> answers = null;
                if (quiz.RunoutTime)
                {
                    quiz.Questions = quiz.Questions.Where(x => x.Options != null).ToList();
                    answers = quiz.Questions.Select(x => new Answer { QuestionOptionId = x.Options.SelectedAnswer }).ToList();
                }

                if (ModelState.IsValid)
                {
                    if (evaluationBL.UserCanBeEvaluated(evaluation, CurrentUser.Id, answers))
                    {
                        evaluationBL.TakeQuiz(evaluation, quiz, CurrentUser.Id);
                        await Db.SaveChangesAsync();
                        Flash.Success("Ok", "El Quiz ha sido presentado exitosamente");
                        QuizViewModel reviewedQuiz = evaluationBL.ReviewQuiz(evaluation, CurrentUser.Id);
                        return View("ReviewQuiz", reviewedQuiz);
                    }
                    else
                    {
                        await Db.SaveChangesAsync();
                        Flash.Error("Error", "Ha caducado el tiempo para presentar el quiz");
                        return RedirectToAction("Evaluations");
                    }
                }
                else
                {
                    Flash.Error("Error", "Ha ocurrido un error al intentar presentar la evaluación");
                    return RedirectToAction("Evaluations");
                }
            }
            catch (Exception)
            {
                Flash.Error("Error", "Ha ocurrido un error al intentar presentar la evaluación");
                return RedirectToAction("Evaluations");
            }
        }

        private async Task<Object> ValidateEvaluation(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = await Db.Evaluations.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return evaluation;
        }
    }
}
