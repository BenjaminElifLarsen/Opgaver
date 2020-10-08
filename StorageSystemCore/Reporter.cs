using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageSystemCore
{
    public static class Reporter
    {
        private static readonly string pathwayLog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Storage System\\Log\\";
        private static readonly string filenameLog = "Log";
        private static readonly string filetypeLog = ".txt";

        private static readonly string pathwayError = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Storage System\\Error\\";
        private static readonly string filenameError = "Error";
        private static readonly string filetypeError = ".txt";

        public static string LogLocation { get => pathwayLog + filenameLog + filetypeLog; }

        public static string ErrorLocation { get => pathwayError + filenameError + filetypeError; }

        public static void Log(string log)
        {
            try
            {
                DateTime time = DateTime.Now;
                string timePoint = $"{time.Year}-{time.Month}-{time.Day}-{time.Hour}-{time.Minute}-{time.Second}-{time.Millisecond}";
                CreateFolder(pathwayLog);
                CreateFile(pathwayLog, filenameLog + filetypeLog);
                string pathFile = Path.Combine(pathwayLog, filenameLog + filetypeLog);
                while (FileInUse(pathwayLog, filenameLog, filetypeLog)) ;
                using (StreamWriter file = new StreamWriter(pathFile, true))
                {
                    file.WriteLine(timePoint);
                    file.Write(Environment.NewLine);
                    file.WriteLine(log);
                    file.Write(Environment.NewLine);
                    file.WriteLine("".PadLeft(100, '-'));
                    file.Write(Environment.NewLine);
                    file.Write(Environment.NewLine);
                    file.Close();
                }
            }
            catch (IOException e)
            {
                Report(e);
            }
        }

        public static void Report(Exception e)
        {
            DateTime time = DateTime.Now;
            string timePoint = $"{time.Year}-{time.Month}-{time.Day}-{time.Hour}-{time.Minute}-{time.Second}-{time.Millisecond}";
            CreateFolder(pathwayError);
            CreateFile(pathwayError, filenameError + filetypeError);
            string pathFile = Path.Combine(pathwayError, filenameError + filetypeError);
            while (FileInUse(pathwayError, filenameError, filetypeError)) ;
            using (StreamWriter file = new StreamWriter(pathFile, true))
            {
                file.WriteLine(timePoint);
                file.Write(Environment.NewLine);
                file.WriteLine(e.Source);
                file.Write(Environment.NewLine);
                file.WriteLine(e.Message);
                file.Write(Environment.NewLine);
                file.WriteLine(e.TargetSite);
                file.Write(Environment.NewLine);
                file.WriteLine(e);
                file.Write(Environment.NewLine);
                file.WriteLine("".PadLeft(100, '-'));
                file.Write(Environment.NewLine);
                file.Write(Environment.NewLine);
                file.Close();
            }

        }


        private static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private static void CreateFile(string path, string file)
        {
            string pathFile = Path.Combine(path, file);
            if (!File.Exists(pathFile))
                using (FileStream fs = File.Create(pathFile)) ;
        }

        private static bool FileInUse(string path, string filename, string filetype)
        {
            try
            {
                string pathFile = Path.Combine(path, filename + filetype);
                using (StreamWriter file = new StreamWriter(pathFile, true)) ;
                return false;

            }
            catch
            {
                return true;
            }
            throw new NotImplementedException();
        }

    }
}
