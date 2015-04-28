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

namespace Tesis.Controllers
{
    public class EvaluationsController : BaseController
    {
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
                evaluation = validatedEvaluation as Evaluation;
            }
            catch (Exception)
            {
                return validatedEvaluation as ActionResult;
            }
            return View(evaluation);
        }

        // GET: Evaluations/Create
        [HttpGet]
        public ActionResult Create()
        {
            EvaluationViewModel evaluation = new EvaluationViewModel();
            ViewBag.Questions = evaluation.Questions;
            return View();
        }

        // POST: Evaluations/Create
        [HttpPost]
        public ActionResult Create(QuestionViewModel questionViewModel)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evaluations/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Evaluations/Edit/5
        [HttpPost]
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Evaluations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
