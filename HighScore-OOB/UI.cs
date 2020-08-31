using System;
using System.Collections.Generic;
using System.Text;

namespace HighScore_OOB
{

    public class UI
    {

        public void Display<T>(List<HighScore<T>> highScores)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int n = 0; n < highScores.Count; n++)
            {
                Console.WriteLine(highScores[n].GetUsername + ": " + highScores[n].GetScore);
            }
        }
    }
}
