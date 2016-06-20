/*
 * RealTimeJudge header
 * All rights reserved.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using SMPR_testing_Lib.Domain;

namespace RealTimeJudge.YesNoQuestions
{
    public class AnswersPriceEvaluator
    {
        public static Tuple<IEnumerable<PriceData>, IList<Task>> EvaluateYesNoQuestions(
            IList<Task> tasks,
            IList<IList<Student_Answer>> answers,
            IList<PriceData> marks,
            double maxMark)
        {
            // answers - for each student his answers for each task

            var initialQuestionsComplexity = tasks.Select(task => task.PriceTask / maxMark).ToList();

            // initial possible variants of the answer for a question
            IList<IList<Answer>> tasksVariants = new List<IList<Answer>>(tasks.Count);

            foreach (var task in tasks)
            {
                tasksVariants.Add(task.Answers.ToList());
            }

            var numberOfStudents = answers.Count();
            var numberOfYesNoQuestions = answers.First().Count();
            
            var judge = new YesNoQuestionsJudge(numberOfStudents, numberOfYesNoQuestions, maxMark);
            var marksAndComplexity = judge.EvaluateYesNoQuestionsMarks(initialQuestionsComplexity, answers, tasksVariants, tasks, marks);

            return marksAndComplexity;
        }
    }
}