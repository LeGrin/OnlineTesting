using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMPR_testing.Models;
using SMPR_testing_Lib.Domain;
using SMPR_testing_Lib.Repository;
using WebMatrix.WebData;

namespace SMPR_testing.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        ISmprRepository _repository;

        public TestController(ISmprRepository repo)
        {
            _repository = repo;
        }

        [Authorize]
        public ActionResult Index(int testId = 1)
        {
            if (!IsSessionActive())
                return RedirectToAction("Index", "Home");

            int userId = WebSecurity.CurrentUserId;
            var taskAnsweredIds = _repository.PricesData.Where(x => x.UserId == userId).Select(x => x.TaskId);

            var test = new TestModel { Tasks = new List<TaskModel>() };
            var tasks = _repository.Tasks.Where(x => x.TestId == testId && !taskAnsweredIds.Contains(x.Id)).ToList();

            foreach (var task in tasks)
            {

                var answers = _repository.Answers.Where(x => x.TaskId == task.Id).ToList();

                test.Tasks.Add(new TaskModel
                {
                    Id = task.Id,
                    TaskType = task.TaskType,
                    Question = task.Name,
                    Answers = answers
                });
            }

            if (tasks.Count == 0)
            {
                ViewBag.TestPassed = true;
            }
            else
            {
                ViewBag.TestPassed = false;
            }

            ViewBag.TestId = testId;

            return View(test);
        }

        [HttpPost]
        public JsonResult Answer(int taskId, string answer)
        {
            string[] answers = answer.Split('|');
            PriceData priceData = new PriceData()
            {
                SessionId = _repository.Sessions.FirstOrDefault().Id,
                UserId = WebSecurity.CurrentUserId,
                TaskId = taskId,
                Price = 0
            };

            _repository.SavePriceData(priceData);
            int priceDataId = _repository.PricesData.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId &&
                                                                         x.TaskId == taskId).Id;

            foreach (var ans in answers)
            {
                var studentAnswer = new Student_Answer { GivenAnswer = ans, PriceDataId = priceDataId };
                _repository.SaveStudent_Answer(studentAnswer);
            }

            return Json("true");
        }

        [Authorize(Roles = "lecturer, admin")]
        public ActionResult AddQuestion(int testId = 1)
        {
            if (_repository.Tests.Where(t => t.Id == testId).Count() == 0)
            {
                return new HttpNotFoundResult("Error occured");
            }
            var taskTypes = _repository.TaskTypes.ToList();

            var model = new AddQuestionModel
            {
                TestId = testId,
                TaskTypes = taskTypes,
                Answers = new List<string>(),
                IsCorrect = new List<bool>(),
            };
            for (int i = 0; i < 3; i++)
            {
                model.Answers.Add("");
                model.IsCorrect.Add(false);
            }

            return View(model);
        }

        [Authorize(Roles = "lecturer, admin")]
        [HttpPost]
        public ActionResult AddQuestion(AddQuestionModel model)
        {
            #region validation
            model.TaskTypes = _repository.TaskTypes.ToList();
            //check if there all answers are emtpy
            if (model.Answers.Count > 0)
            {
                if (model.Answers.Where(x => !string.IsNullOrEmpty(x)).Count() == 0)
                {
                    ModelState.AddModelError("EmptyAnswers", "Ответы должны быть заполненными");
                    return View(model);
                }
            }
            #region RemoveEmptyAnswers
            for (int i = 0; i < model.Answers.Count; i++)
            {
                if (string.IsNullOrEmpty(model.Answers[i]))
                {
                    model.Answers.RemoveAt(i);
                    model.IsCorrect.RemoveAt(i);
                    i--;
                }
            }
            #endregion
            string selectedTaskTypeName = _repository.TaskTypes.Where(x => x.Id == model.TaskTypeId).Select(t => t.Name).FirstOrDefault();
            //min right answers count = 1
            if (!model.IsCorrect.Any(x => x))
            {
                ModelState.AddModelError("RightAnswersCount", "Должен быть как минимум один правильный ответ");
                return View(model);
            }
            //required task type id
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (selectedTaskTypeName == "Один из двух")
            {
                if (model.Answers.Count > 2)
                {
                    ModelState.AddModelError("AnswersCount", "Для данного типа вопросов допускается только два ответа");
                    return View(model);
                }
            }
            if (selectedTaskTypeName == "Один из двух" || selectedTaskTypeName == "Один из многих")
            {
                if (model.IsCorrect.Where(x => x == true).Count() > 1)
                {
                    ModelState.AddModelError("RightAnswersCount", "Для данного типа вопросов допускается только один правильный ответ");
                    return View(model);
                }
            }
            #endregion

            _repository.SaveTask(new Task
            {
                TestId = model.TestId,
                Name = model.Question,
                TaskTypeId = model.TaskTypeId,
                Price = model.Price
            });
            var task = _repository.Tasks.FirstOrDefault(x => x.TestId == model.TestId && x.Name == model.Question);


            double correctAnswerPrice = 1.0 / model.IsCorrect.Count(x => x);
            double count = model.IsCorrect.Count(x => !x);
            double noncorrectAnswerPrice = -(1.0) / (model.IsCorrect.Count(x => !x) == 0 ? 1 : model.IsCorrect.Count(x => !x));

            for (int i = 0; i < model.Answers.Count; ++i)
            {
                _repository.SaveAnswer(new Answer
                {
                    TaskId = task.Id,
                    Text = model.Answers[i],
                    IsCorrect = model.IsCorrect[i],
                    Price = (model.IsCorrect[i] ? correctAnswerPrice : noncorrectAnswerPrice)
                });
            }

            return RedirectToAction("AddQuestion", "Test", new { testId = model.TestId });
        }

        [Authorize(Roles = "lecturer, admin")]
        public ActionResult QuestionList(int testId = 1)
        {

            var questions = _repository.Tasks.Where(x => x.TestId == testId).Select(x => new DeleteQuestionModel
            {
                Id = x.Id,
                Question = x.Name,
            });

            return View(questions.ToList());
        }

        [Authorize(Roles = "lecturer, admin")]
        [HttpPost]
        public ActionResult TaskAnswers(int taskId)
        {
            var task = _repository.Tasks.FirstOrDefault(x => x.Id == taskId);
            return task == null
                ? null
                : PartialView("_TaskAnswers", task.Answers.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "lecturer, admin")]
        public JsonResult DeleteQuestion(int questionId)
        {
            _repository.DeleteTask(questionId);
            return Json(true);
        }

        [HttpPost]
        public bool IsSessionActive()
        {
            var session = _repository.Sessions.FirstOrDefault();
            var now = DateTime.Now;

            if (now >= session.StartDate && now <= session.EndDate)
                return true;

            return false;
        }

        [HttpPost]
        public JsonResult CloseTestForUser(int userId, int testId)
        {
            var taskIds = _repository.PricesData.Where(x => x.UserId == userId).Distinct();
            if (_repository.Tasks.Count() == taskIds.Count())
            {
                var passedTest = _repository.PassedTests.FirstOrDefault(x => x.UserId == userId && x.TestId == testId);

                if (passedTest == null)
                {
                    passedTest = new PassedTest
                    {
                        TestId = testId,
                        UserId = userId,
                    };

                    _repository.SavePassedTest(passedTest);
                }

                return Json(true);
            }

            return Json(false);
        }
    }
}
