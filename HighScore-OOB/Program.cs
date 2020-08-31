using System;
using System.Collections.Generic;
using System.IO;

namespace HighScore_OOB
{
    class Program
    {
        static FileReadWrite fileRW;
        static HighScore highScore;
        static UI ui;

        static void Main(string[] args)
        {
            fileRW = new FileReadWrite();
            highScore = new HighScore();
            ui = new UI();
            highScore.Add("Bob", 4);
            highScore.Add("Salsa", 10);
            ui.Display(highScore.Usernames, highScore.Scores);
            fileRW.Write(highScore.Usernames, highScore.Scores);
            fileRW.Read(out List<string> testNames, out List<int> testScores);
            Console.WriteLine("Data Read:");
            ui.Display(testNames, testScores);
        }
    }

}
