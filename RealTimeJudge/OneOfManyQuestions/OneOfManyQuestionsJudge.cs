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
    class OneOfManyQuestionsJudge
    {
        private IList<IList<double>> _tasksVariantsPrice; // price of each variant of answers for each task

        public OneOfManyQuestionsJudge(int numberOfStudents, int numberOfOneOfManyQuestions, double maxMark)
        {
            NumberOfStudents = numberOfStudents;
            NumberOfOneOfManyQuestions = numberOfOneOfManyQuestions;
            MaxMark = maxMark;
        }

        public int NumberOfStudents { get; private set; } // number of queries from the Students table of the DB

        public int NumberOfOneOfManyQuestions { get; private set; } // number of queries from the Questions table of the DB

        public double MaxMark { get; private set; } // max mark for the test

        public Tuple<IEnumerable<PriceData>, IList<Task>> EvaluateOneOfManyQuestionsMarks(
            IList<double> initialQuestionsComplexity,
            IList<IList<Student_Answer>> answers,
            IList<IList<double>> tasksVariantsPrice,
            IList<Task> tasks,
            IList<PriceData> marks)
        {
            _tasksVariantsPrice = tasksVariantsPrice;

            //marks of each student for the block of one from many questions
            var questionsComplexity = new double[NumberOfStudents][];

            //initial marks - the most expensive answers prices
            IList<double> initialMarks = new List<double>(NumberOfOneOfManyQuestions);
            for (var i = 0; i < NumberOfOneOfManyQuestions; ++i)
            {
                initialMarks.Add(_tasksVariantsPrice[i].Max());
            }

            // for the first student mark we assume that the questions have initialQuestionsComplexity,
            // the previous student had the maxMark Є [0, 1]
            // and he chose the most expensive variants of answers
            questionsComplexity[0] = EvaluateCurrentStudentQuestionsComplexity(
                initialQuestionsComplexity, 
                1,
                initialMarks);
            
            //here we evaluate answers prices depending on student's given answers
            IList<double> previousStudentEachQuestionMarks = new List<double>(NumberOfOneOfManyQuestions);

            for (var i = 0; i < NumberOfOneOfManyQuestions; ++i)
            {
                var taskAnswers = tasks[i].Answers;

                foreach (var answer in taskAnswers.Where(ti => ti.Text == answers[0][i].GivenAnswer))
                {
                    previousStudentEachQuestionMarks.Add(answer.PriceAnswer);
                    break;
                }
            }

			marks[0].PricePriceData = EvaluateCurrentStudentMark(
                questionsComplexity[0], 
                previousStudentEachQuestionMarks);

            // we should reevaluate prices of variants of each question
            _tasksVariantsPrice = EvaluateTasksVariantsPrice();
            previousStudentEachQuestionMarks =
                EvaluateEachQuestionMark(
                    previousStudentEachQuestionMarks,
                    _tasksVariantsPrice);

            for (var j = 1; j < NumberOfStudents; ++j)
            {
                questionsComplexity[j] =
                    EvaluateCurrentStudentQuestionsComplexity(
                        questionsComplexity[j - 1],
						marks[j - 1].PricePriceData,
                        previousStudentEachQuestionMarks);

				marks[j].PricePriceData = EvaluateCurrentStudentMark(
                    questionsComplexity[j], 
                    previousStudentEachQuestionMarks);

                _tasksVariantsPrice = EvaluateTasksVariantsPrice();
                previousStudentEachQuestionMarks =
                    EvaluateEachQuestionMark(
                        previousStudentEachQuestionMarks,
                        _tasksVariantsPrice);
            }
            
            for (var i = 0; i < NumberOfOneOfManyQuestions; ++i)
            {
                tasks[i].PriceTask = questionsComplexity[NumberOfOneOfManyQuestions - 1][i] * MaxMark;
            }

            for (var i = 0; i < NumberOfStudents; ++i)
            {
                marks[i].PricePriceData = MaxMark * EvaluateCurrentStudentMark(
                    questionsComplexity.Last(), 
                    previousStudentEachQuestionMarks);
            }

            return new Tuple<IEnumerable<PriceData>, IList<Task>>(marks, tasks);
        }

        private double[] EvaluateCurrentStudentQuestionsComplexity(
            IList<double> previousStudentQuestionsComplexity,
            double previousStudentMark,
            IList<double> previousStudentEachQuestionMarks)
        {
            var currentStudentQuestionsComplexity = new double[NumberOfOneOfManyQuestions];

            for (var i = 0; i < NumberOfOneOfManyQuestions; ++i)
            {
                currentStudentQuestionsComplexity[i] =
                    previousStudentQuestionsComplexity[i] * 
                        (1.0 + 1.0 / NumberOfOneOfManyQuestions *
                            (previousStudentMark * (_tasksVariantsPrice[i].Max() - previousStudentEachQuestionMarks[i]) -
                            (1 - previousStudentMark) * (previousStudentEachQuestionMarks[i] - _tasksVariantsPrice[i].Min())));
            }

            return currentStudentQuestionsComplexity;
        }

        private double EvaluateCurrentStudentMark(
            IList<double> currentStudentQuestionsComplexity,
            IList<double> currentStudentAnswers)
        {
            double currentComplexity = 0, totalComplexity = 0;

            for (var i = 0; i < NumberOfOneOfManyQuestions; ++i)
            {
                currentComplexity += currentStudentQuestionsComplexity[i] * 
                    (currentStudentAnswers[i] - _tasksVariantsPrice[i].Min()) 
                    / (_tasksVariantsPrice[i].Max() - _tasksVariantsPrice[i].Min());
                totalComplexity += currentStudentQuestionsComplexity[i];
            }

            return currentComplexity / totalComplexity;
        }

        //we should reevaluate each variant price for each task
        private IList<IList<double>> EvaluateTasksVariantsPrice()
        {
            for (var i = 0; i < NumberOfOneOfManyQuestions; ++i)
            {
                for (var j = 0; j < _tasksVariantsPrice[i].Count(); ++j)
                {
                    _tasksVariantsPrice[i][j] = (_tasksVariantsPrice[i][j] - _tasksVariantsPrice[i].Min()) / 
                        (_tasksVariantsPrice[i].Max() - _tasksVariantsPrice[i].Min());
                }
            }

            return _tasksVariantsPrice;
        }

        // to reevaluate question complexity we should use each question mark in the formula
        // (to watch the formul goto the main cycle in EvaluateOneFromManyQuestionsMarks)
        private IList<double> EvaluateEachQuestionMark(
            IList<double> currentStudentAnswers,
            IList<IList<double>> tasksVariantsPrice)
        {
            IList<double> currentStudentEachQuestionMark = new List<double>(NumberOfOneOfManyQuestions);

            for (var i = 0; i < NumberOfOneOfManyQuestions; ++i)
            {
                currentStudentEachQuestionMark.Add((currentStudentAnswers[i] - tasksVariantsPrice[i].Min())
                    / (tasksVariantsPrice[i].Max() - tasksVariantsPrice[i].Min()));
            }

            return currentStudentAnswers;
        }
 
    }
}