using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tesis.Models;
using Tesis.ViewModels;
using MvcFlash.Core.Extensions;
using System.IO;
using System.Threading.Tasks;

namespace Tesis.Controllers
{
    public class QuestionsController : BaseController
    {
        // GET: Questions
        public ActionResult Index()
        {
            return View();
        }

        // GET: Questions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        public async Task<ActionResult> Create(QuestionViewModel questionViewModel)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (questionViewModel.Image == null || questionViewModel.Image.ContentLength == 0)
            {
                ModelState.AddModelError("Image", "This field is required");
            }
            else if (!validImageTypes.Contains(questionViewModel.Image.ContentType))
            {
                ModelState.AddModelError("Image", "Los formatos permitidos son GIF, JPG o PNG.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    if (questionViewModel.Image != null && questionViewModel.Image.ContentLength > 0)
                    {
                        var uploadDir = "~/App_Data/uploads";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), questionViewModel.Image.FileName);
                        var imageUrl = Path.Combine(uploadDir, questionViewModel.Image.FileName);
                        questionViewModel.Image.SaveAs(imagePath);

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
                        return View();
                        //return RedirectToAction("Index");
                    }
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Questions/Edit/5
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

        // GET: Questions/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Questions/Delete/5
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
    }
}
