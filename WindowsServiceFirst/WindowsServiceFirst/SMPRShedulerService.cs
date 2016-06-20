using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using SMPR_testing_Lib.Domain;
using SMPR_testing_Lib.Repository;
using RealTimeJudge;

namespace SMPRShedulerService
{
    public partial class SMPRSheduler : ServiceBase
    {
        private System.Timers.Timer timer1;
        private SessionList sessionList;
        ISmprRepository _repository;

        public SMPRSheduler(ISmprRepository repo)
        {
            InitializeComponent();
            _repository = repo;
        }

        protected override void OnStart(string[] args)
        {
            AddLog("start - only hardcore! SMPR");
            sessionList = new SessionList();
            this.timer1 = new System.Timers.Timer();
            this.timer1.Enabled = true;
            //Интервал 10000мс - 10с.
            this.timer1.Interval = 100000; // 100 seconds
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer2_Elapsed);
            this.timer1.AutoReset = true;
            this.timer1.Start();

        }

        protected override void OnStop()
        {
            this.timer1.Stop();
            AddLog("Timer stopped");
            AddLog("stop - fuck that!");
        }

        public void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("SMPRSheduler"))
                {
                    EventLog.CreateEventSource("SMPRSheduler", "SMPRSheduler");
                }
                eventLog1.Source = "SMPRSheduler";
                eventLog1.WriteEntry(log);
            }
            catch { }
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

        private void runAlgo(int sessionId)
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

        private void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ISmprRepository repo = new SmprRepository();
            IQueryable<Session> sessions = repo.Sessions;
            foreach (Session session in sessions)
            {
                if (session.EndDate <= DateTime.Now && !session.IsCalculated)
                    sessionList.AddSession(new KeyValuePair<int, DateTime>(session.Id, session.EndDate));
            }
            IList<KeyValuePair<int, DateTime>> currentSessions = sessionList.GetAllSessions();
            //AddLog("Current Sessions :");
			AddLog("service check at " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            foreach (KeyValuePair<int, DateTime> session in currentSessions)
            {
                runAlgo(session.Key);
                AddLog("Session " + session.Key.ToString() + " is processed because end date reached" +
                        session.Value.ToString("dd.MM.yyyy HH:mm:ss") + "\n");
                sessionList.Remove(session);
            }

        }

        private Tuple<IEnumerable<PriceData>, IList<SMPR_testing_Lib.Domain.Task>> EvaluateManyOfManyQuestions(Session session, IQueryable<SMPR_testing_Lib.Domain.Task> tasks)
        {
            double maxMarkForQuestionBlock = tasks.Sum(x => x.PriceTask);
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

        private Tuple<IEnumerable<PriceData>, IList<SMPR_testing_Lib.Domain.Task>> EvaluateYesNoQuestions(Session session, IQueryable<SMPR_testing_Lib.Domain.Task> tasks)
        {
            double maxMarkForQuestionBlock = tasks.Sum(x => x.PriceTask);
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

        private Tuple<IEnumerable<PriceData>, IList<SMPR_testing_Lib.Domain.Task>> EvaluateOneOfManyQuestions(Session session, IQueryable<SMPR_testing_Lib.Domain.Task> tasks)
        {
            double maxMarkForQuestionBlock = tasks.Sum(x => x.PriceTask);
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

        public System.Timers.ElapsedEventHandler timer1_Elapsed { get; set; }
    }
}
