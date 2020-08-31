using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace HighScore_OOB
{
    public class FileReadWrite
    {
        private readonly string pathway;
        private readonly string filename;
        private readonly string filetype;

        public FileReadWrite()
        {
            pathway = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Highscore\";
            filename = "Highscore";
            filetype = ".txt";
            CreateFolder();
            CreateFile();
        }

        private void CreateFolder()
        {
            if (!Directory.Exists(pathway))
                Directory.CreateDirectory(pathway);
        }

        private void CreateFile()
        {
            string pathFile = Path.Combine(pathway, filename + filetype);
            if (!File.Exists(pathFile))
                using (FileStream fs = File.Create(pathFile)) ;
        }

        public void Write<T>(List<HighScore<T>> highScores)
        {
            if(highScores.Count > 0) { 
                string pathFile = Path.Combine(pathway, filename + filetype);
                using (StreamWriter file = new StreamWriter(pathFile))
                {
                    for (int n = 0; n < highScores.Count; n++)
                        file.WriteLine("{0}:{1}", highScores[n].GetUsername, highScores[n].GetScore);
                }
            }
        }

        public List<HighScore<int>> Read/*<T>*/()
        {
            List<HighScore<int>> highScores = new List<HighScore<int>>();
            string pathFile = Path.Combine(pathway, filename + filetype);
            //Type test = typeof(T);
            string[] lines = File.ReadAllLines(pathFile);
            //var test = File.ReadAllText(pathFile);
            //var test2 = Environment.NewLine;
            if(lines.Length > 1 || (lines.Length == 1 && lines[0] != ""))
                for(int n = 0; n < lines.Length; n++)
                {
                    if(lines[0] != "") 
                    { 
                        string[] parts = lines[n].Split(':');
                        highScores.Add(new HighScore<int>(parts[0], int.Parse(parts[1])));
                    }
                }
            //highScores = SortingTest(highScores);
            return highScores;

        }

        //private List<HighScore<T>> SortingTest<T>(List<HighScore<T>> highScores)
        //{
        //    //HighScore<int> highScore1 = new HighScore<int>("", 1);
        //    //HighScore<int> highScore2 = new HighScore<int>("", 2);
        //    //var test = highScores.OrderBy((highScore1, highScore2) => highScore1.GetScore.CompareTo(highScore2.GetScore));
        //    //highScores.Sort(new HighScoreList());
        //    return highScores;
        //}

    }
}
