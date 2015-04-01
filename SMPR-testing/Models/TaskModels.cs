using SMPR_testing_Lib.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMPR_testing.Models {
    public class TaskModel {
        public int Id { get; set; }
        public TaskType TaskType { get; set; }
        public string Question { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class TestModel {
        public List<TaskModel> Tasks { get; set; }
    }

    public class AnsweredTask : TaskModel {
        public bool IsChecked { get; set; }
    }

    public class AddQuestionModel {
        public int TestId { get; set; }

        [Required(ErrorMessage="Выберите тип вопроса")]
        public int TaskTypeId { get; set; }
        public List<TaskType> TaskTypes { get; set; }

        [Required(ErrorMessage = "Напишите вопрос")]
        public string Question { get; set; }
        
        public List<string> Answers { get; set; }
        public List<bool> IsCorrect { get; set; }

        [Range(0.0001,double.MaxValue, ErrorMessage= "Максимальный балл за вопрос должен быть больше 0")]
        [DataType("double", ErrorMessage = "Введеный макс балл за вопрос в некорректном формате")]
        [Required(ErrorMessage = "Введите максимальный балл в корректном формате")]
        public double Price { get; set; }
    }

    public class DeleteQuestionModel {
        public int Id { get; set; }
        public string Question { get; set; }
    }

    public class AnswerToShow {
        public string Question { get; set; }
        public string Answer { get; set; }
        public double Price { get; set; }

        public bool IsCorrect { get; set; }
    }

    public class StudentShortStatistic {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group Group { get; set; }

        public int AnswersCorrect { get; set; }
        public int TotalAnswers { get; set; }

        public double Mark { get; set; }
    }

    public class StudentStatistic : StudentShortStatistic {
        public List<AnswerToShow> Answers { get; set; }
    }
}