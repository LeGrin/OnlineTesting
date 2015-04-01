using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SMPR_testing_Lib.Domain;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace SMPR_testing_Lib.Repository
{
    /// <summary>
    /// Предоставляет доступ к данным в бд
    /// 
    /// свойства IQueryable<...> ... возвращают все записи из определенной таблицы, имя таблицы совпадает с именем свойства
    /// 
    /// методы Save...(...) служат для для добавления или же изменения записей
    /// добавление в случае, если поле Id в параметре будет равным 0
    /// изменение в ином случае, все поля в базе, которые будут соответствовать ключу из параметра будут переписаны
    /// 
    /// методы Delete...(...) служат для удаление записей из базы
    /// возвращают удаленный объект, если записи с таким ключем не найдено, будет возвращет NULL
    /// </summary>
    /// 
    public class SmprRepository : ISmprRepository
    {
        SmprDbContext _context = new SmprDbContext();

        public IQueryable<Answer> Answers
        {
            get
            {
                return _context.Answers;
            }
        }

        public void SaveAnswer(Answer answer)
        {
            if (answer.Id == 0)
            {
                _context.Answers.Add(answer);
            }
            else
            {
                Answer dbEntry = _context.Answers.Find(answer.Id);
                if (dbEntry != null)
                {
                    dbEntry.IsCorrect = answer.IsCorrect;
                    dbEntry.Price = answer.Price;
                    dbEntry.TaskId = answer.TaskId;
                    dbEntry.Text = answer.Text;
                }
            }
            _context.SaveChanges();
        }

        public Answer DeleteAnswer(int answerId)
        {
            Answer dbEntry = _context.Answers.Find(answerId);
            if (dbEntry != null)
            {
                _context.Answers.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<Group> Groups
        {
            get
            {
                return _context.Groups;
            }
        }

        public void SaveGroup(Group group)
        {
            if (group.Id == 0)
            {
                _context.Groups.Add(group);
            }
            else
            {
                Group dbEntry = _context.Groups.Find(group.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = group.Name;
                }
            }
            _context.SaveChanges();
        }

        public Group DeleteGroup(int groupId)
        {
            Group dbEntry = _context.Groups.Find(groupId);
            if (dbEntry != null)
            {
                _context.Groups.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<Group_Subject> Groups_Subjects
        {
            get
            {
                return _context.Group_Subject;
            }
        }

        public void SaveGroup_Subject(Group_Subject group_subject)
        {
            if (group_subject.Id == 0)
            {
                _context.Group_Subject.Add(group_subject);
            }
            else
            {
                Group_Subject dbEntry = _context.Group_Subject.Find(group_subject.Id);
                if (dbEntry != null)
                {
                    dbEntry.GroupId = group_subject.GroupId;
                    dbEntry.SubjectId = group_subject.SubjectId;
                }
            }
            _context.SaveChanges();
        }

        public Group_Subject DeleteGroup_Subject(int group_subjectId)
        {
            Group_Subject dbEntry = _context.Group_Subject.Find(group_subjectId);
            if (dbEntry != null)
            {
                _context.Group_Subject.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<PriceData> PricesData
        {
            get
            {
                return _context.PricesDatas;
            }
        }

        public void SavePriceData(PriceData priceData)
        {
            if (priceData.Id == 0)
            {
                _context.PricesDatas.Add(priceData);
            }
            else
            {
                PriceData dbEntry = _context.PricesDatas.Find(priceData.Id);
                if (dbEntry != null)
                {
                    dbEntry.Price = priceData.Price;
                    dbEntry.SessionId = priceData.SessionId;
                    dbEntry.TaskId = priceData.TaskId;
                    dbEntry.UserId = priceData.UserId;
                }
            }
            _context.SaveChanges();
        }

        public PriceData DeletePriceData(int priceDataId)
        {
            PriceData dbEntry = _context.PricesDatas.Find(priceDataId);
            if (dbEntry != null)
            {
                _context.PricesDatas.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<Session> Sessions
        {
            get { return _context.Sessions; }
        }

        public void SaveSession(Session session)
        {
            if (session.Id == 0)
            {
                _context.Sessions.Add(session);
            }
            else
            {
                Session dbEntry = _context.Sessions.Find(session.Id);
                if (dbEntry != null)
                {
                    dbEntry.EndDate = session.EndDate;
                    dbEntry.GroupId = session.GroupId;
                    dbEntry.IsCalculated = session.IsCalculated;
                    dbEntry.StartDate = session.StartDate;
                    dbEntry.TestId = session.TestId;
                }
            }
            _context.SaveChanges();
        }

        public Session DeleteSession(int sessionId)
        {
            Session dbEntry = _context.Sessions.Find(sessionId);
            if (dbEntry != null)
            {
                _context.Sessions.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<Student_Answer> Student_Answers
        {
            get { return _context.Student_Answer; }
        }

        public void SaveStudent_Answer(Student_Answer student_answer)
        {
            if (student_answer.Id == 0)
            {
                _context.Student_Answer.Add(student_answer);
            }
            else
            {
                Student_Answer dbEntry = _context.Student_Answer.Find(student_answer.Id);
                if (dbEntry != null)
                {
                    dbEntry.GivenAnswer = student_answer.GivenAnswer;
                    dbEntry.PriceDataId = student_answer.PriceDataId;
                }
            }
            _context.SaveChanges();
        }

        public Student_Answer DeleteStudent_Answer(int student_answerId)
        {
            Student_Answer dbEntry = _context.Student_Answer.Find(student_answerId);
            if (dbEntry != null)
            {
                _context.Student_Answer.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<Subject> Subjects
        {
            get { return _context.Subjects; }
        }

        public void SaveSubject(Subject subject)
        {
            if (subject.Id == 0)
            {
                _context.Subjects.Add(subject);
            }
            else
            {
                Subject dbEntry = _context.Subjects.Find(subject.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = subject.Name;
                    dbEntry.UserId = subject.UserId;
                }
            }
            _context.SaveChanges();
        }

        public Subject DeleteSubject(int subjectId)
        {
            Subject dbEntry = _context.Subjects.Find(subjectId);
            if (dbEntry != null)
            {
                _context.Subjects.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<Domain.Task> Tasks
        {
            get { return _context.Tasks; }
        }

        public void SaveTask(Domain.Task task)
        {
            if (task.Id == 0)
            {
                _context.Tasks.Add(task);
            }
            else
            {
                Domain.Task dbEntry = _context.Tasks.Find(task.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = task.Name;
                    dbEntry.TaskTypeId = task.TaskTypeId;
                    dbEntry.TestId = task.TestId;
                    dbEntry.Price = task.Price;
                }
            }
            _context.SaveChanges();
        }

        public Domain.Task DeleteTask(int taskId)
        {
            Domain.Task dbEntry = _context.Tasks.Find(taskId);
            
            if (dbEntry != null) {

                foreach (var answer in dbEntry.Answers.ToList())
                    _context.Answers.Remove(answer);

                foreach (var priceData in dbEntry.PricesData.ToList()) {
                    foreach (var answer in priceData.Student_Answers.ToList()) {
                        _context.Student_Answer.Remove(answer);
                    }
                }

                _context.Tasks.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<TaskType> TaskTypes
        {
            get { return _context.TaskTypes; }
        }

        public void SaveTaskType(TaskType taskType)
        {
            if (taskType.Id == 0)
            {
                _context.TaskTypes.Add(taskType);
            }
            else
            {
                TaskType dbEntry = _context.TaskTypes.Find(taskType.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = taskType.Name;
                }
            }
            _context.SaveChanges();
        }

        public TaskType DeleteTaskType(int taskTypeId)
        {
            TaskType dbEntry = _context.TaskTypes.Find(taskTypeId);
            if (dbEntry != null)
            {
                _context.TaskTypes.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<Test> Tests
        {
            get { return _context.Tests; }
        }

        public void SaveTest(Test test)
        {
            if (test.Id == 0)
            {
                _context.Tests.Add(test);
            }
            else
            {
                Test dbEntry = _context.Tests.Find(test.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = test.Name;
                    dbEntry.SubjectId = test.SubjectId;
                    dbEntry.UserId = test.UserId;
                    dbEntry.Price = test.Price;
                }
            }
            _context.SaveChanges();
        }

        public Test DeleteTest(int testId)
        {
            Test dbEntry = _context.Tests.Find(testId);
            if (dbEntry != null)
            {
                _context.Tests.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<TestLog> TestLogs
        {
            get { return _context.TestLogs; }
        }

        public void SaveTestLog(TestLog testLog)
        {
            if (testLog.Id == 0)
            {
                _context.TestLogs.Add(testLog);
            }
            else
            {
                TestLog dbEntry = _context.TestLogs.Find(testLog.Id);
                if (dbEntry != null)
                {
                    dbEntry.Message = testLog.Message;
                    dbEntry.TestId = testLog.TestId;
                }
            }
            _context.SaveChanges();
        }

        public TestLog DeleteTestLog(int testLogId)
        {
            TestLog dbEntry = _context.TestLogs.Find(testLogId);
            if (dbEntry != null)
            {
                _context.TestLogs.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<User> Users
        {
            get { return _context.Users; }
        }

        public void SaveUser(User user)
        {
            try
            {
                if (user.Id == 0)
                {
                    _context.Users.Add(user);
                }
                else
                {
                    User dbEntry = _context.Users.Find(user.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.GroupId = user.GroupId;
                        dbEntry.Name = user.Name;
                    }
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public User DeleteUser(int userId)
        {
            User dbEntry = _context.Users.Find(userId);
            if (dbEntry != null)
            {
                _context.Users.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<PassedTest> PassedTests
        {
            get
            {
                return _context.PassedTests;
            }
        }

        public void SavePassedTest(PassedTest test)
        {
            if (test.Id == 0)
            {
                _context.PassedTests.Add(test);
            }
            else
            {
                PassedTest dbEntry = _context.PassedTests.Find(test.Id);
                if (dbEntry != null)
                {
                    dbEntry.TestId = test.TestId;
                }
            }
            _context.SaveChanges();
        }

        public PassedTest DeletePassedTest(int passedTestId)
        {
            PassedTest dbEntry = _context.PassedTests.Find(passedTestId);
            if (dbEntry != null)
            {
                _context.PassedTests.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
