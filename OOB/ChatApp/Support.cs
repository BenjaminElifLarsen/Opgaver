using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChatApp
{
    class Support
    {
        public static void FoundForbiddenWord()
        {
            Console.WriteLine("Found a forbidden word, operation has been canceled");
            Console.WriteLine(RegexControl.GetForbiddenWords);
            Console.ReadKey();
            ClearBuffer();
        }

        public static void ClearBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        public static string SanitiseSingleQuotes(string text)
        {
            List<char> sanitised = new List<char>();
            foreach(char chr in text)
            {
                sanitised.Add(chr);
                if(chr == '\'')
                {
                    sanitised.Add('\'');
                }
            }

            return new string(sanitised.ToArray());
        }


        public static string[][] MessageTimePrepare(string[][] text)
        {

            for (int n = 0; n < text.Length; n++)
            {
                string[] dateTimeParts = text[n][0].Split(' ');
                string[] dateParts = dateTimeParts[0].Split('-');
                string[] timeParts = dateTimeParts[1].Split(':');
                DateTime oldTime = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]), int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
                string time = oldTime.ToLocalTime().ToString();
                float days = (float)(DateTime.Now - oldTime).TotalDays;
                if (days > 1)
                {
                    string[] dayParts = days.ToString(new CultureInfo("da-DK")).Split(',');
                    string daysShort;
                    string shortDay = dayParts[0] + "," + dayParts[1][0];
                    if (float.Parse(shortDay) - (int)days != 0)
                        daysShort = dayParts[0] + "." + dayParts[1][0];
                    else
                        daysShort = dayParts[0];
                    text[n][0] = daysShort + " days at " + time.Split(' ')[1];
                }
                else
                    text[n][0] = time;
                //Console.WriteLine("Row {0}", pos++);
            }
            return text;
        }

        public static List<Message> MessageTimePrepare(List<Message> text)
        {

            for (int n = 0; n < text.Count; n++)
            {
                string[] dateTimeParts = text[n].Time.ToString().Split(' ');
                string[] dateParts = dateTimeParts[0].Split('-');
                string[] timeParts = dateTimeParts[1].Split(':');
                DateTime oldTime = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]), int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
                string time = oldTime.ToLocalTime().ToString();
                float days = (float)(DateTime.Now - oldTime).TotalDays;
                if (days > 1)
                {
                    string[] dayParts = days.ToString(new CultureInfo("da-DK")).Split(',');
                    string daysShort;
                    string shortDay = dayParts[0] + "," + dayParts[1][0];
                    if (float.Parse(shortDay) - (int)days != 0)
                        daysShort = dayParts[0] + "." + dayParts[1][0];
                    else
                        daysShort = dayParts[0];

                    text[n].TimeSincePost = daysShort + " days at " + timeParts[0]+":"+ timeParts[1];
                }
                else
                    text[n].TimeSincePost = timeParts[0] + ":" + timeParts[1];
                //Console.WriteLine("Row {0}", pos++);
            }
            return text;
        }

        public static void DisplaySelect(string[][] text, string message)
        {
            //int pos = 1;
            Console.WriteLine(message);
            text = MessageTimePrepare(text);
            for (int n = 0; n < text.Length; n++)
            {
                foreach (string str in text[n])
                {
                    Console.Write(str + " | ");
                }
                Console.Write(Environment.NewLine);
            }
        }

    }
}
