using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public void Write<T>(List<string> usernames, List<T> scores)
        {
            string pathFile = Path.Combine(pathway, filename + filetype);
            using (StreamWriter file = new StreamWriter(pathFile))
            {
                for (int n = 0; n < usernames.Count; n++)
                    file.WriteLine(usernames[n] + ":" + scores[n]);
            }
        }

        public void Read/*<T>*/(out List<string> usernames, out List<int> scores)
        {
            usernames = new List<string>();
            scores = new List<int>();
            string pathFile = Path.Combine(pathway, filename + filetype);
            //Type test = typeof(T);
            string[] lines = File.ReadAllLines(pathFile);
            for(int n = 0; n < lines.Length; n++)
            {
                string[] parts = lines[n].Split(':');
                usernames.Add(parts[0]);
                scores.Add(int.Parse(parts[1]));
            }

            //using (FileStream fs = File.OpenRead(pathFile))
            //{
            //    
            //}

        }

    }
}
