using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using Tesis.Models;
using Tesis.ViewModels;
using MvcFlash.Core.Extensions;
using Newtonsoft.Json;
using Tesis.Business;

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
            return View(await Db.Evaluations.ToListAsync());
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
                        QuestionIds = evaluation.Questions.Select(x => x.Id).ToList(),
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
                if (evaluation.Sections.Count() <= 0)
                {
                    evaluation.Name = evaluationViewModel.Name;
                    evaluation.Questions.Clear();
                    evaluation.Questions = await Db.Questions.Where(x => evaluationViewModel.QuestionIds.Contains(x.Id)).ToListAsync();
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok", "La evaluación ha sido editada con exito");
                    return RedirectToAction("Index");
                }
                else
                {
                    Flash.Error("Error", "Esta evaluación no puede ser modificada porque se encuentra asignada");
                    return View(evaluationViewModel);
                }
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
            List<Evaluation> evaluations = await Db.Users.Where(z => z.Id == CurrentUser.Id || z.EvaluationUsers.Select(c => c.UserId).Contains(CurrentUser.Id)).Select(x => x.Section).SelectMany(y => y.Evaluations).ToListAsync();
            EvaluationBL evaluationBL = new EvaluationBL();
            return View(evaluationBL.GetEvaluationStudent(evaluations, CurrentUser.Id));
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        //Falta
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
                SemesterDescription = x.User.Section.Semester.Description,
                SectionNumber = x.User.Section.Number
            }).ToList();
            EvaluationCalificationViewModel evaluationViewModel = new EvaluationCalificationViewModel
            {
                Id = evaluation.Id,
                Name = evaluation.Name,
                TotalScore = evaluation.Questions.Sum(x => x.Score),
                Califications = califications,
            };
            return View(evaluationViewModel);
        }

        [HttpGet]
        [Authorize]
        //Falta
        public async Task<ActionResult> ReviewQuiz(Guid Id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
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
                return View(evaluationBL.GetQuiz(evaluation));
            }
            Flash.Error("Error", "Ha ocurrido un error al intentar presentar la evaluación");
            return View("Evaluations");
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
                if (evaluationBL.UserCanBeEvaluated(evaluation, CurrentUser.Id))
                {
                    evaluationBL.TakeQuiz(evaluation, quiz, CurrentUser.Id);
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok", "El Quiz ha sido presentado exitosamente");
                    QuizViewModel reviewedQuiz = evaluationBL.ReviewQuiz(evaluation, CurrentUser.Id);
                    return View("ReviewQuiz", reviewedQuiz);
                }
                else
                {
                    Flash.Error("Error", "Ha ocurrido un error al intentar presentar la evaluación");
                    return View("Evaluations");
                }
            }
            catch (Exception e)
            {
                Flash.Error("Error", e.Message);
                return View("Evaluations");
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
