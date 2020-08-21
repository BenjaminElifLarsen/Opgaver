using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LagerSystem
{
    public static class RegexControl
    {
        private static string specialSigns = @"_ \- \= \+ \. \,";
        private static Regex rgSpeical = new Regex(@"[" + specialSigns + "]{1,}");
        private static Regex rgValues = new Regex("[0-9]{1,}");
        private static Regex rgLettersLower = new Regex("[a-z]{1,}");
        private static Regex rgLettersUpper = new Regex("[A-Z]{1,}");
        private static Regex rgLength = new Regex("^[a-z A-Z 0-9" + specialSigns + "]{6,16}$");

        public static string GetSpecialSigns { get => specialSigns; }


        public static bool IsValidLength(string text)
        {
            return rgLength.IsMatch(text);
        }

        public static bool IsValidValues(string text)
        {
            return rgValues.IsMatch(text);
        }

        public static bool IsValidLettersLower(string text)
        { 
            return rgLettersLower.IsMatch(text);
        }

        public static bool IsValidLettersUpper(string text)
        {
            return rgLettersUpper.IsMatch(text);
        }

        public static bool IsValidSpecial(string text)
        {
            return rgSpeical.IsMatch(text);
        }

    }



}
