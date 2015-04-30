using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using Tesis.Models;
using Tesis.ViewModels;
using MvcFlash.Core.Extensions;

namespace Tesis.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EvaluationsController : BaseController
    {
        [HttpGet]
        public ActionResult AssignSection()
        {
            return View();
        }

        // GET: Evaluations
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await Db.Evaluations.ToListAsync());
        }

        // GET: Evaluations/Details/5
        [HttpGet]
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
        public ActionResult Create()
        {
            return View(new EvaluationViewModel());
        }

        // POST: Evaluations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Evaluations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evaluations/Delete/5
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
