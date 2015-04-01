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
    class ManyOfManyQuestionsJudge
    {
        private IList<IList<Answer>> _tasksVariants; // variants of answers for each task

        public ManyOfManyQuestionsJudge(int numberOfStudents, int numberOfManyOfManyQuestions, double maxMark)
        {
            NumberOfStudents = numberOfStudents;
            NumberOfManyOfManyQuestions = numberOfManyOfManyQuestions;
            MaxMark = maxMark;
        }

        public int NumberOfStudents { get; private set; } // number of queries from the Students table of the DB

        public int NumberOfManyOfManyQuestions { get; private set; } // number of queries from the Questions table of the DB

        public double MaxMark { get; private set; } // max mark for the test

        public Tuple<IEnumerable<PriceData>, IList<Task>> EvaluateManyOfManyQuestionsMarks(
            IList<double> initialQuestionsComplexity,
            IList<IList<IList<Student_Answer>>> answers,
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

            for (var i = 0; i < NumberOfManyOfManyQuestions; ++i)
            {
                tasks[i].Price = questionsComplexity[NumberOfManyOfManyQuestions - 1][i] * MaxMark;
            }

            for (var i = 0; i < NumberOfStudents; ++i)
            {
                marks[i].Price = MaxMark * EvaluateCurrentStudentMark(questionsComplexity.Last(), answers[i]);
            }

            return new Tuple<IEnumerable<PriceData>, IList<Task>>(marks, tasks);
        }

        private double[] EvaluateCurrentStudentQuestionsComplexity(
            IList<double> previousStudentQuestionsComplexity,
            double previousStudentMark,
            IList<IList<Student_Answer>> currentStudentAnswers)
        {
            var currentStudentQuestionsComplexity = new double[NumberOfManyOfManyQuestions];
            var bp = new List<int>(NumberOfManyOfManyQuestions);
            var bq = new List<int>(NumberOfManyOfManyQuestions);

            for (var i = 0; i < NumberOfManyOfManyQuestions; ++i)
            {
                var correctAnswers = _tasksVariants[i].Where(answer => answer.Price > 0).ToList();
                bp.Add(correctAnswers.Count);
                var incorrectAnswers = _tasksVariants[i].Except(correctAnswers).ToList();
                bq.Add(_tasksVariants[i].Count - bp[i]);
                double correctAnswersSum = 0, incorrectAnswersSum = 0;

                for (var j = 0; j < correctAnswers.Count(); ++j)
                {
                    correctAnswersSum +=
                        currentStudentAnswers[i].Select(answer => answer.GivenAnswer).Contains(correctAnswers[j].Text)
                            ? correctAnswers[j].Price
                            : 0;
                }

                for (var j = 0; j < incorrectAnswers.Count(); ++j)
                {
                    incorrectAnswersSum +=
                        currentStudentAnswers[i].Select(answer => answer.GivenAnswer).Contains(incorrectAnswers[j].Text)
                            ? 1
                            : 0;
                }
            
                currentStudentQuestionsComplexity[i] =
                    previousStudentQuestionsComplexity[i] +
                    previousStudentMark * (1 - previousStudentQuestionsComplexity[i]) *
                    (1 - correctAnswersSum - 1.0 / (bp[i] + bq[i]) * (bq[i] - incorrectAnswersSum));
            }

            return currentStudentQuestionsComplexity;
        }

        private double EvaluateCurrentStudentMark(
            IList<double> currentStudentQuestionsComplexity,
            IList<IList<Student_Answer>> currentStudentAnswers)
        {
            double currentComplexity = 0, totalComplexity = 0;

            for (var i = 0; i < NumberOfManyOfManyQuestions; ++i)
            {
                var correctAnswers = _tasksVariants[i].Where(answer => answer.Price > 0).ToList();
                var incorrectAnswers = _tasksVariants[i].Except(correctAnswers).ToList();
                double correctAnswersSum = 0, incorrectAnswersSum = 0;

                for (var j = 0; j < correctAnswers.Count(); ++j)
                {
                    correctAnswersSum +=
                        currentStudentAnswers[i].Select(answer => answer.GivenAnswer).Contains(correctAnswers[j].Text)
                            ? correctAnswers[j].Price
                            : 0;
                }

                for (var j = 0; j < incorrectAnswers.Count(); ++j)
                {
                    incorrectAnswersSum +=
                        currentStudentAnswers[i].Select(answer => answer.GivenAnswer).Contains(incorrectAnswers[j].Text)
                            ? 1
                            : 0;
                }

                currentComplexity += currentStudentQuestionsComplexity[i] *
                    (correctAnswersSum - correctAnswers.Select(answer => answer.Price).Min() * incorrectAnswersSum) 
                    / _tasksVariants[i].Count; 
                totalComplexity += currentStudentQuestionsComplexity[i];
            }

            return currentComplexity / totalComplexity;
        }
    }
}
