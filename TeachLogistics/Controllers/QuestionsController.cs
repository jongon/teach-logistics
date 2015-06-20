using MvcFlash.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TeachLogisticsTest.Models;
using TeachLogisticsTest.ViewModels;

namespace TeachLogisticsTest.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class QuestionsController : BaseController
    {
        // GET: Questions
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await Db.Questions.ToListAsync());
        }

        // GET: Questions/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await Db.Questions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Exclude="ImagePath")]QuestionViewModel questionViewModel)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (questionViewModel.Image != null && !validImageTypes.Contains(questionViewModel.Image.ContentType))
            {
                ModelState.AddModelError("Image", "Los formatos permitidos son GIF, JPG o PNG.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var imageUrl = "";
                    if (questionViewModel.Image != null && questionViewModel.Image.ContentLength > 0)
                    {
                        var uploadDir = "~/Content/app_images/";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), questionViewModel.Image.FileName);
                        imageUrl = Path.Combine(uploadDir, questionViewModel.Image.FileName);
                        questionViewModel.Image.SaveAs(imagePath);
                    }
                    QuestionOption[] questionOptions = new QuestionOption[]{
                        new QuestionOption {
                            IsCorrectOption = true,
                            Option = questionViewModel.CorrectOption,
                        },
                        new QuestionOption {
                            IsCorrectOption = false,
                            Option = questionViewModel.IncorrectOption1
                        },
                        new QuestionOption {
                            IsCorrectOption = false,
                            Option = questionViewModel.IncorrectOption2
                        },
                        new QuestionOption {
                            IsCorrectOption = false,
                            Option = questionViewModel.IncorrectOption3
                        }
                    };
                    Question question = new Question
                    {
                        Id = Guid.NewGuid(),
                        QuestionText = questionViewModel.QuestionText,
                        Score = questionViewModel.Score,
                        Options = new List<QuestionOption>(questionOptions),
                        ImagePath = imageUrl,
                    };
                    // TODO: Add insert logic here
                    Db.Questions.Add(question);
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok", "La Pregunta ha sido guardada exitosamente");
                    return RedirectToAction("Index");
                }
                return View(questionViewModel);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Flash.Error("Error", "Un error inesperado ha ocurrido");
                return View(questionViewModel);
            }
        }

        // GET: Questions/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await Db.Questions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (question == null)
            {
                return HttpNotFound();
            }

            if (question.Evaluations.Count() <= 0)
            {
                List<QuestionOption> options = await Db.QuestionOptions.Where(x => x.QuestionId == id).ToListAsync();
                List<QuestionOption> incorrectOptions = options.Where(x => x.IsCorrectOption == false && x.QuestionId == id).Take(3).ToList();
                QuestionOption correctOption = options.Where(x => x.IsCorrectOption == true).FirstOrDefault();

                QuestionViewModel questionViewModel = new QuestionViewModel
                {
                    QuestionText = question.QuestionText,
                    CorrectOption = correctOption.Option,
                    IncorrectOption1 = incorrectOptions[0].Option,
                    IncorrectOption2 = incorrectOptions[1].Option,
                    IncorrectOption3 = incorrectOptions[2].Option,
                    ImagePath = question.ImagePath,
                    Score = question.Score,
                };
                ViewBag.Id = id;
                return View(questionViewModel);
            }
            Flash.Error("Error", "La pregunta está presente en una evaluación y no se puede editar");
            return RedirectToAction("Index");
        }

        // POST: Questions/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid? id,[Bind(Exclude = "ImagePath")]QuestionViewModel questionViewModel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await Db.Questions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (question == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (question.Evaluations.Count() > 0)
                {
                    Flash.Error("Error", "No se puede editar la pregunta");
                    return RedirectToAction("Index");
                }
                // TODO: Add update logic here
                //Eliminar imagen si ha sido modificada la imagen
                if (questionViewModel.Image != null && question.ImagePath != "")
                {
                    string fullPath = Request.MapPath(question.ImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                //Guardar la imagen
                var imageUrl = "";
                if (questionViewModel.Image != null && questionViewModel.Image.ContentLength > 0)
                {
                    var uploadDir = "~/Content/app_images/";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), questionViewModel.Image.FileName);
                    imageUrl = Path.Combine(uploadDir, questionViewModel.Image.FileName);
                    questionViewModel.Image.SaveAs(imagePath);
                }

                //Guardar en la Bd
                question.QuestionText = questionViewModel.QuestionText;
                question.Score = questionViewModel.Score;
                if (imageUrl != "")
                    question.ImagePath = imageUrl;
                QuestionOption correctOption = question.Options.Where(x => x.IsCorrectOption == true).FirstOrDefault();
                correctOption.Option = questionViewModel.CorrectOption;
                List<QuestionOption> incorrectOptions = question.Options.Where(x => x.IsCorrectOption == false).ToList();
                incorrectOptions[0].Option = questionViewModel.IncorrectOption1;
                incorrectOptions[1].Option = questionViewModel.IncorrectOption2;
                incorrectOptions[2].Option = questionViewModel.IncorrectOption3;
                await Db.SaveChangesAsync();
                Flash.Success("Ok", "La Pregunta ha sido editada correctamente");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Questions/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await Db.Questions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid? id)
        {
            Question question = await Db.Questions.FindAsync(id);
            try
            {
                if (question.Evaluations.Count() == 0)
                {
                    if (!String.IsNullOrEmpty(question.ImagePath))
                    {
                        string fullPath = Request.MapPath(question.ImagePath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                    Db.Questions.Remove(question);
                    await Db.SaveChangesAsync();
                    Flash.Success("Ok", "Pregunta eliminada satisfactoriamente");
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Flash.Error("Error", "Pregunta no puede ser eliminada, revise que no tenga relaciones");
                return View(question);
            }
        }
    }
}
