using SMPR_testing.Models;
using SMPR_testing_Lib.Domain;
using SMPR_testing_Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SMPR_testing.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        ISmprRepository _repository;

        public ProfileController(ISmprRepository repo)
        {
            _repository = repo;
        }

        [AllowAnonymous]
        public ActionResult Index(int userId = 0)
        {
            if ((User.IsInRole("lecturer") || User.IsInRole("admin")) && userId == 0)
                return RedirectToAction("AllInfo");

            if (User.IsInRole("student"))
                userId = WebSecurity.CurrentUserId;

            var statistic = new StudentStatistic();

            var priceDatas = _repository.PricesData.Where(x => x.UserId == userId);
            var studentAnswers = priceDatas.SelectMany(x => x.Student_Answers).ToList();

            var taskIds = priceDatas.Select(x => x.TaskId).Distinct();
            var answers = _repository.Answers.Where(x => taskIds.Contains(x.TaskId));

            statistic.Answers = new List<AnswerToShow>();

            foreach (var studentAnswer in studentAnswers)
            {
                var answerToShow = new AnswerToShow();
                answerToShow.Question = studentAnswer.PriceData.Task.Name;
                answerToShow.Price = studentAnswer.PriceData.PricePriceData;
                answerToShow.Answer = studentAnswer.GivenAnswer;

                int taskId = studentAnswer.PriceData.TaskId;
                var answer = answers.FirstOrDefault(x => x.TaskId == taskId
                                                         && x.Text == studentAnswer.GivenAnswer);

                if (answer != null)
                    answerToShow.IsCorrect = answer.IsCorrect;

                statistic.Answers.Add(answerToShow);
            }

            statistic.TotalAnswers = answers.Count(x => x.IsCorrect);
            statistic.AnswersCorrect = statistic.Answers.Count(x => x.IsCorrect);

            return View(statistic);
        }

        [Authorize(Roles = "lecturer, admin")]
        public ActionResult AllInfo(int testId = 1) 
        {
            if (_repository.Tests.Where(t => t.Id == testId).Count() == 0)
            {
                return new HttpNotFoundResult("Error occured");
            }
            var answers = _repository.Student_Answers.Where(x => x.PriceData.Task.TestId == testId);
            var students = answers.GroupBy(x => x.PriceData.User).ToList();
            var statistic = new List<StudentShortStatistic>();
            double maxMarkForText = _repository.Tests.Where(x => x.Id == testId).First().GPriceTest;
            double maxMarkForQuestions = _repository.Tests.Where(x => x.Id == testId).First().Tasks.Sum(t => t.PriceTask);

            foreach (var studentAnswers in students) {
                var name = studentAnswers.Key.Name;
                int studId = studentAnswers.Key.Id;
                var group = studentAnswers.Key.Group;

                Func<Student_Answer, Answer> answerInAnswers =
                    x =>
                    _repository.Answers.FirstOrDefault(y => y.TaskId == x.PriceData.TaskId && y.Text == x.GivenAnswer);

				double resultMarkForStudent = studentAnswers.Sum(x => x.PriceData.PricePriceData);

                var studStat = new StudentShortStatistic {
                    Id = studId,
                    Name = name,
                    Group = group,
                    TotalAnswers = _repository.Answers.Count(x => x.Task.TestId == testId && x.IsCorrect),
                    AnswersCorrect = studentAnswers.Count(x => answerInAnswers(x) != null && answerInAnswers(x).IsCorrect),
                    Mark = maxMarkForText * (resultMarkForStudent / maxMarkForQuestions)
                };

                statistic.Add(studStat);

                statistic = statistic.OrderByDescending(x => x.AnswersCorrect).ToList();
            }

            return View(statistic);
        }

    }
}
