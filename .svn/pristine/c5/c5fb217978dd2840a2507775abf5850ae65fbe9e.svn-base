using SMPR_testing_Lib.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPR_testing_Lib.Repository
{
    /// <summary>
    /// Интерфейс для работы с БД
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
    public interface ISmprRepository
    {
        IQueryable<Answer> Answers { get; }

        void SaveAnswer(Answer answer);

        Answer DeleteAnswer(int answerId);

        IQueryable<Group> Groups { get; }

        void SaveGroup(Group group);

        Group DeleteGroup(int groupId);

        IQueryable<Group_Subject> Groups_Subjects { get; }

        void SaveGroup_Subject(Group_Subject group_subject);

        Group_Subject DeleteGroup_Subject(int group_subjectId);

        IQueryable<PriceData> PricesData { get; }

        void SavePriceData(PriceData priceData);

        PriceData DeletePriceData(int priceDataId);

        IQueryable<Session> Sessions { get; }

        void SaveSession(Session session);

        Session DeleteSession(int sessionId);

        IQueryable<Student_Answer> Student_Answers { get; }

        void SaveStudent_Answer(Student_Answer student_answer);

        Student_Answer DeleteStudent_Answer(int student_answerId);

        IQueryable<Subject> Subjects { get; }

        void SaveSubject(Subject subject);

        Subject DeleteSubject(int subjectId);

        IQueryable<SMPR_testing_Lib.Domain.Task> Tasks { get; }

        void SaveTask(SMPR_testing_Lib.Domain.Task task);

        Domain.Task DeleteTask(int taskId);

        IQueryable<TaskType> TaskTypes { get; }

        void SaveTaskType(TaskType taskType);

        TaskType DeleteTaskType(int taskTypeId);

        IQueryable<Test> Tests { get; }

        void SaveTest(Test test);

        Test DeleteTest(int testId);

        IQueryable<TestLog> TestLogs { get; }

        void SaveTestLog(TestLog testLog);

        TestLog DeleteTestLog(int testLogId);

        IQueryable<User> Users { get; }

        void SaveUser(User user);

        User DeleteUser(int userId);

        IQueryable<PassedTest> PassedTests { get; }

        void SavePassedTest(PassedTest test);

        PassedTest DeletePassedTest(int passedTestId);
    }
}
