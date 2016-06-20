/*
 * RealTimeJudge header
 * All rights reserved.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using SMPR_testing_Lib.Domain;

namespace RealTimeJudge.ManyOfManyQuestions
{
    public class AnswersPriceEvaluator
    {
        public static Tuple<IEnumerable<PriceData>, IList<Task>> EvaluateManyOfManyQuestions(
            IList<Task> tasks,
            IList<IList<IList<Student_Answer>>> answers,
            IList<PriceData> marks,
            double maxMark)
        {
            // answers: for each student variants he chosen for each task
            
            
            // initial price of each possible variant of the answer for a question
            IList<IList<Answer>> tasksVariants = new List<IList<Answer>>(tasks.Count);

            foreach (var task in tasks)
            {
                tasksVariants.Add(task.Answers.ToList());
            }

            var numberOfStudents = answers.Count();
            var numberOfOneFromManyQuestions = answers.First().Count();

            var judge = new ManyOfManyQuestionsJudge(numberOfStudents, numberOfOneFromManyQuestions, maxMark);
            var marksAndComplexity = judge.EvaluateManyOfManyQuestionsMarks(initialQuestionsComplexity, answers, tasksVariants, tasks, marks);

            return marksAndComplexity;
        }
    }
}
