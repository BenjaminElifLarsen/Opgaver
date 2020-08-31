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
            Console.ForegroundColor = ConsoleColor.White;
            for (int n = 0; n < highScores.Count; n++)
            {
                Console.WriteLine(highScores[n].GetUsername + ": " + highScores[n].GetScore);
            }
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
