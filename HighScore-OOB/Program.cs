using System;
using System.Collections.Generic;
using System.IO;

namespace HighScore_OOB
{
    class Program
    {
        static FileReadWrite fileRW;
        static HighScores highScore;
        static UI ui;

        static void Main(string[] args)
        {
            fileRW = new FileReadWrite();
            highScore = new HighScores();
            ui = new UI();
            highScore.Add("Bob", 4);
            highScore.Add("Salsa", 10);
            Console.WriteLine("Data in highScore:");
            ui.Display(highScore.GetHighScoreList);
            fileRW.Write(highScore.GetHighScoreList);
            fileRW.Read(out List<HighScore<int>> testHighScore);
            Console.WriteLine("Data Read:");
            ui.Display(testHighScore);
        }
    }

}
