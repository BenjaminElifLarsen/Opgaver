using System;
using System.Collections.Generic;
using System.Text;

namespace HighScore_OOB
{

    public class HighScore<T>
    {
        T score;
        string username;

        private HighScore() { }

        public HighScore(string username, T score)
        {
            this.username = username;
            this.score = score;
        }

        public string GetUsername { get => username; }
        public T GetScore { get => score; }

    }
}
