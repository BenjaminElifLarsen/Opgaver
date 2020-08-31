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
            Run();
        }


        private static void Run()
        {
            string[] options = new string[] { "Add Highscore", "Display Highscores", "Write Highscores", "Read Highscores", "Exit" };
            do
            {
                byte selected = MenuRun(options);
                switch (selected)
                {
                    case 0:
                        ui.EnterScore(highScore);
                        break;
                    case 1:
                        ui.Display(highScore.HighScoreList);
                        break;
                    case 2:
                        fileRW.Write(highScore.HighScoreList);
                        break;
                    case 3:
                        highScore.HighScoreList = fileRW.Read();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        }


        public static byte MenuRun(string[] options, string title = null)
        {
            byte hoveredOver = 0;
            bool selected;
            do
            {
                MenuDisplay(options, hoveredOver, title);
                hoveredOver = MenuSelection(out selected, options.Length, hoveredOver);
            } while (!selected);
            return hoveredOver;
        }

        private static byte MenuSelection(out bool selected, int optionAmount, byte currentHoveredOver = 0)
        {
            while (!Console.KeyAvailable) ;
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                selected = true;
                return currentHoveredOver;
            }
            selected = false;
            if (key == ConsoleKey.DownArrow && currentHoveredOver < optionAmount - 1)
                return ++currentHoveredOver;
            else if (key == ConsoleKey.UpArrow && currentHoveredOver > 0)
                return --currentHoveredOver;
            else
                return currentHoveredOver;
        }

        private static void MenuDisplay(string[] options, byte currentHoveredOver = 0, string title = null)
        {
            Console.Clear();
            if (title != null)
                Console.WriteLine(title);
            for (int n = 0; n < options.Length; n++)
                if (n == currentHoveredOver)
                {
                    Console.CursorLeft = 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(options[n]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.CursorLeft = 1;
                    Console.WriteLine(options[n]);
                }
        }

    }

}
