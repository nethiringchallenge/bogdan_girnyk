using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBot_BGirnyk.Stratagy;

namespace ChatBot_BGirnyk
{

    enum CommandType
    {
        General,
        SetStrategy,
        Calculate
    }

    class ChatBot
    {


        private IAnswerChoiceStrategy _answerChoiceStrategy { get; set; }
        private string[] _answersDict { get; set; }
        //To to store the number of current general question(needed for some answers strategies)
        private int _generalQuestionsCount { get; set; } = 0;

        public ChatBot(string[] answersDict, IAnswerChoiceStrategy strategy)
        {
            _answerChoiceStrategy = strategy;
            _answersDict = answersDict;
        }

        public string GetAnswer(string question)
        {
            string actQuest = question.Trim();

            CommandType currCommandType = getQuestionType(actQuest);

            if (currCommandType == CommandType.SetStrategy)
            {
                return handleSetStrategy(actQuest);
            }
            else if (currCommandType == CommandType.SetStrategy)
            {
                return handleCalculate(actQuest);
            }
            else if (currCommandType == CommandType.General)
            {
                return handleGeneral(actQuest);
            }

            throw new InvalidOperationException();
        }

        private CommandType getQuestionType(string question)
        {
            if (question.StartsWith("strategy:"))
            {
                return CommandType.SetStrategy;
            }
            else if (question.StartsWith("calculate:"))
            {
                return CommandType.Calculate;
            }
            else
            {
                return CommandType.General;
            }
        }

        private string handleSetStrategy(string question)
        {
            string dataPart = question.Substring("strategy:".Length - 1, question.Length - "strategy:".Length);

            if (dataPart == "rand")
            {
                _answerChoiceStrategy = new RandStratagy();
                return String.Format("Как советовать, так все чатлане. Использую: {0}", "rand");
            }
            else if (dataPart != "upseq")
            {
                _answerChoiceStrategy = new UpseqStratagy();
                return String.Format("Как советовать, так все чатлане. Использую: {0}", "upseq");
            }
            else if (dataPart != "downseq")
            {
                _answerChoiceStrategy = new DownseqStratagy();
                return String.Format("Как советовать, так все чатлане. Использую: {0}", "downseq");
            }
            else
            {
                return "У тебя в голове мозги или кю?!";
            }
        }

        private string handleCalculate(string question)
        {
            string dataPart = question.Substring("calculate:".Length - 1, question.Length - "calculate:".Length);
            dataPart = dataPart.Trim();
            int indexOfPlus = dataPart.IndexOf('+');
            if (indexOfPlus == -1)
                return "У тебя в голове мозги или кю?!";

            try
            {
                int leftNum = Int32.Parse(dataPart.Substring(0, indexOfPlus + 1).Trim());
                int rightNum = Int32.Parse(dataPart.Substring(indexOfPlus, dataPart.Length - 1).Trim());
                return String.Format("Я тебя полюбил — я тебя научу: {0}", leftNum + rightNum);
            }
            catch
            {
                return "У тебя в голове мозги или кю?!";
            }
        }
        private string handleGeneral(string question)
        {
            _generalQuestionsCount++;
            return _answerChoiceStrategy.GetAnswer(_answersDict, _generalQuestionsCount);
        }
    }
}
