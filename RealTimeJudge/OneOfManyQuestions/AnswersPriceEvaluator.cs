/*
 * RealTimeJudge header
 * All rights reserved.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using SMPR_testing_Lib.Domain;

namespace RealTimeJudge.OneOfManyQuestions
{
    public class AnswersPriceEvaluator
    {
        public static Tuple<IEnumerable<PriceData>, IList<Task>> EvaluateOneOfManyQuestions(
            IList<Task> tasks,
            IList<IList<Student_Answer>> answers,
            IList<PriceData> marks,
            double maxMark)
        {
            // answers - for each student his answers for each task
            
            var initialQuestionsComplexity = tasks.Select(task => task.Price / maxMark).ToList();
            
            // initial price of each possible variant of the answer for a question
            IList<IList<double>> tasksVariantsPrice = new List<IList<double>>(tasks.Count);

            foreach (var task in tasks)
            {
                tasksVariantsPrice.Add(task.Answers.Select(answer => answer.Price).ToList());
            }

            var numberOfStudents = answers.Count();
            var numberOfOneFromManyQuestions = answers.First().Count();

            var judge = new OneOfManyQuestionsJudge(numberOfStudents, numberOfOneFromManyQuestions, maxMark);
            var marksAndComplexity = judge.EvaluateOneOfManyQuestionsMarks(initialQuestionsComplexity, answers, tasksVariantsPrice, tasks, marks);

            return marksAndComplexity;
        }
    }
}
