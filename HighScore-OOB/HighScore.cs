using System;
using System.Collections.Generic;
using System.Text;

namespace HighScore_OOB
{
    public class HighScore
    {
        List<string> usernames;
        List<int> scores;

        public HighScore()
        {
            usernames = new List<string>();
            scores = new List<int>();
        }

        public List<string> Usernames { get => usernames; set => usernames = value; }
        public List<int> Scores { get => scores; set => scores = value; } 

        public void Add(string username, int score)
        {
            if (scores.Count == 0)
            {
                usernames.Add(username);
                scores.Add(score);
            }
            else
            {
                int highscoreCounter = scores.Count;
                int pos = 0;
                while (pos <= highscoreCounter)
                {
                    if (pos < highscoreCounter && scores[pos] > score)
                    {
                        scores.Insert(pos, score);
                        usernames.Insert(pos, username);
                        break;
                    }
                    else if (pos == highscoreCounter && highscoreCounter < 10)
                    {
                        scores.Add(score);
                        usernames.Insert(pos, username);
                        break;
                    }
                    pos++;
                }

            }
            Trim();
        }

        private void Trim()
        {
            while (scores.Count > 10)
            {
                scores.RemoveAt(scores.Count - 1);
                usernames.RemoveAt(scores.Count - 1);
            }
        }

    }
}
