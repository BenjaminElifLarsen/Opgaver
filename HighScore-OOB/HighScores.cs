using System;
using System.Collections.Generic;
using System.Text;

namespace HighScore_OOB
{
    public class HighScores
    {
        List<HighScore<int>> highscoreList;
        int maxLength;

        public HighScores()
        {
            highscoreList = new List<HighScore<int>>();
            maxLength = 10;
        }

        public List<HighScore<int>> HighScoreList { get => highscoreList; set => highscoreList = value; }



        public int MaxHighScoreLength { get => maxLength; set { maxLength = value; Trim(); } }

        public void Add(string username, int score)
        {
            if (highscoreList.Count == 0)
            {
                highscoreList.Add(new HighScore<int>(username, score));
            }
            else
            {
                int highscoreCounter = highscoreList.Count;
                int pos = 0;
                while (pos <= highscoreCounter)
                {
                    if (pos < highscoreCounter && highscoreList[pos].GetScore < score)
                    {
                        highscoreList.Insert(pos, (new HighScore<int>(username, score)));
                        break;
                    }
                    else if (pos == highscoreCounter && highscoreCounter < maxLength)
                    {
                        highscoreList.Add(new HighScore<int>(username, score));
                        break;
                    }
                    pos++;
                }

            }
            Trim();
        }

        private void Trim()
        {
            while (highscoreList.Count > maxLength)
            {
                highscoreList.RemoveAt(highscoreList.Count - 1);
            }
        }

    }
}
