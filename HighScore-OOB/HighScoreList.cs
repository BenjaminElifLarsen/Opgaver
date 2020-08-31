using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HighScore_OOB
{
    public class HighScoreList : IComparer<HighScore<int>>
    {
        private List<HighScore<int>> highscoreList;
        private int maxLength;

        public HighScoreList()
        {
            highscoreList = new List<HighScore<int>>();
            maxLength = 10;
        }

        public List<HighScore<int>> ScoreList { get => highscoreList; set => highscoreList = value; }



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

        public int Compare(HighScore<int> x, HighScore<int> y)
        {//lecture stuff
            return x.GetScore >= y.GetScore ? 1: -1;
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
