using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_BGirnyk.Stratagy
{
    interface IAnswerChoiceStrategy
    {
        String GetAnswer(string[] answerDict, int currAnswerNum); 
    }
}
