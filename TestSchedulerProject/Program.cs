using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPR_testing_Lib;
using RealTimeJudge;
using SMPR_testing_Lib.Repository;
using SMPR_testing_Lib.Domain;

namespace TestSchedulerProject
{
    class Program
    {
        static ISmprRepository _repository;
        static void Main(string[] args)
        {
            _repository = new SmprRepository();
            runAlgo(3);
        }
        static private void runAlgo(int sessionId)
        {
            Session cursession = _repository.Sessions.Where(x => x.Id == sessionId).FirstOrDefault();
            if (cursession.Id != 0)
            {
                IQueryable<SMPR_testing_Lib.Domain.Task> tasks = cursession.Test.Tasks.AsQueryable<SMPR_testing_Lib.Domain.Task>();
                //разбираем по типам вопросов
                IQueryable<TaskType> taskTypes = _repository.TaskTypes;
                foreach (var item in taskTypes)
                {
                    Tuple<IEnumerable<PriceData>, IList<SMPR_testing_Lib.Domain.Task>> result = null;
                    IQueryable<SMPR_testing_Lib.Domain.Task> tasksForCurrentBlock = tasks.Where(x => x.TaskType == item);
                    if (item.Name == "Много из многих")
                    {
                        result = EvaluateManyOfManyQuestions(cursession, tasksForCurrentBlock);
                    }
                    else if (item.Name == "Один из двух")
                    {
                        result = EvaluateYesNoQuestions(cursession, tasksForCurrentBlock);
                    }
                    else if (item.Name == "Один из многих")
                    {
                        result = EvaluateOneOfManyQuestions(cursession, tasksForCurrentBlock);
                    }
                    foreach (var priceData in result.Item1)
                    {
                        _repository.SavePriceData(priceData);
                    }
                    foreach (var task in result.Item2)
                    {
                        _repository.SaveTask(task);
                    }
                }
            }
        }

        static private Tuple<IEnumerable<PriceData>, IList<SMPR_testing_Lib.Domain.Task>> EvaluateManyOfManyQuestions(Session session, IQueryable<SMPR_testing_Lib.Domain.Task> tasks)
        {
            double maxMarkForQuestionBlock = tasks.Sum(x => x.Price);
            IList<IList<IList<Student_Answer>>> studAnswers = new List<IList<IList<Student_Answer>>>();
            //<student<task<student_answer>>>
            foreach (var student in session.Group.Users)
            {
                IList<IList<Student_Answer>> answersForOneStudent = new List<IList<Student_Answer>>();
                foreach (var task in tasks)
                {
                    foreach (var studentAnswer in _repository.PricesData.Where(x => x.TaskId == task.Id && x.UserId == student.Id))
                    {
                        answersForOneStudent.Add(studentAnswer.Student_Answers.ToList());
                    }
                }
                studAnswers.Add(answersForOneStudent);
            }
            IQueryable<int> taskIds = tasks.Select(t => t.Id);
            IQueryable<int> userIds = session.Group.Users.Select(u => u.Id).AsQueryable();
            List<PriceData> marks = _repository.PricesData.Where(x => taskIds.Contains(x.TaskId) && userIds.Contains(x.UserId)).ToList();
            return RealTimeJudge.ManyOfManyQuestions.AnswersPriceEvaluator.EvaluateManyOfManyQuestions(tasks.ToList(), studAnswers, marks, maxMarkForQuestionBlock);
        }

        static private Tuple<IEnumerable<PriceData>, IList<SMPR_testing_Lib.Domain.Task>> EvaluateYesNoQuestions(Session session, IQueryable<SMPR_testing_Lib.Domain.Task> tasks)
        {
            double maxMarkForQuestionBlock = tasks.Sum(x => x.Price);
            IList<IList<Student_Answer>> studAnswers = new List<IList<Student_Answer>>();
            foreach (var student in session.Group.Users)
            {
                IList<Student_Answer> answersForOneStudent = new List<Student_Answer>();
                foreach (var task in tasks)
                {
                    foreach (var studentAnswer in _repository.PricesData.Where(x => x.TaskId == task.Id && x.UserId == student.Id))
                    {
                        if (studentAnswer.Student_Answers.Count > 0)
                            answersForOneStudent.Add(studentAnswer.Student_Answers.First());
                    }
                }
                studAnswers.Add(answersForOneStudent);
            }
            IQueryable<int> taskIds = tasks.Select(t => t.Id);
            IQueryable<int> userIds = session.Group.Users.Select(u => u.Id).AsQueryable();
            List<PriceData> marks = _repository.PricesData.Where(x => taskIds.Contains(x.TaskId) && userIds.Contains(x.UserId)).ToList();
            return RealTimeJudge.YesNoQuestions.AnswersPriceEvaluator.EvaluateYesNoQuestions(tasks.ToList(), studAnswers, marks, maxMarkForQuestionBlock);
        }

        static private Tuple<IEnumerable<PriceData>, IList<SMPR_testing_Lib.Domain.Task>> EvaluateOneOfManyQuestions(Session session, IQueryable<SMPR_testing_Lib.Domain.Task> tasks)
        {
            double maxMarkForQuestionBlock = tasks.Sum(x => x.Price);
            IList<IList<Student_Answer>> studAnswers = new List<IList<Student_Answer>>();
            foreach (var student in session.Group.Users)
            {
                IList<Student_Answer> answersForOneStudent = new List<Student_Answer>();
                foreach (var task in tasks)
                {
                    foreach (var studentAnswer in _repository.PricesData.Where(x => x.TaskId == task.Id && x.UserId == student.Id))
                    {
                        if (studentAnswer.Student_Answers.Count > 0)
                            answersForOneStudent.Add(studentAnswer.Student_Answers.First());
                    }
                }
                studAnswers.Add(answersForOneStudent);
            }
            IQueryable<int> taskIds = tasks.Select(t => t.Id);
            IQueryable<int> userIds = session.Group.Users.Select(u => u.Id).AsQueryable();
            List<PriceData> marks = _repository.PricesData.Where(x => taskIds.Contains(x.TaskId) && userIds.Contains(x.UserId)).ToList();
            return RealTimeJudge.OneOfManyQuestions.AnswersPriceEvaluator.EvaluateOneOfManyQuestions(tasks.ToList(), studAnswers, marks, maxMarkForQuestionBlock);
        }
    }
}
