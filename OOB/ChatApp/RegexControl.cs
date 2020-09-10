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
        private static Regex rgLength = new Regex("[a-z A-Z 0-9]{4,16}");
        private static Regex rgLengthPassword = new Regex("[a-z A-Z 0-9" + specialSigns + "]{8,26}");
        private static Regex rgForbiddenWords = new Regex(@"\bDrop\b",RegexOptions.IgnoreCase);
        private static Regex rgForbiddenSigns = new Regex("[^a-z ^A-Z ^0-9 ^" + specialSigns + "]{1,}");
        private static Regex rgSingleQuote = new Regex("[\' ]{2,}");
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

        /// <summary>
        /// Uses Regex to ensure <paramref name="text"/> is of a specific length of valid signs. Used for passwords. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Uses Regex to check if <paramref name="text"/> contains one or more forbidden words. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool ContainsDrop(string text)
        {
            return rgForbiddenWords.IsMatch(text);
        }

        /// <summary>
        /// Uses Regex to check if <paramref name="text"/> contains one or more forbidden signs 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool ContainsForbiddenSigns(string text)
        {
            return rgForbiddenSigns.IsMatch(text);
        }

        /// <summary>
        /// Uses Regex to check if <paramref name="text"/> contains two or more single quotes. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool ContainsSingleQuouteMark(string text)
        {
            return rgSingleQuote.IsMatch(text);
        }
    }
}
