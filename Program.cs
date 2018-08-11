using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBot_BGirnyk.Stratagy;

namespace ChatBot_BGirnyk
{
    class Program
    {
        private static string dictPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\AppData\\answers.txt";
        static void Main(string[] args)
        {

            string[] dict = loadDictionary(dictPath);

            var chatBot = new ChatBot(dict, new RandStratagy());

            Console.WriteLine("welcome");
            while (true)
            {

                string input = Console.ReadLine();
                string answer = chatBot.GetAnswer(input);
                Console.WriteLine(answer);
            }

        }

        private static string[] loadDictionary(string dictPath)
        {

            var lineCount = 0;
            using (var reader = File.OpenText(dictPath))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }

            var returnDict = new string[lineCount];

            int curline = 0;

            using (FileStream fs = File.Open(dictPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    returnDict[curline] = line.Substring(1,line.Length-1);
                    curline++;
                }
            }

            return returnDict;

        }
    }
}
