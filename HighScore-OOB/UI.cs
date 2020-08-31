using System;
using System.Collections.Generic;
using System.Text;

namespace HighScore_OOB
{

    public class UI
    {

        public void EnterScore(HighScores highScores)
        {
            Console.Clear();
            Console.Write("Enter Username: ");
            string username = Console.ReadLine().Trim();
            Console.Write("Enter Score: ");
            int result;
            while (!int.TryParse(Console.ReadLine(), out  result));
            highScores.Add(username, result);
            Console.Clear();
        }

        public void Display<T>(List<HighScore<T>> highScores)
        {
            Console.Clear();
            if (highScores.Count != 0)
            {
                ConsoleColor colour1 = ConsoleColor.Green;
                ConsoleColor colour2 = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Username : Score");
                for (int n = 0; n < highScores.Count; n++)
                {
                    Console.ForegroundColor = n % 2 == 0 ? colour1 : colour2;
                    Console.WriteLine("{0} : {1}", highScores[n].GetUsername, highScores[n].GetScore);
                }
            }
            else
                Console.WriteLine("No highscores.");
            Console.ReadKey(true);
        }
    }
}
