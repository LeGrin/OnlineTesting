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
    class YesNoQuestionsJudge
    {
        private IList<IList<Answer>> _tasksVariants; // variants of answers for each task

        public YesNoQuestionsJudge(int numberOfStudents, int numberOfYesNoQuestions, double maxMark)
        {
            NumberOfStudents = numberOfStudents;
            NumberOfYesNoQuestions = numberOfYesNoQuestions;
            MaxMark = maxMark;
        }

        public int NumberOfStudents { get; private set; } // number of queries from the Students table of the DB

        public int NumberOfYesNoQuestions { get; private set; } // number of queries from the Questions table of the DB

        public double MaxMark { get; private set; } // max mark for the test

        public Tuple<IEnumerable<PriceData>, IList<Task>> EvaluateYesNoQuestionsMarks(
            IList<double> initialQuestionsComplexity,
            IList<IList<Student_Answer>> answers,
            IList<IList<Answer>> tasksVariants,
            IList<Task> tasks,
            IList<PriceData> marks)
        {
            _tasksVariants = tasksVariants;
            var questionsComplexity = new double[NumberOfStudents][];

            // for the first student mark we assume that the questions have initialQuestionsComplexity
            // and the previous student had the maxMark Є [0, 1]
            questionsComplexity[0] = EvaluateCurrentStudentQuestionsComplexity(initialQuestionsComplexity, 1, answers.First());
            marks[0].Price = EvaluateCurrentStudentMark(questionsComplexity[0], answers.First());

            for (var j = 1; j < NumberOfStudents; ++j)
            {
                questionsComplexity[j] = 
                    EvaluateCurrentStudentQuestionsComplexity(
                        questionsComplexity[j - 1],
                        marks[j - 1].Price,
                        answers[j]);

                marks[j].Price = EvaluateCurrentStudentMark(questionsComplexity[j], answers[j]);
            }

            for (var i = 0; i < NumberOfYesNoQuestions; ++i)
            {
                tasks[i].Price = questionsComplexity[NumberOfYesNoQuestions - 1][i] * MaxMark;
            }

            // to evaluate result marks we use questionsComplexity evaluated at the last step
            for (var i = 0; i < NumberOfStudents; ++i)
            {
                marks[i].Price = MaxMark * EvaluateCurrentStudentMark(questionsComplexity.Last(), answers[i]);
            }

            return new Tuple<IEnumerable<PriceData>, IList<Task>>(marks, tasks);
        }

        private double[] EvaluateCurrentStudentQuestionsComplexity(
            IList<double> previousStudentQuestionsComplexity,
            double previousStudentMark,
            IList<Student_Answer> currentStudentAnswers)
        {
            var currentStudentQuestionsComplexity = new double[NumberOfYesNoQuestions];
            IList<Answer> isCorrect = new List<Answer>(NumberOfYesNoQuestions);

            // to determine if the answer given by a student was correct
            // we should find this variant of an answer in the set of answers
            for (var i = 0; i < NumberOfYesNoQuestions; ++i)
            {
                isCorrect.Add(_tasksVariants[i].First(answer => answer.Text == currentStudentAnswers[i].GivenAnswer));
            }

            for (var i = 0; i < NumberOfYesNoQuestions; ++i)
            {
                currentStudentQuestionsComplexity[i] = 
                    isCorrect[i].IsCorrect
                        ? previousStudentQuestionsComplexity[i] * previousStudentMark
                        : previousStudentQuestionsComplexity[i] + 
                          previousStudentMark * (1 - previousStudentQuestionsComplexity[i]);
            }

            return currentStudentQuestionsComplexity;
        }

        private double EvaluateCurrentStudentMark(
            IList<double> currentStudentQuestionsComplexity,
            IList<Student_Answer> currentStudentAnswers)
        {
            IList<Answer> isCorrect = new List<Answer>(NumberOfYesNoQuestions);

            for (var i = 0; i < NumberOfYesNoQuestions; ++i)
            {
                isCorrect.Add(_tasksVariants[i].First(answer => answer.Text == currentStudentAnswers[i].GivenAnswer));
            }

            double currentComplexity = 0, totalComplexity = 0;

            for (var i = 0; i < NumberOfYesNoQuestions; ++i)
            {
                currentComplexity += isCorrect[i].IsCorrect ? currentStudentQuestionsComplexity[i] : 0;
                totalComplexity += currentStudentQuestionsComplexity[i];
            }

            return currentComplexity / totalComplexity;
        }
    }
}