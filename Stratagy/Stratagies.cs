using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_BGirnyk.Stratagy
{
    class RandStratagy : IAnswerChoiceStrategy
    {
        public string GetAnswer(string[] answerDict, int currAnswerNum)
        {
            int count = answerDict.Length;
            Random rnd = new Random();
            int randomPos = rnd.Next(1, count);
            return answerDict[randomPos];
        }
    }

    class UpseqStratagy : IAnswerChoiceStrategy
    {
        public string GetAnswer(string[] answerDict, int currAnswerNum)
        {
            int count = answerDict.Length;
            int ActualAnswerNum = currAnswerNum % count;
            return answerDict[ActualAnswerNum - 1];
        }
    }

    class DownseqStratagy : IAnswerChoiceStrategy
    {
        public string GetAnswer(string[] answerDict, int currAnswerNum)
        {
            int count = answerDict.Length;
            int ActualAnswerNum = currAnswerNum % count;
            return answerDict[count - ActualAnswerNum];
        }
    }

}
