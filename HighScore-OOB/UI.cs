using System;
using System.Collections.Generic;
using System.Text;

namespace HighScore_OOB
{

    public class UI
    {


        public void Display<T>(List<string> usernames, List<T> scores)
        {
            for (int n = 0; n < usernames.Count; n++)
            {
                Console.WriteLine(usernames[n] + ": " + scores[n]);
            }
        }
    }
}
