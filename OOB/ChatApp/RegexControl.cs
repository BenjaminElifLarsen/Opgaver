using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ChatApp
{
    /// <summary>
    /// Contains functions that use Regex.
    /// </summary>
    class RegexControl
    {
        private static string specialSigns = @"_ \- \= \+ \. \,";
        private static Regex rgSpeical = new Regex(@"[" + specialSigns + "]{1,}");
        private static Regex rgValues = new Regex("[0-9]{1,}");
        private static Regex rgLettersLower = new Regex("[a-z]{1,}");
        private static Regex rgLettersUpper = new Regex("[A-Z]{1,}");
        private static Regex rgLength = new Regex("^[a-z A-Z 0-9]{4,16}$");
        private static Regex rgLengthPassword = new Regex("^[a-z A-Z 0-9" + specialSigns + "]{8,26}$");

        public static string GetSpecialSigns { get => specialSigns.Replace("\\", ""); }

        /// <summary>
        /// Uses Regex to ensure <paramref name="text"/> is of a specific length of valid signs.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidLength(string text)
        {
            return rgLength.IsMatch(text);
        }

        public static bool IsValidPasswordLength(string text)
        {
            return rgLengthPassword.IsMatch(text);
        }

        /// <summary>
        /// Uses Regex to ensure <paramref name="text"/> contains at least 1 number.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidValues(string text)
        {
            return rgValues.IsMatch(text);
        }

        /// <summary>
        /// Uses Regex to ensure <paramref name="text"/> contains at least 1 lowercase letter.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidLettersLower(string text)
        {
            return rgLettersLower.IsMatch(text);
        }

        /// <summary>
        /// Uses Regex to ensure <paramref name="text"/> contains at least 1 uppercase letter.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidLettersUpper(string text)
        {
            return rgLettersUpper.IsMatch(text);
        }

        /// <summary>
        /// Uses Regex to ensure <paramref name="text"/> contains at least 1 special symbol.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidSpecial(string text)
        {
            return rgSpeical.IsMatch(text);
        }
    }
}
