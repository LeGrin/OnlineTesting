using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

/*
	Just create instance of CheckDifferences class and 
	call the method - getResultOfComparison(1st text, 2nd text); and get the result;
*/

namespace CheckDifferenceTexts
{

    class CheckDifferences
    {

        private String text1;
        private String text2;
        private readonly String RESULT = "The result of the comparison: "; // Результат:
        private readonly String PERCENT = "%";

        private readonly String regexStr = "[(){}+,\\-!.\\s?:\"']";

        public static void Main(string[] args) {
             String s1 = "Сегодня хорошая погода, а завтра будет плохая погода!";
             String s2 = "Завтра будет хорошая погода, а сегодня плохая погода!";
             CheckDifferences diff = new CheckDifferences();

             diff.getResultOfComparison(s1, s2);
         }

        CheckDifferences()
        {
        }

        public void getResultOfComparison(String s1, String s2)
        {
            text1 = s1;
            text2 = s2;
            double res1 = this.CheckDifference();
            double res2 = this.CompareStrings();

            this.print(res1, res2);
        }

        private void print(double res1, double res2)
        {
            if (res1.Equals(0) || res2 > res1)
            {
                Console.WriteLine(RESULT + res2.ToString("0.") + PERCENT);
            }
            else if (res2.Equals(0) || res1 > res2)
            {
                Console.WriteLine(RESULT + res1.ToString("0.") + PERCENT);
            }
        }

        private double CheckDifference()
        {
            String textH1 = text1.ToLower();
            String textH2 = text2.ToLower();
            String[] text1Words = textH1.Split(' ');
            String[] text2Words = textH2.Split(' ');
            double sameWords;
            double max;
            Regex regex = new Regex(regexStr);

            for (int i = 0; i < text1Words.Length; i++)
            {
                text1Words[i] = regex.Replace(text1Words[i], "");
            }

            for (int i = 0; i < text2Words.Length; i++)
            {
                text2Words[i] = regex.Replace(text2Words[i], "");
            }
            int text1WordCount = text1Words.Length;
            int text2WordCount = text2Words.Length;


            int[,] solutionMatrix = new int[text1WordCount + 1, text2WordCount + 1];

            for (int i = text1WordCount - 1; i >= 0; i--)
            {
                for (int j = text2WordCount - 1; j >= 0; j--)
                {
                    if (text1Words[i].Equals(text2Words[j]))
                    {
                        solutionMatrix[i, j] = solutionMatrix[i + 1, j + 1] + 1;
                    }
                    else
                    {
                        solutionMatrix[i, j] = Math.Max(solutionMatrix[i + 1, j],
                            solutionMatrix[i, j + 1]);
                    }
                }
            }

            int k = 0, k1 = 0;
            ArrayList lcsResultList = new ArrayList();
            while (k < text1WordCount && k1 < text2WordCount)
            {
                if (text1Words[k].Equals(text2Words[k1]))
                {
                    lcsResultList.Add(text2Words[k1]);
                    k++;
                    k1++;
                }
                else if (solutionMatrix[k + 1, k1] >= solutionMatrix[k, k1 + 1])
                {
                    k++;
                }
                else
                {
                    k1++;
                }
            }

            max = text1WordCount > text2WordCount ? text1WordCount : text2WordCount;
            sameWords = (double)lcsResultList.Count / max;

            return sameWords * 100;
        }

        private String[] LetterPairs(String str)
        {
            int numPairs = str.Length - 1;
            String[] pairs = new String[numPairs];

            for (int i = 0; i < numPairs; i++)
            {
                pairs[i] = str.Substring(i, 2);
            }

            return pairs;
        }

        private ArrayList WordLetterPairs(String str)
        {
            ArrayList allPairs = new ArrayList();

            String[] words = str.Split(' ');

            for (int w = 0; w < words.Length; w++)
            {
                // Find the pairs of characters
                String[] pairsInWord = LetterPairs(words[w]);
                for (int p = 0; p < pairsInWord.Length; p++)
                {
                    allPairs.Add(pairsInWord[p]);
                }
            }
            return allPairs;
        }

        public double CompareStrings()
        {
            Regex regex = new Regex(regexStr);
            ArrayList pairs1 = WordLetterPairs(regex.Replace(text1.ToUpper(), ""));
            ArrayList pairs2 = WordLetterPairs(regex.Replace(text2.ToUpper(), ""));

            int intersection = 0;
            int union = pairs1.Count + pairs2.Count;

            for (int i = 0; i < pairs1.Count; i++)
            {
                Object pair1 = pairs1[i];
                for (int j = 0; j < pairs2.Count; j++)
                {
                    Object pair2 = pairs2[j];
                    if (pair1.Equals(pair2))
                    {
                        intersection++;
                        pairs2.Remove(j);
                        break;
                    }
                }
            }

            return ((2.0 * intersection) / union) * 100;
        }

    }

}
