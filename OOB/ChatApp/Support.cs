using System;
using System.Collections.Generic;
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

        public static string SanitiseSingleQuoutes(string text)
        {
            List<char> sanitised = new List<char>();
            foreach(char chr in text)
            {
                sanitised.Add(chr);
                if(chr == '\'')
                {
                    sanitised.Add('\'');
                    sanitised.Add('\'');
                }
            }

            return new string(sanitised.ToArray());
        }
    }
}
